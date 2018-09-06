using System.Collections.Generic;
using Verse;
using Verse.Noise;

namespace TerraCore
{

	public static class IslandNoises
    {

        private static ModExt_Biome_GenStep_Islands extIslands;
        private static ModuleBase noiseElevation;
        private static ModuleBase noiseFertility;
        private static List<IslandCone> islandCones;

        public static void Init(Map map)
        {
            ModExt_Biome_GenStep_Islands extIslands = map.Biome.GetModExtension<ModExt_Biome_GenStep_Islands>();
            if (extIslands == null)
            {
                return;
            }
            IslandNoises.extIslands = extIslands;

            string noiseLabel = "islands " + map.Biome.defName;
            // Base noise
            ModuleBase moduleBase = new Perlin(extIslands.baseFrequency, 2.0, 0.5, 5, Rand.Range(0, int.MaxValue), QualityMode.High);
            NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base");
            // Elevation noise scaling
            noiseElevation = new ScaleBias(extIslands.noiseElevationPreScale, extIslands.noiseElevationPreOffset, moduleBase);
            NoiseDebugUI.StoreNoiseRender(noiseElevation, noiseLabel + " elevation");
            // Fertility noise scaling
            noiseFertility = new ScaleBias(extIslands.noiseFertilityPreScale, extIslands.noiseFertilityPreOffset, moduleBase);
            NoiseDebugUI.StoreNoiseRender(noiseFertility, noiseLabel + " fertility");

            // Create island cones
            islandCones = new List<IslandCone>();
            int count = Rand.Range(extIslands.islandCountMin, extIslands.islandCountMax);
            for (int i = 0; i < count; i++)
            {
                IslandCone islandCone = new IslandCone();
                SetIslandOnRandomLocation(ref islandCone, map);
                islandCones.Add(islandCone);
            }
        }

        public static void Reset()
        {
            extIslands = null;
            noiseElevation = null;
            noiseFertility = null;
            islandCones = null;
        }
            
		public static void WorkOnMapGenerator(Map map)
		{
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            MapGenFloatGrid fertility = MapGenerator.Fertility;
            foreach (IntVec3 cell in map.AllCells)
            {
                // Set elevation
                if (extIslands.calcElevationType == GenStepCalculationType.Set)
                {
                    elevation[cell] = GetValueAt(cell, noiseElevation) * extIslands.elevationPostScale + extIslands.elevationPostOffset;
                }
                else if (extIslands.calcElevationType == GenStepCalculationType.Add)
                {
                    elevation[cell] += GetValueAt(cell, noiseElevation) * extIslands.elevationPostScale + extIslands.elevationPostOffset;
                }
                // Set fertility
                if (extIslands.calcFertilityType == GenStepCalculationType.Set)
                {
                    fertility[cell] = GetValueAt(cell, noiseFertility) * extIslands.fertilityPostScale + extIslands.fertilityPostOffset;
                }
                else if (extIslands.calcFertilityType == GenStepCalculationType.Add)
                {
                    fertility[cell] += GetValueAt(cell, noiseFertility) * extIslands.fertilityPostScale + extIslands.fertilityPostOffset;
                }
            }
		}

        public static TerrainDef TerrainAtFromTerrainPatchMakerByFertility(IntVec3 c, Map map, TerrainDef current)
        {
            if (extIslands.terrainPatchMakerByIslandFertility == null)
            {
                return null;
            }
            return TerrainThresholdWEO.TerrainAtValue(extIslands.terrainPatchMakerByIslandFertility, GetValueAt(c, noiseFertility), current);
        }

        private static void SetIslandOnRandomLocation(ref IslandCone islandNoise, Map map)
        {
            ModExt_Biome_GenStep_Islands extIslands = map.Biome.GetModExtension<ModExt_Biome_GenStep_Islands>();
            int minSizeX = (int)(map.Size.x * extIslands.minSizeX);
            int maxSizeX = (int)(map.Size.x * extIslands.maxSizeX);
            int minSizeZ = (int)(map.Size.z * extIslands.minSizeZ);
            int maxSizeZ = (int)(map.Size.z * extIslands.maxSizeZ);
            // Calculate coordinates
            int radiusX = Rand.Range(minSizeX, maxSizeX) / 2;
            int radiusZ = Rand.Range(minSizeZ, maxSizeZ) / 2;
            islandNoise.radiusX = radiusX;
            islandNoise.radiusZ = radiusZ;
            islandNoise.centerX = Rand.Range(radiusX + 5, map.Size.x - radiusX - 5);
            islandNoise.centerZ = Rand.Range(radiusZ + 5, map.Size.z - radiusZ - 5);
            islandNoise.distZFactor = (float)radiusX / radiusZ;
            islandNoise.radiusSquared = radiusX * radiusX;
        }

        private static float GetValueAt(IntVec3 cell, ModuleBase noise)
        {
            // Calculate value for this cell
            float val = -1000f;
            for (int i = 0; i < islandCones.Count; i++)
            {
                // Calculate value
                IslandCone cone = islandCones[i];
                float distX = cone.centerX - cell.x;
                float distZ = cone.distZFactor * (cone.centerZ - cell.z);
                float distSquared = distX * distX + distZ * distZ;
                float value = GenMath.LerpDouble(0f, cone.radiusSquared, extIslands.centerHeightValue, 0f, distSquared);
                // Kepp the highest value
                if (value > val)
                {
                    val = value;
                }
            }
            val += noise.GetValue(cell);
            // Invert values over threshold
            if (val > extIslands.invertOver)
            {
                val -= (val - extIslands.invertOver) * 2;
            }
            // Invert values under threshold
            if (val < extIslands.invertUnder)
            {
                val += (extIslands.invertUnder - val) * 2;
                // Cut too large values
                if (val > extIslands.centerHeightValue)
                {
                    val = extIslands.centerHeightValue;
                }
            }
            // Cut too small values
            if (val < 0f)
            {
                val = 0f;
            }
            // Invert all
            if (extIslands.invertSwap)
            {
                val = extIslands.centerHeightValue - val;
            }
            return val;
        }

        public class IslandCone
        {
            public int centerX;
            public int centerZ;
            public int radiusX;
            public int radiusZ;
            public float distZFactor;
            public int radiusSquared;
        }

    }

}
