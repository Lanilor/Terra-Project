using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using UnityEngine;

namespace TerraFW
{

    [HarmonyPatch(typeof(RockNoises))]
    [HarmonyPatch("Init")]
    public class Harmony_RockNoises_Init
    {

        public static void Postfix(Map map)
        {
            IslandNoises.Init(map);
        }

    }

    [HarmonyPatch(typeof(RockNoises))]
    [HarmonyPatch("Reset")]
    class Harmony_RockNoises_Reset
    {

        public static void Postfix()
        {
            IslandNoises.Reset();
        }

    }

}
