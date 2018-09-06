using Harmony;
using RimWorld.Planet;

namespace TerraCore
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
