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

    [HarmonyPatch(typeof(RCellFinder))]
    [HarmonyPatch("TryFindRandomPawnEntryCell")]
    public class Harmony_RCellFinder_TryFindRandomPawnEntryCell
    {

        public static bool Prefix(ref bool __result, out IntVec3 result, Map map, float roadChance, Predicate<IntVec3> extraValidator = null)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.overwriteRoof != RoofOverwriteType.None)
            {
                __result = CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => c.Standable(map) && map.reachability.CanReachColony(c) && c.GetRoom(map, RegionType.Set_Passable).TouchesMapEdge && (extraValidator == null || extraValidator(c)), map, roadChance, out result);
                return false;
            }
            result = IntVec3.Invalid;
            return true;
        }

    }

}
