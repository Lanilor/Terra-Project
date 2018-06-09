using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.Noise;
using UnityEngine;

namespace TerraFW
{

    public class GenStep_BetterCaves : GenStep
	{

        private const float DirectionNoiseFrequency = 0.00205f;
        private const float TunnelWidthNoiseFrequency = 0.020f;

		private static readonly SimpleCurve TunnelsWidthPerRockCount = new SimpleCurve
		{
			{
				new CurvePoint(300f, 2.5f),
				true
			},
			{
				new CurvePoint(600f, 4.2f),
				true
			},
			{
				new CurvePoint(6000f, 9.5f),
				true
			},
			{
				new CurvePoint(30000f, 15.5f),
				true
			}
		};

        private ModExt_Biome_GenStep_BetterCaves extCaves;
        private ModuleBase directionNoise;
        private ModuleBase tunnelWidthNoise;
		private static HashSet<IntVec3> groupSet = new HashSet<IntVec3>();
		private static HashSet<IntVec3> groupVisited = new HashSet<IntVec3>();
        private static List<IntVec3> subGroup = new List<IntVec3>();
        private static List<IntVec3> tmpCells = new List<IntVec3>();
        private static HashSet<IntVec3> tmpGroupSet = new HashSet<IntVec3>();

		public override void Generate(Map map)
		{
			if (!Find.World.HasCaves(map.Tile))
			{
				return;
			}
            ModExt_Biome_GenStep_BetterCaves extCaves = map.Biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
            if (extCaves == null)
            {
                return;
            }
            this.extCaves = extCaves;
            directionNoise = new Perlin(DirectionNoiseFrequency, 2.0, 0.5, 4, Rand.Int, QualityMode.Medium);
            tunnelWidthNoise = new Perlin(TunnelWidthNoiseFrequency, 2.0, 0.5, 4, Rand.Int, QualityMode.Medium);
            tunnelWidthNoise = new ScaleBias(0.4, 1.0, tunnelWidthNoise);
			MapGenFloatGrid elevation = MapGenerator.Elevation;
			BoolGrid visited = new BoolGrid(map);
			List<IntVec3> rockCells = new List<IntVec3>();
			foreach (IntVec3 cell in map.AllCells)
			{
				if (!visited[cell] && this.IsRock(cell, elevation, map))
				{
					rockCells.Clear();
					map.floodFiller.FloodFill(cell, (IntVec3 x) => IsRock(x, elevation, map), delegate(IntVec3 x)
					{
						visited[x] = true;
						rockCells.Add(x);
					});
                    Trim(rockCells, map);
                    RemoveSmallDisconnectedSubGroups(rockCells, map);
					if (rockCells.Count >= extCaves.minRocksToGenerateAnyTunnel)
					{
						StartWithTunnel(rockCells, map);
					}
				}
			}
            // Smooth caves after generation
            SmoothGenerated(map);
		}

        private void Trim(List<IntVec3> group, Map map)
        {
            // Remove spiky rock areas and open small bridges
            // This is like transforming the rock into a spiky dough and letting it flow into a blob with lesser surface area
            GenMorphology.Open(group, 6, map);
        }

		private bool IsRock(IntVec3 c, MapGenFloatGrid elevation, Map map)
		{
			return c.InBounds(map) && elevation[c] > 0.7f;
		}

		private void StartWithTunnel(List<IntVec3> group, Map map)
		{
            int tunnelSystemCount = GenMath.RoundRandom((float)group.Count * extCaves.startTunnelsPer10k / 10000f);
            tunnelSystemCount = Mathf.Min(tunnelSystemCount, extCaves.maxStartTunnelsPerRockGroup);
			float startWidth = TunnelsWidthPerRockCount.Evaluate((float)group.Count);
			for (int i = 0; i < tunnelSystemCount; i++)
			{
				IntVec3 mostDistantStartingCell = IntVec3.Invalid;
				float distanceToCave = -1f;
				float dir = -1f;
				float distAtDirToNextEnd = -1f;
				for (int j = 0; j < 10; j++)
				{
					IntVec3 edgeCell = FindRandomEdgeCellForTunnel(group, map);
					float currDistance = GetDistToCave(edgeCell, group, map, 40f, false);
					float currDir8MaxDist;
					float currAngle = FindBestInitialDir(edgeCell, group, out currDir8MaxDist);
					if (!mostDistantStartingCell.IsValid || currDistance > distanceToCave || (currDistance == distanceToCave && currDir8MaxDist > distAtDirToNextEnd))
					{
						mostDistantStartingCell = edgeCell;
						distanceToCave = currDistance;
						dir = currAngle;
						distAtDirToNextEnd = currDir8MaxDist;
					}
				}
                float width = startWidth * Rand.Range(extCaves.tunnelStartWidthFactorMin, extCaves.tunnelStartWidthFactorMax);
				this.Dig(mostDistantStartingCell, dir, width, group, map, BranchType.Normal, 1);
			}
		}

