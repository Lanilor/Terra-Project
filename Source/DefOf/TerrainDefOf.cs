using RimWorld;
using Verse;

namespace TerraCore
{

    [DefOf]
    public static class TerrainDefOf
    {

        static TerrainDefOf()
		{
            DefOfHelper.EnsureInitializedInCtor(typeof(TerrainDefOf));
		}

        // Gravel

        public static TerrainDef ParchedBarrenGravel;
        public static TerrainDef DryBarrenGravel;
        public static TerrainDef NormalBarrenGravel;
        public static TerrainDef WetBarrenGravel;
        public static TerrainDef FloodedBarrenGravel;
        public static TerrainDef FrozenFloodedBarrenGravel;

        public static TerrainDef ParchedGravel;
        public static TerrainDef DryGravel;
        public static TerrainDef NormalGravel;
        public static TerrainDef WetGravel;
        public static TerrainDef FloodedGravel;
        public static TerrainDef FrozenFloodedGravel;

        public static TerrainDef ParchedFertileGravel;
        public static TerrainDef DryFertileGravel;
        public static TerrainDef NormalFertileGravel;
        public static TerrainDef WetFertileGravel;
        public static TerrainDef FloodedFertileGravel;
        public static TerrainDef FrozenFloodedFertileGravel;

        // Normal soil

        public static TerrainDef ParchedSand;
        public static TerrainDef DrySand;
        public static TerrainDef NormalSand;
        public static TerrainDef WetSand;
        public static TerrainDef FloodedSand;
        public static TerrainDef FrozenFloodedSand;

        public static TerrainDef ParchedLaterite;
        public static TerrainDef DryLaterite;
        public static TerrainDef NormalLaterite;
        public static TerrainDef WetLaterite;
        public static TerrainDef FloodedLaterite;
        public static TerrainDef FrozenFloodedLaterite;

        public static TerrainDef ParchedBarrenSoil;
        public static TerrainDef DryBarrenSoil;
        public static TerrainDef NormalBarrenSoil;
        public static TerrainDef WetBarrenSoil;
        public static TerrainDef FloodedBarrenSoil;
        public static TerrainDef FrozenFloodedBarrenSoil;

        public static TerrainDef ParchedSoil;
        public static TerrainDef DrySoil;
        public static TerrainDef NormalSoil;
        public static TerrainDef WetSoil;
        public static TerrainDef FloodedSoil;
        public static TerrainDef FrozenFloodedSoil;

        public static TerrainDef ParchedFertileSoil;
        public static TerrainDef DryFertileSoil;
        public static TerrainDef NormalFertileSoil;
        public static TerrainDef WetFertileSoil;
        public static TerrainDef FloodedFertileSoil;
        public static TerrainDef FrozenFloodedFertileSoil;

        public static TerrainDef ParchedRichSoil;
        public static TerrainDef DryRichSoil;
        public static TerrainDef NormalRichSoil;
        public static TerrainDef WetRichSoil;
        public static TerrainDef FloodedRichSoil;
        public static TerrainDef FrozenFloodedRichSoil;

        // Plown soil

        public static TerrainDef ParchedSandPlown;
        public static TerrainDef DrySandPlown;
        public static TerrainDef NormalSandPlown;
        public static TerrainDef WetSandPlown;

        public static TerrainDef ParchedLateritePlown;
        public static TerrainDef DryLateritePlown;
        public static TerrainDef NormalLateritePlown;
        public static TerrainDef WetLateritePlown;

        public static TerrainDef ParchedBarrenSoilPlown;
        public static TerrainDef DryBarrenSoilPlown;
        public static TerrainDef NormalBarrenSoilPlown;
        public static TerrainDef WetBarrenSoilPlown;

        public static TerrainDef ParchedSoilPlown;
        public static TerrainDef DrySoilPlown;
        public static TerrainDef NormalSoilPlown;
        public static TerrainDef WetSoilPlown;

        public static TerrainDef ParchedFertileSoilPlown;
        public static TerrainDef DryFertileSoilPlown;
        public static TerrainDef NormalFertileSoilPlown;
        public static TerrainDef WetFertileSoilPlown;

        public static TerrainDef ParchedRichSoilPlown;
        public static TerrainDef DryRichSoilPlown;
        public static TerrainDef NormalRichSoilPlown;
        public static TerrainDef WetRichSoilPlown;

        // Water

        public static TerrainDef WaterDeep;
        public static TerrainDef WaterSloping;
        public static TerrainDef WaterChestDeep;
        public static TerrainDef WaterHipDeep;
        public static TerrainDef WaterShallow;

        public static TerrainDef WaterOceanDeep;
        public static TerrainDef WaterOceanSloping;
        public static TerrainDef WaterOceanChestDeep;
        public static TerrainDef WaterOceanHipDeep;
        public static TerrainDef WaterOceanShallow;

        public static TerrainDef WaterMovingDeep;
        public static TerrainDef WaterMovingSloping;
        public static TerrainDef WaterMovingChestDeep;
        public static TerrainDef WaterMovingHipDeep;
        public static TerrainDef WaterMovingShallow;

        // Ice

        public static TerrainDef IceDeep;
        public static TerrainDef IceSloping;
        public static TerrainDef IceChestDeep;
        public static TerrainDef IceHipDeep;
        public static TerrainDef IceShallow;

        public static TerrainDef IceOceanDeep;
        public static TerrainDef IceOceanSloping;
        public static TerrainDef IceOceanChestDeep;
        public static TerrainDef IceOceanHipDeep;
        public static TerrainDef IceOceanShallow;

        public static TerrainDef IceMovingDeep;
        public static TerrainDef IceMovingSloping;
        public static TerrainDef IceMovingChestDeep;
        public static TerrainDef IceMovingHipDeep;
        public static TerrainDef IceMovingShallow;

        // Stone

        public static TerrainDef Sandstone_Rough;
        public static TerrainDef Granite_Rough;
        public static TerrainDef Limestone_Rough;
        public static TerrainDef Slate_Rough;
        public static TerrainDef Marble_Rough;

        public static TerrainDef Sandstone_Smooth;
        public static TerrainDef Granite_Smooth;
        public static TerrainDef Limestone_Smooth;
        public static TerrainDef Slate_Smooth;
        public static TerrainDef Marble_Smooth;

        public static TerrainDef FloodedSandstone;
        public static TerrainDef FrozenFloodedSandstone;

        public static TerrainDef FloodedGranite;
        public static TerrainDef FrozenFloodedGranite;

        public static TerrainDef FloodedLimestone;
        public static TerrainDef FrozenFloodedLimestone;

        public static TerrainDef FloodedSlate;
        public static TerrainDef FrozenFloodedSlate;

        public static TerrainDef FloodedMarble;
        public static TerrainDef FrozenFloodedMarble;

        // Various

        public static TerrainDef FloodedTerrain;

        // Filler

        public static TerrainDef FillerTerrain;
        public static TerrainDef FillerFloodedTerrain;
        public static TerrainDef FillerStone;
        public static TerrainDef FillerWater;

    }
}
