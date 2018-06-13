using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

    public class ModExt_Biome_GenStep_Ravine : DefModExtension
    {

        public float ravineWidthMin = 0.10f;
        public float ravineWidthMax = 0.20f;
        public float slopeFactor = 1.0f;
        public bool invert = false;
        public float relativeOffsetVariance = 0f;
        public float relativeOffsetFixed = 0f;
        public int rotationVariance = 0;

        public float modAMin = 0f;
        public float modAMax = 0f;
        public float modBMin = 0f;
        public float modBMax = 0f;
        public float modCMin = 0f;
        public float modCMax = 0f;

        public GenStepCalculationType calcElevationType = GenStepCalculationType.None;
        public float noiseElevationPreScale = 1.0f;
        public float noiseElevationPreOffset = 0f;
        public float elevationPostOffset = 0f;

        public GenStepCalculationType calcFertilityType = GenStepCalculationType.None;
        public float noiseFertilityPreScale = 1.0f;
        public float noiseFertilityPreOffset = 0f;
        public float fertilityPostOffset = 0f;

    }
    
}
