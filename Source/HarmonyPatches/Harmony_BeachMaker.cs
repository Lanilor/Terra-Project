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
                    yield return new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(TerrainDefOf), "WaterOceanSloping"));
                    yield return new CodeInstruction(OpCodes.Stloc_0);
                    yield return new CodeInstruction(OpCodes.Br, instructions.ElementAt(i - 2).operand);
                    yield return new CodeInstruction(OpCodes.Ldloc_1) { labels = new List<Label>() { jumpTarget } };
                }
                /*if (ci.opcode == OpCodes.Ldc_R4 && ci.operand.GetType() == typeof(Single) && (Single)ci.operand == (Single)1.0f)
                {
                }*/
                yield return ci;
            }
            yield break;
        }

    }

}