		private IntVec3 FindRandomEdgeCellForTunnel(List<IntVec3> group, Map map)
		{
			MapGenFloatGrid caves = MapGenerator.Caves;
			IntVec3[] cardinalDirections = GenAdj.CardinalDirections;
			tmpCells.Clear();
			tmpGroupSet.Clear();
			tmpGroupSet.AddRange(group);
			for (int i = 0; i < group.Count; i++)
			{
				if (group[i].DistanceToEdge(map) >= 3 && caves[group[i]] <= 0f)
				{
					for (int j = 0; j < 4; j++)
					{
						IntVec3 item = group[i] + cardinalDirections[j];
						if (!tmpGroupSet.Contains(item))
						{
							tmpCells.Add(group[i]);
							break;
						}
					}
				}
			}
			if (!tmpCells.Any<IntVec3>())
			{
				Log.Warning("Could not find any valid edge cell.");
				return group.RandomElement<IntVec3>();
			}
			return tmpCells.RandomElement<IntVec3>();
		}

		private float FindBestInitialDir(IntVec3 start, List<IntVec3> group, out float dist)
		{
			float distE = (float)GetDistToNonRock(start, group, IntVec3.East, 40);
			float distW = (float)GetDistToNonRock(start, group, IntVec3.West, 40);
			float distS = (float)GetDistToNonRock(start, group, IntVec3.South, 40);
			float distN = (float)GetDistToNonRock(start, group, IntVec3.North, 40);
			float distNW = (float)GetDistToNonRock(start, group, IntVec3.NorthWest, 40);
			float distNE = (float)GetDistToNonRock(start, group, IntVec3.NorthEast, 40);
			float distSW = (float)GetDistToNonRock(start, group, IntVec3.SouthWest, 40);
			float distSE = (float)GetDistToNonRock(start, group, IntVec3.SouthEast, 40);
			dist = Mathf.Max(new float[] { distE, distW, distS, distN, distNW, distNE, distSW, distSE });
			return GenMath.MaxByRandomIfEqual<float>(0f, distE + distSE / 2f + distNE / 2f, 45f, distSE + distS / 2f + distE / 2f, 90f, distS + distSE / 2f + distSW / 2f, 135f, distSW + distS / 2f + distW / 2f, 180f, distW + distSW / 2f + distNW / 2f, 225f, distNW + distN / 2f + distW / 2f, 270f, distN + distNE / 2f + distNW / 2f, 315f, distNE + distN / 2f + distE / 2f);
		}

