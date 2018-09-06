using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(ScenPart_PlayerPawnsArriveMethod))]
    [HarmonyPatch("GenerateIntoMap")]
    public class Harmony_ScenPart_PlayerPawnsArriveMethod_GenerateIntoMap
    {

        public static void Prefix(ScenPart_PlayerPawnsArriveMethod __instance, Map map)
        {
            if (Find.GameInitData == null)
            {
                return;
            }
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRoof != RoofOverwriteType.FullStable)
            {
                return;
            }
            // Force standing spawn type if roof is full overwritten
            Traverse.Create(__instance).Field("method").SetValue(PlayerPawnsArriveMethod.Standing);
        }

    }

}
