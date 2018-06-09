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
using Verse.Noise;

namespace TerraFW
{

    public class Harmony_RiverMaker_ValidatePassage
    {

        static void Postfix(object __instance, Map map)
        {
            IntVec3[] cardinalAdj = GenAdj.CardinalDirections;
            TerrainDef[] grid = map.terrainGrid.topGrid;
            for (int i = 0, iLen = grid.Count(); i < iLen; i++)
            {
                if (grid[i] == TerrainDefOf.WaterMovingDeep)
                {
                    IntVec3 currCell = map.cellIndices.IndexToCell(i);
                    for (int j = 0; j < 4; j++)
                    {
                        IntVec3 c = currCell + cardinalAdj[j];
                        if (c.InBounds(map) && map.terrainGrid.TerrainAt(c) == TerrainDefOf.WaterMovingShallow)
                        {
                            map.terrainGrid.SetTerrain(c, TerrainDefOf.WaterMovingSloping);
                            map.terrainGrid.SetTerrain(currCell, TerrainDefOf.WaterMovingSloping);
                        }
                    }
                }
            }
        }

    }

}
