using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(RiverMaker))]
    [HarmonyPatch("ValidatePassage")]
    public class Harmony_RiverMaker_ValidatePassage
    {

        public static void Postfix(object __instance, Map map)
        {
            IntVec3[] cardinalAdj = GenAdj.CardinalDirections;
            TerrainDef[] grid = map.terrainGrid.topGrid;
            for (int i = 0, iLen = grid.Count(); i < iLen; i++)
            {
                if (grid[i] == TerrainDefOf.WaterMovingChestDeep)
                {
                    IntVec3 currCell = map.cellIndices.IndexToCell(i);
                    for (int j = 0; j < 4; j++)
                    {
                        IntVec3 c = currCell + cardinalAdj[j];
                        if (c.InBounds(map) && map.terrainGrid.TerrainAt(c) == TerrainDefOf.WaterMovingShallow)
                        {
                            // TODO: Re-add deep water
                            //map.terrainGrid.SetTerrain(c, TerrainDefOf.WaterMovingSloping);
                            //map.terrainGrid.SetTerrain(currCell, TerrainDefOf.WaterMovingSloping);
                        }
                    }
                }
            }
        }

    }

}
