using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(GenStep_FindPlayerStartSpot))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_FindPlayerStartSpot_Generate
    {

        private const int MaxDistanceToEdge = 6;
        private const int MinRoomCellCount = 30;
        
        public static bool Prefix(Map map)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRoof != RoofOverwriteType.FullStable)
            {
                return true;
            }
            DeepProfiler.Start("RebuildAllRegions");
            map.regionAndRoomUpdater.RebuildAllRegionsAndRooms();
            DeepProfiler.End();
            MapGenerator.PlayerStartSpot = CellFinderLoose.TryFindCentralCell(map, 7, MinRoomCellCount, (IntVec3 x) => x.CloseToEdge(map, MaxDistanceToEdge));
            return false;
        }

    }

}
