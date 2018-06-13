using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace TerraFW
{

	public static class BiomeDefExtension
    {

        public static BiomeWorkerSpecial WorkerSpecial(this BiomeDef biome)
        {
            return biome.Worker as BiomeWorkerSpecial;
        }

    }

}
