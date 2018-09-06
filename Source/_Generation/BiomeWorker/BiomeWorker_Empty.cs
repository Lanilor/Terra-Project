using RimWorld;
using RimWorld.Planet;

namespace TerraCore
{

    public class BiomeWorker_Empty : BiomeWorker
    {

        public override float GetScore(Tile tile, int tileID)
        {
            return -100f;
        }

    }

}
