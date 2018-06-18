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
using RimWorld.Planet;
using Verse.Noise;

namespace TerraFW
{

    [HarmonyPatch(typeof(GenStep_CavesTerrain))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_CavesTerrain_Generate
    {
        
        public static bool Prefix(Map map)
        {
            if (!Find.World.HasCaves(map.Tile))
            {
                return false;
            }
            ModExt_Biome_GenStep_BetterCaves extCaves = map.Biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
            if (extCaves == null || (extCaves.terrainPatchMakerCaveWater == null && extCaves.terrainPatchMakerCaveGravel == null))
            {
                return true;
            }
            ModuleBase noiseWater = new Perlin(extCaves.terrainPatchMakerFrequencyCaveWater, 2.0, 0.5, 6, Rand.Int, QualityMode.Medium);
            ModuleBase noiseGravel = new Perlin(extCaves.terrainPatchMakerFrequencyCaveGravel, 2.0, 0.5, 6, Rand.Int, QualityMode.Medium);
            MapGenFloatGrid caves = MapGenerator.Caves;
            foreach (IntVec3 c in map.AllCells)
            {
                if (caves[c] > 0f)
                {
                    TerrainDef terrain = c.GetTerrain(map);
                    if (terrain != TerrainDefOf.WaterMovingShallow && terrain != TerrainDefOf.WaterMovingSloping && terrain != TerrainDefOf.WaterMovingChestDeep)
                    {
                        // Try set water terrain
                        float valWater = (float)noiseWater.GetValue(c);
                        TerrainDef currentTerrain = map.terrainGrid.TerrainAt(c);
                        TerrainDef newTerrainWater = TerrainThresholdWEO.TerrainAtValue(extCaves.terrainPatchMakerCaveWater, valWater, currentTerrain);
                        if (newTerrainWater != null)
                        {
                            map.terrainGrid.SetTerrain(c, newTerrainWater);
                        }
                        else
                        {
                            // Try set gravel terrain if no water was set first
                            float valGravel = (float)noiseGravel.GetValue(c);
                            TerrainDef newTerrainGravel = TerrainThresholdWEO.TerrainAtValue(extCaves.terrainPatchMakerCaveGravel, valGravel, currentTerrain);
                            if (newTerrainGravel != null)
                            {
                                map.terrainGrid.SetTerrain(c, newTerrainGravel);
                            }
                        }
                    }
                }
            }
            return false;
        }

    }

}
