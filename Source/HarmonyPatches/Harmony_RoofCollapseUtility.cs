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
using Verse.Noise;

namespace TerraFW
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
