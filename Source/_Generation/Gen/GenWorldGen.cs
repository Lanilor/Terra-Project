using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace TerraCore
{

	public static class GenWorldGen
    {

        public static void UpdateTileByBiomeModExts(Tile tile)
        {
            // Module replacement
            ModExt_Biome_Replacement extReplacement = tile.biome.GetModExtension<ModExt_Biome_Replacement>();
            if (extReplacement != null)
            {
                // Replace elevation
                if (extReplacement.replaceElevation)
                {
                    tile.elevation = Rand.RangeInclusive(extReplacement.elevationMin, extReplacement.elevationMax);
                }
                // Replace hilliness
                if (extReplacement.replaceHilliness != null)
                {
                    tile.hilliness = (Hilliness)extReplacement.replaceHilliness;
                }
            }
            // Module temperature
            ModExt_Biome_Temperature extTemperature = tile.biome.GetModExtension<ModExt_Biome_Temperature>();
            if (extTemperature != null)
            {
                // Adjust base temperature
                tile.temperature = tile.temperature * (1f - extTemperature.tempStableWeight) + extTemperature.tempStableValue * extTemperature.tempStableWeight + extTemperature.tempOffset;
            }
        }

        public static int NextRandomDigDir(int dir, int step)
        {
            step = Mathf.Clamp(step, 1, 3);
            dir += Rand.RangeInclusive(-step, step);
            if (dir < 0)
            {
                dir += 6;
            }
            if (dir > 5)
            {
                dir -= 6;
            }
            return dir;
        }

        public static int InvertDigDir(int dir)
        {
            dir = dir + 3;
            if (dir > 5)
            {
                dir -= 6;
            }
            return dir;
        }

    }

}
