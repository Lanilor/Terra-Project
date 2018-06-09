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

namespace TerraFW
{

    [HarmonyPatch(typeof(World))]
    [HarmonyPatch("CoastDirectionAt")]
    public class Harmony_World_CoastDirectionAt
    {

        public static bool Prefix(World __instance, ref Rot4 __result, int tileID)
        {
            Tile tileObj = __instance.grid[tileID];
            ModExt_Biome_FeatureControl extFtControl = tileObj.biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.removeBeach)
            {
                __result = Rot4.Invalid;
                return false;
            }
            return true;
        }

    }

    [HarmonyPatch(typeof(World))]
    [HarmonyPatch("HasCaves")]
    public class Harmony_World_HasCaves
    {
        
        public static bool Prefix(World __instance, ref bool __result, int tile)
        {
            BiomeDef biome = __instance.grid[tile].biome;
            // Set via modext caves
            ModExt_Biome_GenStep_BetterCaves extCaves = biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
            if (extCaves != null)
            {
                __result = true;
                return false;
            }
            // Set via modext feature control
            ModExt_Biome_FeatureControl extFtControl = biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null)
            {
                if (extFtControl.overwriteCaves == GenFeatureTristate.Add)
                {
                    __result = true;
                    return false;
                }
                if (extFtControl.overwriteCaves == GenFeatureTristate.Remove)
                {
                    __result = false;
                    return false;
                }
            }
            return true;
        }

    }

}
