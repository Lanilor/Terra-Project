using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;

namespace TerraFW
{

    public class BiomeWorker_Empty : BiomeWorker
    {

        public override float GetScore(Tile tile)
        {
            return -100f;
        }

    }

}
