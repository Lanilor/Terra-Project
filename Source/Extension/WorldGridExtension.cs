using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;

namespace TerraFW
{

	public static class WorldGridExtension
    {

        private static List<int> tmpNeighbors = new List<int>();

        public static int GetDirection6WayIntFromTo(this WorldGrid grid, int fromTileID, int toTileID)
        {
            float headingFromTo = grid.GetHeadingFromTo(fromTileID, toTileID);
            if (headingFromTo >= 330f || headingFromTo < 30f)
            {
                return 0;
            }
            if (headingFromTo < 90f)
            {
                return 1;
            }
            if (headingFromTo < 150f)
            {
                return 2;
            }
            if (headingFromTo < 210f)
            {
                return 3;
            }
            if (headingFromTo < 270f)
            {
                return 4;
            }
            return 5;
        }

        public static int GetTileNeighborByDirection6WayInt(this WorldGrid grid, int tileID, int dir)
        {
            grid.GetTileNeighbors(tileID, tmpNeighbors);
            foreach (int tile2 in tmpNeighbors)
            {
                if (grid.GetDirection6WayIntFromTo(tileID, tile2) == dir)
                {
                    return tile2;
                }
            }
            return tileID;
        }

        /*public static int?[] GetTileNeighborsSorted(this WorldGrid wg, int tileID)
        {
            int?[] sortedNeighbors = new int?[] { null, null, null, null, null, null };
            wg.GetTileNeighbors(tileID, tmpNeighbors);
            foreach (int tile2 in tmpNeighbors)
            {
                int dir = wg.GetDirection6WayIntFromTo(tileID, tile2);
                sortedNeighbors[dir] = tile2;
            }
            return sortedNeighbors;
        }*/

        /*public static void GetTileDirsOfNeighborsWithSameBiome(this WorldGrid wg, int tileID, List<int> outNeighborDirs, List<int> outAdjacentIds)
        {
            BiomeDef biome = wg[tileID].biome;
            wg.GetTileNeighbors(tileID, tmpNeighbors);
            for (int i = 0; i < tmpNeighbors.Count; i++)
            {
                if (wg[tmpNeighbors[i]].biome == biome)
                {
                    outNeighborDirs.Add(wg.GetDirection6WayIntFromTo(tileID, tmpNeighbors[i]));
                    outAdjacentIds.Add(i);
                }
            }
        }*/

        public static void GetTileDirsOfNeighborsByValidator(this WorldGrid wg, int tileID, List<int> outNeighborDirs, List<int> outAdjacentIds, TileNeightborPredicate validator)
        {
            Tile tile = wg[tileID];
            wg.GetTileNeighbors(tileID, tmpNeighbors);
            for (int i = 0; i < tmpNeighbors.Count; i++)
            {
                if (validator(tile, wg[tmpNeighbors[i]]))
                {
                    outNeighborDirs.Add(wg.GetDirection6WayIntFromTo(tileID, tmpNeighbors[i]));
                    outAdjacentIds.Add(i);
                }
            }
        }

