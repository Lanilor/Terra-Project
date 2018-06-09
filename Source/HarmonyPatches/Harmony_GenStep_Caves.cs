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

    [HarmonyPatch(typeof(GenStep_Caves))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_Caves_Generate
    {

        public static bool Prefix(Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.overwriteCaves == GenFeatureTristate.Remove)
            {
                return false;
            }
            return true;
        }

    }

}
