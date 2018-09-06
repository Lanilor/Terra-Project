using System.Collections.Generic;
using Verse;

namespace TerraCore
{

    public class TerrainThresholdWEO
    {

        public TerrainDef terrain;
        public float min = -1000f;
        public float max = 1000f;
        public List<TerrainDef> excludeOverwrite;

        public static TerrainDef TerrainAtValue(List<TerrainThresholdWEO> threshes, float val, TerrainDef current)
        {
            if (threshes == null)
            {
                return null;
            }
            for (int i = 0; i < threshes.Count; i++)
            {
                if (threshes[i].min <= val && threshes[i].max >= val)
                {
                    if (threshes[i].excludeOverwrite != null && threshes[i].excludeOverwrite.Contains(current))
                    {
                        return null;
                    }
                    return threshes[i].terrain;
                }
            }
            return null;
        }

    }

}
