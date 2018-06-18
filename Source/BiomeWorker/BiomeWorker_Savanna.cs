using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorker_Savanna : BiomeWorker
    {

        public override float GetScore(Tile tile, int tileID)
        {
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if (tile.temperature < 10f)
            {
                return 0f;
            }
            if (tile.rainfall < 500f || tile.rainfall >= 1400f)
            {
                return 0f;
            }
            return 17.7f + (tile.temperature - 13.5f) * 1.6f + (tile.rainfall - 600f) / 140f;
        }

    }

}
