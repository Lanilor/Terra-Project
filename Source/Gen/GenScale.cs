using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

	public static class GenScale
    {

        private const int normalMapWidth = 250;
        private const int normalMapHeight = 250;

        public static float OnMapSize(float value, Map map)
        {
            return value * map.Size.x * map.Size.y / normalMapWidth / normalMapHeight;
        }

        public static int OnMapSize(int value, Map map)
        {
            return GenMath.RoundRandom((float)value * map.Size.x * map.Size.y / normalMapWidth / normalMapHeight);
        }

    }

}
