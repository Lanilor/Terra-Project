using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using UnityEngine;
using System.Reflection.Emit;

namespace TerraFW
{

    [HarmonyPatch(typeof(GenStep_Terrain))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_Terrain_Generate
    {

        public static void Postfix(Map map)
        {
            IntVec3[] adj = GenAdj.AdjacentCells;
            TerrainDef[] grid = map.terrainGrid.topGrid;
            // Do stuff for biome TundraSkerries
            if (map.Biome == BiomeDefOf.TundraSkerries)
            {
                // Smooth island edges
                for (int i = 0, iLen = grid.Count(); i < iLen; i++)
                {
                    int count = 0;
                    if (grid[i] == TerrainDefOf.NormalSand || grid[i] == TerrainDefOf.Sandstone_Rough || grid[i] == TerrainDefOf.Granite_Rough || grid[i] == TerrainDefOf.Limestone_Rough || grid[i] == TerrainDefOf.Slate_Rough || grid[i] == TerrainDefOf.Marble_Rough)
                    {
                        IntVec3 currCell = map.cellIndices.IndexToCell(i);
                        for (int j = 0; j < 8; j++)
                        {
                            IntVec3 c = currCell + adj[j];
                            if (c.InBounds(map) && map.terrainGrid.TerrainAt(c) == TerrainDefOf.WaterOceanShallow)
                            {
                                count++;
                            }
                        }
                        if (count > 5)
                        {
                            map.terrainGrid.SetTerrain(currCell, TerrainDefOf.WaterOceanShallow);
                        }
                    }
                }
            }
            // Do general postfix replacement afterwards
            for (int i = 0, iLen = grid.Count(); i < iLen; i++)
            {
                // Check for too steep water edges
                if (grid[i] == TerrainDefOf.WaterOceanDeep)
                {
                    IntVec3 currCell = map.cellIndices.IndexToCell(i);
                    for (int j = 0; j < 8; j++)
                    {
                        IntVec3 c = currCell + adj[j];
                        if (c.InBounds(map) && map.terrainGrid.TerrainAt(c) == TerrainDefOf.WaterOceanShallow)
                        {
                            map.terrainGrid.SetTerrain(c, TerrainDefOf.WaterOceanSloping);
                            map.terrainGrid.SetTerrain(currCell, TerrainDefOf.WaterOceanSloping);
                        }
                    }
                }
            }
            // Overwrite roof if needed
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null)
            {
                if (extFtControl.overwriteRoof == RoofOverwriteType.FullStable)
                {
                    GenRoof.SetRoofComplete(map, RoofDefOf.RoofRockUncollapsable);
                }
                else if (extFtControl.overwriteRoof == RoofOverwriteType.DeepOnlyStable)
                {
                    GenRoof.SetStableDeepRoof(map);
                }
            }
        }

    }

    [HarmonyPatch(typeof(GenStep_Terrain))]
    [HarmonyPatch("TerrainFrom")]
    public class Harmony_GenStep_Terrain_TerrainFrom
    {

        public static void Postfix(ref TerrainDef __result, IntVec3 c, Map map)
        {
            BiomeDef biome = map.Biome;
            // General cell terrain replcement
            ModExt_Biome_Replacement extReplacement = biome.GetModExtension<ModExt_Biome_Replacement>() ?? ModExt_Biome_Replacement.defaultValues;
            // Replace sand (mostly from beach)
            if (__result == RimWorld.TerrainDefOf.Sand)
            {
                __result = extReplacement.sandReplacement;
            }
            // Replace gravel
            if (__result == RimWorld.TerrainDefOf.Gravel)
            {
                __result = extReplacement.gravelRelacement;
            }

            // Addtitional island terrainPatchMaker by fertility
            if (biome.HasModExtension<ModExt_Biome_GenStep_Islands>())
            {
                TerrainDef newTerrain = IslandNoises.TerrainAtFromTerrainPatchMakerByFertility(c, map, __result);
                if (newTerrain != null)
                {
                    __result = newTerrain;
                }
            }

            // Post-terrain-gen terrain replacement
            // Replace filler stone
            if (__result == TerrainDefOf.FillerStone)
            {
                __result = GenStep_RocksFromGrid.RockDefAt(c).building.naturalTerrain;
            }
        }

    }

}
