using RimWorld;
using Verse;

namespace TerraCore
{

	public class GenStep_ElevationFertilityPost : GenStep
    {

        public override int SeedPart
        {
            get
            {
                return 220686156;
            }
        }

        public override void Generate(Map map, GenStepParams parms)
        {
            BiomeDef biome = map.Biome;
            // Add GenStep Spikes
            if (biome.HasModExtension<ModExt_Biome_GenStep_Spikes>())
            {
                SpikeNoises.WorkOnMapGenerator(map);
            }
            // Add GenStep Islands
            if (biome.HasModExtension<ModExt_Biome_GenStep_Islands>())
            {
                IslandNoises.WorkOnMapGenerator(map);
            }
            // Add GenStep Ravine
            if (biome.HasModExtension<ModExt_Biome_GenStep_Ravine>())
            {
                RavineNoises.WorkOnMapGenerator(map);
            }
        }

    }

}
