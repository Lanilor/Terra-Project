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

    public class Harmony_WorldLayer_Hills_Regenerate
    {

        private static bool CheckSkipHelper(Tile tile)
        {
            if (tile.hilliness == Hilliness.Impassable)
            {
                return true;
            }
            if (tile.biome.WorkerSpecial() != null || tile.biome == BiomeDefOf.CaveEntrance || tile.biome == BiomeDefOf.TunnelworldCave)
            {
                return true;
            }
            return false;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            Label? jumpTarget = null;
            for (int i = 0, iLen = instructions.Count(); i < iLen; i++)
            {
                CodeInstruction ci = instructions.ElementAt(i);
                if (ci.opcode == OpCodes.Ldloc_3 && instructions.ElementAt(i + 1).opcode == OpCodes.Ldc_I4_1 && instructions.ElementAt(i + 2).opcode == OpCodes.Add && instructions.ElementAt(i + 3).opcode == OpCodes.Stloc_3)
                {
                    jumpTarget = ci.labels[0];
                    break;
                }
            }
            if (jumpTarget == null) {
                Log.Error("Transpiler Harmony_WorldLayer_Hills_Regenerate could not find needed jumpTarget.");
                yield break;
            }
            for (int i = 0, iLen = instructions.Count(); i < iLen; i++)
            {
                CodeInstruction ci = instructions.ElementAt(i);
                if (ci.opcode == OpCodes.Stloc_S && instructions.ElementAt(i - 1).opcode == OpCodes.Callvirt && instructions.ElementAt(i - 2).opcode == OpCodes.Ldloc_3 && instructions.ElementAt(i - 3).opcode == OpCodes.Ldfld && instructions.ElementAt(i - 4).opcode == OpCodes.Ldarg_0)
                {
                    yield return ci;
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 4);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Harmony_WorldLayer_Hills_Regenerate), "CheckSkipHelper"));
                    yield return new CodeInstruction(OpCodes.Brtrue, jumpTarget.Value);
                }
                else
                {
                    yield return ci;
                }
            }
            yield break;
        }

    }

}
