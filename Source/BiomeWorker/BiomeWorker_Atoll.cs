using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorker_Atoll : BiomeWorker
    {

        public override float GetScore(Tile tile)
        {
            if (!tile.WaterCovered || tile.elevation > -20f || Rand.Value > 0.0004f)
            {
                return -100f;
            }
            if (tile.temperature < 20f)
            {
                return 0f;
            }
            return 25f;
        }

    }

}
