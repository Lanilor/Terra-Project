using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(GenStep_ScatterShrines))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_ScatterShrines_Generate
    {

        public static bool Prefix(Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.removeShrines)
            {
                return false;
            }
            return true;
        }

    }

}
