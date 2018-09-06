using System;
using Harmony;
using RimWorld;
using Verse;

namespace TerraCore
{

    [HarmonyPatch(typeof(RCellFinder))]
    [HarmonyPatch("TryFindRandomPawnEntryCell")]
    public class Harmony_RCellFinder_TryFindRandomPawnEntryCell
    {

        public static bool Prefix(ref bool __result, out IntVec3 result, Map map, float roadChance, Predicate<IntVec3> extraValidator = null)
        {
            ModExt_Biome_FeatureControl extFtControl = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
            if (extFtControl == null || extFtControl.overwriteRoof != RoofOverwriteType.FullStable)
            {
                result = IntVec3.Invalid;
                return true;
            }
            __result = CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => c.Standable(map) && map.reachability.CanReachColony(c) && c.GetRoom(map, RegionType.Set_Passable).TouchesMapEdge && (extraValidator == null || extraValidator(c)), map, roadChance, out result);
            return false;
        }

    }

    /*public static bool TryFindRandomCellToPlantInFromOffMap(ThingDef plantDef, Map map, out IntVec3 plantCell)
		{
			Predicate<IntVec3> validator = delegate(IntVec3 c)
			{
				if (c.Roofed(map))
				{
					return false;
				}
				if (!plantDef.CanEverPlantAt(c, map))
				{
					return false;
				}
				Room room = c.GetRoom(map, RegionType.Set_Passable);
				return room != null && room.TouchesMapEdge;
			};
			return CellFinder.TryFindRandomEdgeCellWith(validator, map, CellFinder.EdgeRoadChance_Animal, out plantCell);
		}*/

    //TryFindRandomSpotJustOutsideColony(IntVec3 root, Map map, Pawn searcher, out IntVec3 result, Predicate<IntVec3> extraValidator = null)
    
    //TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(IntVec3 pos, Map map, float minDistToColony, out IntVec3 result)

}
