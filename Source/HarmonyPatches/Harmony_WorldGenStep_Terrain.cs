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
using RimWorld.Planet;
using Verse.Noise;

namespace TerraFW
{

    [HarmonyPatch(typeof(WorldGenStep_Terrain))]
    [HarmonyPatch("GenerateTileFor")]
    public class Harmony_WorldGenStep_Terrain_GenerateTileFor
    {
        
        public static void Postfix(ref Tile __result)
        {
            GenWorldGen.UpdateTileByBiomeModExts(__result);
        }

    }

}
