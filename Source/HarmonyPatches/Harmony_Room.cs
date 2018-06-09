using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;

namespace TerraFW
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
