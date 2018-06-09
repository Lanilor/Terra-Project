using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorker_SpikeMountains : BiomeWorker
    {

        public override float GetScore(Tile tile)
        {
            if (tile.WaterCovered || Rand.Value > 0.001f)
            {
                return -100f;
            }
            if (tile.temperature < -10f)
            {
                return 0f;
            }
            
            if (tile.rainfall < 1200f)
            {
                return 0f;
            }
            return 50f + (tile.temperature - 7f) + (tile.rainfall - 600f) / 180f;
        }

    }

}
