using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Harmony;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace TerraCore
{

    [HarmonyPatch(typeof(BeachMaker))]
    [HarmonyPatch("BeachTerrainAt")]
    public class Harmony_BeachMaker_BeachTerrainAt
    {

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            for (int i = 0, iLen = instructions.Count(); i < iLen; i++)
            {
                CodeInstruction ci = instructions.ElementAt(i);
                if (ci.opcode == OpCodes.Ldc_R4 && ci.operand.GetType() == typeof(Single) && (Single)ci.operand == (Single)0.45f)
                {
                    Label jumpTarget = il.DefineLabel();
                    yield return new CodeInstruction(OpCodes.Ldc_R4, (object)0.25f);
                    yield return new CodeInstruction(OpCodes.Bge_Un, jumpTarget);
                    yield return new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(TerrainDefOf), "WaterOceanChestDeep"));
                    yield return new CodeInstruction(OpCodes.Ret);
                    yield return new CodeInstruction(OpCodes.Ldloc_0) { labels = new List<Label>() { jumpTarget } };
                }
                yield return ci;
            }
            yield break;
        }

    }

}
