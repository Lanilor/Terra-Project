using Verse;

namespace TerraCore
{

	public static class MapExtension
    {

        private const int normalMapWidth = 250;
        private const int normalMapHeight = 250;

        public static float ScaleValueOnSize(this Map map, float value)
        {
            return value * map.Size.x * map.Size.y / normalMapWidth / normalMapHeight;
        }

        public static int ScaleValueOnSize(this Map map, int value)
        {
            return GenMath.RoundRandom((float)value * map.Size.x * map.Size.y / normalMapWidth / normalMapHeight);
        }

    }

}
