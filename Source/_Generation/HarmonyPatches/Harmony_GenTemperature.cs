using Harmony;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(GenTemperature))]
    [HarmonyPatch("GetTemperatureFromSeasonAtTile")]
    public class Harmony_GenTemperature_GetTemperatureFromSeasonAtTile
    {

        public static void Postfix(ref float __result, int tile)
        {
            ModExt_Biome_Temperature extTemperature = Find.WorldGrid[tile].biome.GetModExtension<ModExt_Biome_Temperature>();
            if (extTemperature == null)
            {
                return;
            }
            __result = __result * (1f - extTemperature.tempStableWeight) + extTemperature.tempStableValue * extTemperature.tempStableWeight + extTemperature.tempOffset;
        }

    }

}