		private void Dig(IntVec3 start, float dir, float width, List<IntVec3> group, Map map, BranchType branchType, int depth, HashSet<IntVec3> visited = null)
		{
			Vector3 vector = start.ToVector3Shifted();
			IntVec3 digCell = start;
			float currTunnelLength = 0f;
			MapGenFloatGrid elevation = MapGenerator.Elevation;
			MapGenFloatGrid caves = MapGenerator.Caves;
            int stepBranchLeft = 0;
            int stepBranchRight = 0;
            float widthOffsetPerCell = Rand.Range(extCaves.widthOffsetPerCellMin, extCaves.widthOffsetPerCellMax);
			if (visited == null)
			{
				visited = new HashSet<IntVec3>();
			}
			tmpGroupSet.Clear();
			tmpGroupSet.AddRange(group);
			int step = 0;
			while (true)
			{
				// Check for branching
                if (branchType == BranchType.Normal)
                {
                    if (step - stepBranchLeft >= extCaves.allowBranchingAfterSteps && Rand.Chance(extCaves.branchChance))
                    {
                        BranchType newBranchType = RandomBranchTypeByChance();
                        float newWidth = CalculateBranchWidth(branchType, width);
                        if (newWidth > extCaves.minTunnelWidth)
                        {
                            DigInBestDirection(digCell, dir, new FloatRange(-90f, -40f), newWidth, group, map, newBranchType, depth, visited);
                            stepBranchLeft = step;
                        }
                    }
                    if (step - stepBranchRight >= extCaves.allowBranchingAfterSteps && Rand.Chance(extCaves.branchChance))
                    {
                        BranchType newBranchType = RandomBranchTypeByChance();
                        float newWidth = CalculateBranchWidth(branchType, width);
                        if (newWidth > extCaves.minTunnelWidth)
                        {
						    DigInBestDirection(digCell, dir, new FloatRange(40f, 90f), newWidth, group, map, newBranchType, depth, visited);
						    stepBranchRight = step;
                        }
					}
				}
                // Set cave and go further
				bool hitAnotherTunnel;
				SetCaveAround(digCell, width, map, visited, out hitAnotherTunnel);
				if (hitAnotherTunnel)
				{
                    // Stop digging when stuck on another tunnel
					return;
				}
                // Stop if cave is too long
                if (branchType == BranchType.Room && step >= extCaves.branchRoomMaxLength)
                {
                    return;
                }
                // Move temp dig cell forward until on another cell
				while (vector.ToIntVec3() == digCell)
				{
					vector += Vector3Utility.FromAngleFlat(dir) * 0.5f;
					currTunnelLength += 0.5f;
				}
                IntVec3 nextDigCell = vector.ToIntVec3();
				if (!tmpGroupSet.Contains(nextDigCell))
				{
                    // Stop digging if new cell is outside of the rock area
					return;
				}
                // Copy to a new IntVec3 to not just store a reference to the other one in visited
                IntVec3 visitedCell = new IntVec3(digCell.x, 0, nextDigCell.z);
				if (IsRock(visitedCell, elevation, map))
				{
					caves[visitedCell] = Mathf.Max(caves[visitedCell], width);
					visited.Add(visitedCell);
				}
                // Make tunnel smaller for next step
                if (branchType == BranchType.Tunnel)
                {
                    width -= widthOffsetPerCell * extCaves.widthOffsetPerCellTunnelFactor;
                }
                else
                {
                    width -= widthOffsetPerCell;
                }
                if (width < extCaves.minTunnelWidth)
				{
                    // Stop digging if tunnel becomes too small
					return;
                }
                digCell = nextDigCell;
                dir += (float)directionNoise.GetValue(currTunnelLength * 60f, (float)start.x * 200f, (float)start.z * 200f) * extCaves.directionChangeSpeed;
				step++;
			}
		}

		private void DigInBestDirection(IntVec3 curIntVec, float curDir, FloatRange dirOffset, float width, List<IntVec3> group, Map map, BranchType branchType, int depth, HashSet<IntVec3> visited = null)
		{
            // Calculate best direction
            float dir = -1f;
            int distAtDirToOutside = -1;
			for (int i = 0; i < 6; i++)
			{
				float randomDir = curDir + dirOffset.RandomInRange;
				int distToOutside = GetDistToNonRock(curIntVec, group, randomDir, 50);
				if (distToOutside > distAtDirToOutside)
				{
					distAtDirToOutside = distToOutside;
					dir = randomDir;
				}
			}
            if (distAtDirToOutside < extCaves.minDistToOutsideForBranching)
			{
				return;
			}
            // Dig new tunnel one level deeper
			Dig(curIntVec, dir, width, group, map, branchType, depth + 1, visited);
		}

		private void SetCaveAround(IntVec3 center, float tunnelWidth, Map map, HashSet<IntVec3> visited, out bool hitAnotherTunnel)
		{
			hitAnotherTunnel = false;
			int numCellsInRadius = GenRadial.NumCellsInRadius(tunnelWidth / 2f * tunnelWidthNoise.GetValue(center));
			MapGenFloatGrid elevation = MapGenerator.Elevation;
			MapGenFloatGrid caves = MapGenerator.Caves;
			for (int i = 0; i < numCellsInRadius; i++)
			{
				IntVec3 currCell = center + GenRadial.RadialPattern[i];
				if (this.IsRock(currCell, elevation, map))
				{
					if (caves[currCell] > 0f && !visited.Contains(currCell))
					{
						hitAnotherTunnel = true;
					}
					caves[currCell] = Mathf.Max(caves[currCell], tunnelWidth);
					visited.Add(currCell);
				}
			}
		}

        private BranchType RandomBranchTypeByChance()
        {
            float rand = Rand.Value;
            if (rand < extCaves.branchChanceTypeRoom)
            {
                return BranchType.Room;
            }
            if (rand < extCaves.branchChanceTypeRoom + extCaves.branchChanceTypeTunnel)
            {
                return BranchType.Tunnel;
            }
            return BranchType.Normal;
        }

