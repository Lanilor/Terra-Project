using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace TerraFW
{

    [StaticConstructorOnStartup]
    class Harmony
    {

        static Harmony()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.lanilor.terra");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
            harmony.Patch(AccessTools.Method(AccessTools.TypeByName("BeachMaker"), "BeachTerrainAt"), null, null, new HarmonyMethod(typeof(Harmony_BeachMaker_BeachTerrainAt), "Transpiler"));
            harmony.Patch(AccessTools.Method(AccessTools.TypeByName("CachedTileTemperatureData"), "CalculateOutdoorTemperatureAtTile"), null, new HarmonyMethod(typeof(Harmony_CachedTileTemperatureData_CalculateOutdoorTemperatureAtTile), "Postfix"));
            harmony.Patch(AccessTools.Method(AccessTools.TypeByName("RiverMaker"), "ValidatePassage"), null, new HarmonyMethod(typeof(Harmony_RiverMaker_ValidatePassage), "Postfix"));

            harmony.Patch(AccessTools.Method(typeof(WorldLayer_Hills).GetNestedTypes(BindingFlags.Instance | BindingFlags.NonPublic).First(), "MoveNext"), null, null, new HarmonyMethod(typeof(Harmony_WorldLayer_Hills_Regenerate), "Transpiler"));

        }

    }

}
