using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using UnityEngine;

namespace TerraFW
{

    [HarmonyPatch(typeof(GenStep_FindPlayerStartSpot))]
    [HarmonyPatch("Generate")]
    public class Harmony_GenStep_FindPlayerStartSpot_Generate
    {

        private const int MaxDistanceToEdge = 5;
        private const int MinRoomCellCount = 30;
        
        public static void Postfix(Map map)
        {
            ModExt_Biome_GenStep_BetterCaves extCaves = map.Biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
            if (extCaves == null)
            {
                return;
            }
            MapGenerator.PlayerStartSpot = CellFinderLoose.TryFindCentralCell(map, 7, MinRoomCellCount, (IntVec3 x) => x.CloseToEdge(map, MaxDistanceToEdge));
        }

    }

}
