using Harmony;
using Verse;

namespace TerraCore
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
    public class Harmony_RockNoises_Reset
    {

        public static void Postfix()
        {
            IslandNoises.Reset();
        }

    }

}
