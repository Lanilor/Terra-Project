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

    [HarmonyPatch(typeof(WorldGenStep_Terrain))]
    [HarmonyPatch("GenerateTileFor")]
    public class Harmony_WorldGenStep_Terrain_GenerateTileFor
    {
        
        public static void Postfix(ref Tile __result, int tileID)
        {
            // Module replacement
            ModExt_Biome_Replacement extReplacement = __result.biome.GetModExtension<ModExt_Biome_Replacement>();
            if (extReplacement != null)
            {
                // Replace elevation
                if (extReplacement.replaceElevation)
                {
                    __result.elevation = Rand.RangeInclusive(extReplacement.elevationMin, extReplacement.elevationMax);
                }
                // Replace hilliness
                if (extReplacement.replaceHilliness != null)
                {
                    __result.hilliness = (Hilliness)extReplacement.replaceHilliness;
                }
            }
            // Module temperature
            ModExt_Biome_Temperature extTemperature = __result.biome.GetModExtension<ModExt_Biome_Temperature>();
            if (extTemperature != null)
            {
                // Adjust base temperature
                __result.temperature = __result.temperature * (1f - extTemperature.tempStableWeight) + extTemperature.tempStableValue * extTemperature.tempStableWeight + extTemperature.tempOffset;
            }
        }

    }

}
