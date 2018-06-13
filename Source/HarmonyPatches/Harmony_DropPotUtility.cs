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

namespace TerraFW
{

    [HarmonyPatch(typeof(DropPodUtility))]
    [HarmonyPatch("DropThingGroupsNear")]
    public class Harmony_DropPodUtility_DropThingGroupsNear
    {

        private const int MaxDistanceToEdge = 6;

        public static void Prefix(IntVec3 dropCenter, Map map, ref bool instaDrop)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRoof != RoofOverwriteType.FullStable)
            {
                return;
            }
            // Set instaDrop to true if the drop pot should arrive near the map edge
            // This is mainly for event refugee and resource drops (which should maybe be deactivated at all on deep cave maps)
            if (dropCenter.CloseToEdge(map, MaxDistanceToEdge))
            {
                instaDrop = true;
            }
        }

    }

}
