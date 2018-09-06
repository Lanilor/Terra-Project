using RimWorld;

namespace TerraCore
{

	public static class BiomeDefExtension
    {

        public static BiomeWorkerSpecial WorkerSpecial(this BiomeDef biome)
        {
            return biome.Worker as BiomeWorkerSpecial;
        }

    }

}
