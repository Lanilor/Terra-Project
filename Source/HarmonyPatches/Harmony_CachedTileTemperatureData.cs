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

    public class Harmony_CachedTileTemperatureData_CalculateOutdoorTemperatureAtTile
    {
        
        public static void Postfix(object __instance, ref float __result)
        {
            int tile = Traverse.Create(__instance).Field("tile").GetValue<int>();
            ModExt_Biome_Temperature extTemperature = Find.WorldGrid[tile].biome.GetModExtension<ModExt_Biome_Temperature>();
            if (extTemperature == null)
            {
                return;
            }
            __result = __result * (1f - extTemperature.tempStableWeight) + extTemperature.tempStableValue * extTemperature.tempStableWeight + extTemperature.tempOffset;
        }

    }

}
