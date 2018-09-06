using Verse;

namespace TerraCore
{

    public class ModExt_Biome_GenStep_Spikes : DefModExtension
    {

        public float baseFrequency = 0.0647f;

        public float sineX = 5f;
        public float sineZ = 5f;
        public float sineScale = 1.0f;
        public float sineOffset = 0f;

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
