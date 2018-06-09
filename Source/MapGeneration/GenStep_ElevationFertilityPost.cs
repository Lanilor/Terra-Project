using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.Noise;
using RimWorld;

namespace TerraFW
{

	public class GenStep_ElevationFertilityPost : GenStep
    {

        public override void Generate(Map map)
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
