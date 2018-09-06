using System.Collections.Generic;
using RimWorld;
using Verse;

namespace TerraCore
{

    public class ScattererValidator_ExcludeBiomes : ScattererValidator
	{

        public List<BiomeDef> biomes;
		
		public override bool Allows(IntVec3 c, Map map)
		{
            BiomeDef mapBiome = map.Biome;
            foreach (BiomeDef b in biomes)
            {
                if (mapBiome == b)
                {
                    return false;
                }
            }
            return true;
		}

	}

}
