using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(GenStep_ScatterRuinsSimple))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_ScatterRuinsSimple_Generate
    {

        public static bool Prefix(Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.removeRuinsSimple)
            {
                return false;
            }
            return true;
        }

    }

}
