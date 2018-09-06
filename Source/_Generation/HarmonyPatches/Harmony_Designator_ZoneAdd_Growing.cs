using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Harmony;
using RimWorld;

namespace TerraCore
{

    [HarmonyPatch(typeof(Designator_ZoneAdd_Growing))]
    [HarmonyPatch("CanDesignateCell")]
    public class Harmony_Designator_ZoneAdd_Growing_CanDesignateCell
    {

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            for (int i = 0, iLen = instructions.Count(); i < iLen; i++)
            {
                CodeInstruction ci = instructions.ElementAt(i);
                if (ci.opcode == OpCodes.Ldsfld && instructions.ElementAt(i - 1).opcode == OpCodes.Callvirt && instructions.ElementAt(i + 3).opcode == OpCodes.Bge_Un)
                {
                    yield return new CodeInstruction(OpCodes.Ldc_R4, (object)0f);
                    yield return new CodeInstruction(OpCodes.Bgt_Un, instructions.ElementAt(i + 3).operand);
                    i += 3;
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
