using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

	public static class GenRoof
    {

        private const int deepRoofDistance = 11;

        public static void SetRoofComplete(Map map, RoofDef type)
        {
            foreach (IntVec3 cell in map.AllCells)
            {
                map.roofGrid.SetRoof(cell, type);
            }
            map.roofGrid.RoofGridUpdate();
        }

        public static void SetStableDeepRoof(Map map)
        {
            List<IntVec3> unroofedCells = new List<IntVec3>();
            foreach (IntVec3 cell in map.AllCells)
            {
                if (!map.roofGrid.Roofed(cell))
                {
                    unroofedCells.Add(cell);
                }
            }
            GenMorphology.Dilate(unroofedCells, deepRoofDistance, map);
            foreach (IntVec3 cell in map.AllCells)
            {
                if (!unroofedCells.Contains(cell))
                {
                    map.roofGrid.SetRoof(cell, RoofDefOf.RoofRockUncollapsable);
                }
            }
            map.roofGrid.RoofGridUpdate();
        }

    }

}
