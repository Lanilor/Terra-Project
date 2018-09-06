using System.Linq;
using System.Reflection;
using Harmony;
using RimWorld.Planet;
using Verse;

namespace TerraCore
{

    [StaticConstructorOnStartup]
    class Harmony
    {

        static Harmony()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.lanilor.terracore");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
            harmony.Patch(AccessTools.Method(AccessTools.TypeByName("CachedTileTemperatureData"), "CalculateOutdoorTemperatureAtTile"), null, new HarmonyMethod(typeof(Harmony_CachedTileTemperatureData_CalculateOutdoorTemperatureAtTile), "Postfix"));
            
            harmony.Patch(AccessTools.Method(typeof(WorldLayer_Hills).GetNestedTypes(BindingFlags.Instance | BindingFlags.NonPublic).First(), "MoveNext"), null, null, new HarmonyMethod(typeof(Harmony_WorldLayer_Hills_Regenerate), "Transpiler"));
        }

    }

}
