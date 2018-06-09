using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorker_TundraSkerries : BiomeWorker
    {

        public override float GetScore(Tile tile)
        {
            if (!tile.WaterCovered || tile.elevation > -20f || tile.elevation < -120f || Rand.Value > 0.002f)
            {
                return -100f;
            }
            if (tile.temperature > 8f)
            {
                return 0f;
            }
            return 20f - BiomeWorker_IceSheet.PermaIceScore(tile);
        }

    }

}
