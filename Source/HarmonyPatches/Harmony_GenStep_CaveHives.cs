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

    [HarmonyPatch(typeof(GenStep_CaveHives))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_CaveHives_Generate
    {

        public static void Postfix(GenStep_CaveHives __instance, Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && (extFtControl.overwriteHives == HiveOverwriteType.Add || extFtControl.overwriteHives == HiveOverwriteType.AddActive))
            {
                int hiveCount = GenScale.OnMapSize(extFtControl.additionalHivesScaling, map);
                Traverse trav = Traverse.Create(__instance).Method("TrySpawnHive", new object[] { map });
                for (int i = 0; i < hiveCount; i++)
                {
                    trav.GetValue();
                }
            }
        }

    }

    [HarmonyPatch(typeof(GenStep_CaveHives))]
    [HarmonyPatch("TrySpawnHive")]
    class Harmony_GenStep_CaveHives_TrySpawnHive
    {

        public static bool Prefix(GenStep_CaveHives __instance, Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.overwriteHives == HiveOverwriteType.Remove)
            {
                return false;
            }
            return true;
        }

        public static void Postfix(GenStep_CaveHives __instance, Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl != null && extFtControl.overwriteHives == HiveOverwriteType.AddActive)
            {
                List<Hive> spawnedHives = Traverse.Create(__instance).Field("spawnedHives").GetValue<List<Hive>>();
                Hive lastAddedHive = spawnedHives.Last();
                lastAddedHive.canSpawnPawns = true;
                lastAddedHive.GetComp<CompSpawnerHives>().canSpawnHives = true;
            }
        }

    }

}