        public static void GetTileGraphicDataFromNeighbors(this WorldGrid grid, int tileID, out int atlasX, out int atlasZ, out int rotVector, TileNeightborPredicate validator)
        {
            atlasX = 0;
            atlasZ = 0;
            rotVector = 0;
            //rotVector = Vector3.up;
            List<int> neighborDirs = new List<int>();
            List<int> adjacentIds = new List<int>();
            grid.GetTileDirsOfNeighborsByValidator(tileID, neighborDirs, adjacentIds, validator);
            int dirCount = neighborDirs.Count();
            if (dirCount > 0)
            {
                int dirSum = neighborDirs.Sum();
                int dirMin = neighborDirs.Min();
                int dirMax = neighborDirs.Max();
                int rotTargetDir = dirMin;
                if (dirCount == 1)
                {
                    atlasZ = 1;
                }
                else if (dirCount == 2)
                {
                    atlasX = 1;
                    int diff = dirMax - dirMin;
                    if (diff == 1 || diff == 5)
                    {
                        atlasX++;
                    }
                    if (diff == 2 || diff == 4)
                    {
                        atlasZ = 1;
                    }
                    if (diff == 4 || diff == 5)
                    {
                        rotTargetDir = dirMax;
                    }
                }
                else if (dirCount == 3)
                {
                    atlasX = 3;
                    if (neighborDirs.Count <= 2) { Log.Error("neighborDirs[2] out of range"); }
                    int diff1 = Math.Abs(neighborDirs[0] - neighborDirs[1]);
                    int diff2 = Math.Abs(neighborDirs[1] - neighborDirs[2]);
                    int diff3 = Math.Abs(neighborDirs[0] - neighborDirs[2]);
                    if (diff1 == 3 || diff2 == 3 || diff3 == 3)
                    {
                        atlasZ = 1;
                        if ((dirSum - 1) % 3 == 0)
                        {
                            atlasX++;
                        }
                        if (diff1 == 1 || diff1 == 5) { rotTargetDir = neighborDirs[2]; }
                        else if (diff2 == 1 || diff2 == 5) { rotTargetDir = neighborDirs[0]; }
                        else if (diff3 == 1 || diff3 == 5) { rotTargetDir = neighborDirs[1]; }
                    }
                    else
                    {
                        if ((diff1 == 2 || diff1 == 4) && (diff2 == 2 || diff2 == 4) && (diff3 == 2 || diff3 == 4))
                        {
                            atlasX++;
                        }
                        else
                        {
                            if (dirMin == 0 && dirMax == 5)
                            {
                                if (dirSum == 9) { rotTargetDir = 4; }
                                else { rotTargetDir = 5; }
                            }
                        }
                    }
                }
                else if (dirCount == 4)
                {
                    atlasX = 5;
                    if (neighborDirs.Count <= 3) { Log.Error("neighborDirs[3] out of range"); }
                    int diff1 = Math.Abs(neighborDirs[0] - neighborDirs[1]);
                    int diff2 = Math.Abs(neighborDirs[0] - neighborDirs[2]);
                    int diff3 = Math.Abs(neighborDirs[0] - neighborDirs[3]);
                    int diff4 = Math.Abs(neighborDirs[1] - neighborDirs[2]);
                    int diff5 = Math.Abs(neighborDirs[1] - neighborDirs[3]);
                    int diff6 = Math.Abs(neighborDirs[3] - neighborDirs[3]);
                    int distance3 = 0;
                    if (diff1 == 3) { distance3++; }
                    if (diff2 == 3) { distance3++; }
                    if (diff3 == 3) { distance3++; }
                    if (diff4 == 3) { distance3++; }
                    if (diff5 == 3) { distance3++; }
                    if (diff6 == 3) { distance3++; }
                    if (distance3 == 2)
                    {
                        atlasZ = 1;
                        if (dirMin == 0 && dirMax == 5) { rotTargetDir = 5; }
                    }
                    else if (dirSum % 2 == 0)
                    {
                        atlasX++;
                        if (dirMin == 0 && dirMax == 5) { rotTargetDir = 9 - dirSum / 2; }
                    }
                    else
                    {
                        if (!neighborDirs.Contains(5) && !neighborDirs.Contains(1)) { }
                        else if (!neighborDirs.Contains(4) && !neighborDirs.Contains(0)) { rotTargetDir = 5; }
                        else { rotTargetDir = (15 - dirSum) / 2; }
                    }
                }
                else if (dirCount == 5)
                {
                    atlasX = 6;
                    atlasZ = 1;
                    if (dirSum > 10) { rotTargetDir = 16 - dirSum; }
                }
                else
                {
                    atlasX = 2;
                    atlasZ = 1;
                }
                rotVector = rotTargetDir;
                int neighborIndex = neighborDirs.IndexOf(rotTargetDir);
                if (neighborIndex >= 0)
                {
                    if (adjacentIds.Count <= neighborIndex) { Log.Error("adjacentIds[neighborIndex] out of range"); }
                    int rotTargetAdjId = adjacentIds[neighborIndex];

                    List<Vector3> tmpVertsSelf = new List<Vector3>();
                    List<Vector3> tmpVertsNeighbor = new List<Vector3>();
                    List<int> sameIndex = new List<int>();
                    grid.GetTileVertices(tileID, tmpVertsSelf);
                    grid.GetTileVertices(grid.GetTileNeighbor(tileID, rotTargetAdjId), tmpVertsNeighbor);
                    for (int i = 0; i < tmpVertsSelf.Count; i++)
                    {
                        if (tmpVertsSelf.Count <= i) { Log.Error("tmpVertsSelf[i] out of range"); }
                        if (tmpVertsNeighbor.Contains(tmpVertsSelf[i]))
                        {
                            sameIndex.Add(i);
                        }
                    }
                    if (sameIndex.Contains(0) && sameIndex.Contains(5))
                    {
                        rotVector = 1;
                    }
                    else
                    {
                        rotVector = sameIndex.Max() + 1;
                    }
                    rotVector = rotVector % 6;

                    //Vector3 rotTargetPos = grid.GetTileCenter(grid.GetTileNeighbor(tileID, rotTargetAdjId));
                    //rotVector = (rotTargetPos - grid.GetTileCenter(tileID)).normalized;
                }
            }
        }

    }

}