        private float CalculateBranchWidth(BranchType branchType, float prevWidth)
        {
            if (branchType == BranchType.Room)
            {
                return Rand.Range(extCaves.branchRoomFixedWidthMin, extCaves.branchRoomFixedWidthMax);
            }
            if (branchType == BranchType.Tunnel)
            {
                return Rand.Range(extCaves.branchTunnelFixedWidthMin, extCaves.branchTunnelFixedWidthMax);
            }
            return prevWidth * Rand.Range(extCaves.branchWidthFactorMin, extCaves.branchWidthFactorMax);
        }

		private int GetDistToNonRock(IntVec3 from, List<IntVec3> group, IntVec3 offset, int maxDist)
		{
			groupSet.Clear();
			groupSet.AddRange(group);
			for (int i = 0; i <= maxDist; i++)
			{
				IntVec3 item = from + offset * i;
				if (!groupSet.Contains(item))
				{
					return i;
				}
			}
			return maxDist;
		}

		private int GetDistToNonRock(IntVec3 from, List<IntVec3> group, float dir, int maxDist)
		{
			groupSet.Clear();
			groupSet.AddRange(group);
			Vector3 a = Vector3Utility.FromAngleFlat(dir);
			for (int i = 0; i <= maxDist; i++)
			{
				IntVec3 item = (from.ToVector3Shifted() + a * i).ToIntVec3();
				if (!groupSet.Contains(item))
				{
					return i;
				}
			}
			return maxDist;
		}

		private float GetDistToCave(IntVec3 cell, List<IntVec3> group, Map map, float maxDist, bool treatOpenSpaceAsCave)
		{
			MapGenFloatGrid caves = MapGenerator.Caves;
			tmpGroupSet.Clear();
			tmpGroupSet.AddRange(group);
			int cellCount = GenRadial.NumCellsInRadius(maxDist);
			IntVec3[] radialPattern = GenRadial.RadialPattern;
			for (int i = 0; i < cellCount; i++)
			{
				IntVec3 loc = cell + radialPattern[i];
				if ((treatOpenSpaceAsCave && !tmpGroupSet.Contains(loc)) || (loc.InBounds(map) && caves[loc] > 0f))
				{
					return cell.DistanceTo(loc);
				}
			}
			return maxDist;
		}

		private void RemoveSmallDisconnectedSubGroups(List<IntVec3> group, Map map)
		{
			groupSet.Clear();
			groupSet.AddRange(group);
			groupVisited.Clear();
			for (int i = 0; i < group.Count; i++)
			{
				if (!groupVisited.Contains(group[i]) && groupSet.Contains(group[i]))
				{
					subGroup.Clear();
					map.floodFiller.FloodFill(group[i], (IntVec3 x) => groupSet.Contains(x), delegate(IntVec3 x)
					{
						subGroup.Add(x);
						groupVisited.Add(x);
					});
                    if (subGroup.Count < extCaves.minRocksToGenerateAnyTunnel || (float)subGroup.Count < 0.05f * group.Count)
					{
						for (int j = 0; j < subGroup.Count; j++)
						{
							groupSet.Remove(subGroup[j]);
						}
					}
				}
			}
			group.Clear();
			group.AddRange(groupSet);
		}

        private void SmoothGenerated(Map map)
        {
            MapGenFloatGrid caves = MapGenerator.Caves;
            List<IntVec3> caveCells = new List<IntVec3>();
            foreach (IntVec3 cell in map.AllCells)
            {
                if (caves[cell] > 0f)
                {
                    caveCells.Add(cell);
                }
            }
            GenMorphology.Close(caveCells, 3, map);
            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.CloseToEdge(map, 3)) // Skip changing caves near the edge
                {
                    continue;
                }
                if (caveCells.Contains(cell)) // Cave spot on cell (cave should be there)
                {
                    if (caves[cell] <= 0f) // No existing cave
                    {
                        caves[cell] = 1f; // Add new cave spot
                    }
                }
                else // No cave spot on cell (cave should not be there)
                {
                    if (caves[cell] > 0f) // Old existing cave
                    {
                        caves[cell] = 0f; // Remove cave spot
                    }
                }
            }
        }

        private enum BranchType
        {
            Normal,
            Room,
            Tunnel
        };

	}

}
