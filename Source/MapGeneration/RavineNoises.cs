using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.Noise;
using RimWorld;

namespace TerraFW
{

	public static class RavineNoises
	{

		public static void WorkOnMapGenerator(Map map)
		{
            ModExt_Biome_GenStep_Ravine extRavine = map.Biome.GetModExtension<ModExt_Biome_GenStep_Ravine>();
            if (extRavine == null)
            {
                return;
            }

            string noiseLabel = "ravine " + map.Biome.defName;
            double xOffset = map.Size.x / 2;
            double zOffset = map.Size.z / 2;
            float baseWidth = Rand.Range(extRavine.ravineWidthMin, extRavine.ravineWidthMax);
            double modA = Rand.Range(extRavine.modAMin, extRavine.modAMax);
            double modB = Rand.Range(extRavine.modBMin, extRavine.modBMax);
            double modC = Rand.Range(extRavine.modCMin, extRavine.modCMax);
            // Base noise
            ModuleBase moduleBase = new DistFromAxis((float)map.Size.x * baseWidth);
            //NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base core");
            moduleBase = new CurveAxis(modA, modB, modC, moduleBase);
            //NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base curved");
            moduleBase = new ScaleBias(extRavine.slopeFactor, -1.0, moduleBase);
            //NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base scale");
            if (extRavine.invert)
            {
                moduleBase = new Invert(moduleBase);
            }
            //NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base invert");
            moduleBase = new Clamp(0.0, 999, moduleBase);
            //NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base clamp");
            NoiseDebugUI.StoreNoiseRender(moduleBase, noiseLabel + " base");
            // Add offset variance
            float offsetVariance = (Rand.Value - 0.5f) * extRavine.relativeOffsetVariance;
            xOffset += map.Size.x * offsetVariance;
            zOffset += map.Size.z * offsetVariance;
            // Add random rotation variance
            double rotVariance = (Rand.Value - 0.5f) * extRavine.rotationVariance;
            // Get overall ravine direction
            Rot4 random;
            do
            {
                random = Rot4.Random;
            }
            while (random == Find.World.CoastDirectionAt(map.Tile));
            if (random == Rot4.North || random == Rot4.South)
            {
                moduleBase = new Rotate(0.0, 90.0 + rotVariance, 0.0, moduleBase);
                moduleBase = new Translate(0.0, 0.0, (-zOffset), moduleBase);
            }
            else if (random == Rot4.East || random == Rot4.West)
            {
                moduleBase = new Rotate(0.0, 0.0 + rotVariance, 0.0, moduleBase);
                moduleBase = new Translate((-xOffset), 0.0, 0.0, moduleBase);
            }
            // Elevation noise scaling
            ModuleBase noiseElevation = new ScaleBias(extRavine.noiseElevationPreScale, extRavine.noiseElevationPreOffset, moduleBase);
            NoiseDebugUI.StoreNoiseRender(noiseElevation, noiseLabel + " elevation");
            // Fertility noise scaling
            ModuleBase noiseFertility = new ScaleBias(extRavine.noiseFertilityPreScale, extRavine.noiseFertilityPreOffset, moduleBase);
            NoiseDebugUI.StoreNoiseRender(noiseFertility, noiseLabel + " fertility");

            // Work on MapGenerator grid
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            MapGenFloatGrid fertility = MapGenerator.Fertility;
            foreach (IntVec3 cell in map.AllCells)
            {
                // Set elevation
                if (extRavine.calcElevationType == GenStepCalculationType.Set)
                {
                    elevation[cell] = noiseElevation.GetValue(cell) + extRavine.elevationPostOffset;
                }
                else if (extRavine.calcElevationType == GenStepCalculationType.Add)
                {
                    elevation[cell] += noiseElevation.GetValue(cell) + extRavine.elevationPostOffset;
                }
                // Set fertility
                if (extRavine.calcFertilityType == GenStepCalculationType.Set)
                {
                    fertility[cell] = noiseFertility.GetValue(cell) + extRavine.fertilityPostOffset;
                }
                else if (extRavine.calcFertilityType == GenStepCalculationType.Add)
                {
                    fertility[cell] += noiseFertility.GetValue(cell) + extRavine.fertilityPostOffset;
                }
            }
		}

    }

}
