using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorker_Oasis : BiomeWorker
    {

        public override float GetScore(Tile tile)
        {
            if (tile.WaterCovered || Rand.Value > 0.003f)
            {
                return -100f;
            }
            if (tile.temperature < 15f)
            {
                return 0f;
            }
            if (tile.rainfall >= 600f)
            {
                return 0f;
            }
            if (tile.swampiness >= 0.5f)
            {
                return 0f;
            }
            return tile.temperature + 100f;
        }

    }

}
