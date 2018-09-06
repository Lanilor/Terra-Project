using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.Noise;

namespace TerraCore
{

    public class GenStep_CaveRockChunks : GenStep
	{
        
		private ModuleBase freqFactorNoise;
		private const float ThreshLooseRock = 0.55f;
		private const float PlaceProbabilityPerCell = 0.006f;
		private const float RubbleProbability = 0.2f;
        private static readonly IntRange MaxRockChunksPerGroup = new IntRange(1, 6);

        public override int SeedPart
        {
            get
            {
                return 488758298;
            }
        }

        public override void Generate(Map map, GenStepParams parms)
		{
			if (map.TileInfo.WaterCovered)
			{
				return;
			}
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRockChunks != RockChunksOverwriteType.AddToCaves)
            {
                return;
            }
            freqFactorNoise = new Perlin(0.014999999664723873, 2.0, 0.5, 6, Rand.Int, QualityMode.Medium);
			freqFactorNoise = new ScaleBias(1.0, 1.0, freqFactorNoise);
			NoiseDebugUI.StoreNoiseRender(freqFactorNoise, "cave_rock_chunks_freq_factor");
			MapGenFloatGrid elevation = MapGenerator.Elevation;
			foreach (IntVec3 cell in map.AllCells)
			{
                float probVal = PlaceProbabilityPerCell * freqFactorNoise.GetValue(cell);
                if (elevation[cell] >= ThreshLooseRock && Rand.Value < probVal)
				{
					GrowLowRockFormationFrom(cell, map);
				}
			}
			freqFactorNoise = null;
		}

		private void GrowLowRockFormationFrom(IntVec3 root, Map map)
		{
			ThingDef rockRubble = ThingDefOf.Filth_RubbleRock;
			ThingDef mineableThing = Find.World.NaturalRockTypesIn(map.Tile).RandomElement<ThingDef>().building.mineableThing;
			Rot4 excludedDir = Rot4.Random;
			MapGenFloatGrid elevation = MapGenerator.Elevation;
			IntVec3 cell = root;
            int maxRockChunks = MaxRockChunksPerGroup.RandomInRange;
            for (int i = 0; i < maxRockChunks; i++)
            {
                Rot4 nextDir = Rot4Utility.RandomButExclude(excludedDir);
                cell += nextDir.FacingCell;
                if (!cell.InBounds(map) || cell.GetEdifice(map) != null || cell.GetFirstItem(map) != null)
                {
                    break;
                }
                if (elevation[cell] < ThreshLooseRock)
                {
                    break;
                }
                List<TerrainAffordanceDef> affordances = map.terrainGrid.TerrainAt(cell).affordances;
                if (!(affordances.Contains(TerrainAffordanceDefOf.Medium) || affordances.Contains(TerrainAffordanceDefOf.Heavy)))
                {
                    break;
                }
                GenSpawn.Spawn(mineableThing, cell, map);
                foreach (IntVec3 adjCell in GenAdj.AdjacentCellsAndInside)
                {
                    if (Rand.Value < RubbleProbability)
                    {
                        continue;
                    }
                    IntVec3 c = cell + adjCell;
                    if (c.InBounds(map))
                    {
                        bool cellUsed = false;
                        List<Thing> thingList = c.GetThingList(map);
                        for (int j = 0; j < thingList.Count; j++)
                        {
                            Thing thing = thingList[j];
                            if (thing.def.category != ThingCategory.Plant && thing.def.category != ThingCategory.Item && thing.def.category != ThingCategory.Pawn)
                            {
                                cellUsed = true;
                                break;
                            }
                        }
                        if (!cellUsed)
                        {
                            FilthMaker.MakeFilth(c, map, rockRubble, 1);
                        }
                    }
                }
            }
		}

	}

}
