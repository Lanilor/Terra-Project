using Harmony;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(Room))]
    [HarmonyPatch("PsychologicallyOutdoors", PropertyMethod.Getter)]
    public class Harmony_Room_PsychologicallyOutdoors
    {

        public static bool Prefix(Room __instance, ref bool __result)
        {
            ModExt_Biome_FeatureControl extFtControl = __instance.Map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.roomCalculationType == RoomCalculationType.Default)
            {
                return true;
            }
            __result = __instance.OutdoorsByRCType(extFtControl.roomCalculationType);
            return false;
        }

    }

    [HarmonyPatch(typeof(Room))]
    [HarmonyPatch("OutdoorsForWork", PropertyMethod.Getter)]
    public class Harmony_Room_OutdoorsForWork
    {

        public static bool Prefix(Room __instance, ref bool __result)
        {
            ModExt_Biome_FeatureControl extFtControl = __instance.Map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.roomCalculationType == RoomCalculationType.Default)
            {
                return true;
            }
            __result = __instance.OutdoorsByRCType(extFtControl.roomCalculationType);
            return false;
        }

    }

}
