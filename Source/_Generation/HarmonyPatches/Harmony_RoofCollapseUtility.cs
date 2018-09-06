using Harmony;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(RoofCollapseUtility))]
    [HarmonyPatch("WithinRangeOfRoofHolder")]
    public class Harmony_GenStep_RoofCollapseUtility_WithinRangeOfRoofHolder
    {
        
        public static bool Prefix(ref bool __result, IntVec3 c, Map map)
        {
            if (map.roofGrid.RoofAt(c) == RoofDefOf.RoofRockUncollapsable)
            {
                __result = true;
                return false;
            }
            return true;
        }

    }

}
