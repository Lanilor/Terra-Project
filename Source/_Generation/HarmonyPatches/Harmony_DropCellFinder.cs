using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(DropCellFinder))]
    [HarmonyPatch("CanPhysicallyDropInto")]
    public class Harmony_DropCellFinder_CanPhysicallyDropInto
    {

        private const int MaxDistanceToEdge = 5;

        public static bool Prefix(ref bool __result, IntVec3 c, Map map, bool canRoofPunch)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRoof != RoofOverwriteType.FullStable)
            {
                return true;
            }
            if (!c.Walkable(map))
            {
                __result = false;
                return false;
            }
            if (c.CloseToEdge(map, MaxDistanceToEdge))
            {
                __result = true;
                return false;
            }
            RoofDef roof = c.GetRoof(map);
            if (roof != null && !canRoofPunch)
            {
                __result = false;
            }
            else
            {
                __result = true;
            }
            return false;
        }

    }

}
