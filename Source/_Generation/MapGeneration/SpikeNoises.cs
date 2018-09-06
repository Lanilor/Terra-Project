using System;
using Verse;
using Verse.Noise;

namespace TerraCore
{

	public static class SpikeNoises
	{

		public static void WorkOnMapGenerator(Map map)
		{
            ModExt_Biome_GenStep_Spikes extSpikes = map.Biome.GetModExtension<ModExt_Biome_GenStep_Spikes>();
            if (extSpikes == null)
            {
                return;
            }

            string noiseLabel = "spikes " + map.Biome.defName;
            double xOffset = map.Size.x / 2;
            double zOffset = map.Size.z / 2;
            // Base noise
            ModuleBase moduleBase = new Perlin(extSpikes.baseFrequency, 2.0, 0.5, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base");
            // Elevation noise scaling
            ModuleBase noiseElevation = new ScaleBias(extSpikes.noiseElevationPreScale, extSpikes.noiseElevationPreOffset, moduleBase);
            NoiseDebugUI.StoreNoiseRender(noiseElevation, noiseLabel + " elevation");
            // Fertility noise scaling
            ModuleBase noiseFertility = new ScaleBias(extSpikes.noiseFertilityPreScale, extSpikes.noiseFertilityPreOffset, moduleBase);
            NoiseDebugUI.StoreNoiseRender(noiseFertility, noiseLabel + " fertility");

            // Work on MapGenerator grid
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            MapGenFloatGrid fertility = MapGenerator.Fertility;
            foreach (IntVec3 cell in map.AllCells)
            {
                // Calculate sine value for this cell
                float sin = (float)(Math.Sin((double)(cell.x - xOffset) / extSpikes.sineX) * Math.Sin((double)(cell.z - zOffset) / extSpikes.sineZ));
                sin = sin * extSpikes.sineScale + extSpikes.sineOffset;
                // Set elevation
                if (extSpikes.calcElevationType == GenStepCalculationType.Set)
                {
                    elevation[cell] = noiseElevation.GetValue(cell) * sin + extSpikes.elevationPostOffset;
                }
                else if (extSpikes.calcElevationType == GenStepCalculationType.Add)
                {
                    elevation[cell] += noiseElevation.GetValue(cell) * sin + extSpikes.elevationPostOffset;
                }
                // Set fertility
                if (extSpikes.calcFertilityType == GenStepCalculationType.Set)
                {
                    fertility[cell] = noiseFertility.GetValue(cell) * sin + extSpikes.fertilityPostOffset;
                }
                else if (extSpikes.calcFertilityType == GenStepCalculationType.Add)
                {
                    fertility[cell] += noiseFertility.GetValue(cell) * sin + extSpikes.fertilityPostOffset;
                }
            }
		}

    }

}
