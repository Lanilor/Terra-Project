using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorker_VolcanicIsland : BiomeWorker
    {

        public override float GetScore(Tile tile)
        {
            if (!tile.WaterCovered || tile.elevation > -20f || Rand.Value > 0.0003f)
            {
                return -100f;
            }
            if (tile.temperature < 0f || tile.temperature >= 25f)
            {
                return 0f;
            }
            return 24f;
        }

    }

}
