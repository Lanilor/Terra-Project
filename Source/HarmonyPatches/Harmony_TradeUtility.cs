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

    [HarmonyPatch(typeof(TradeUtility))]
    [HarmonyPatch("SpawnDropPod")]
    public class Harmony_TradeUtility_SpawnDropPod
    {

        public static bool Prefix(IntVec3 dropSpot, Map map, Thing t)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRoof != RoofOverwriteType.FullStable)
            {
                return true;
            }
            // Spawn things from orbital trading directly on deep cave maps instead making a drop pod
            GenPlace.TryPlaceThing(t, dropSpot, map, ThingPlaceMode.Near);
            return false;
        }

    }

}
