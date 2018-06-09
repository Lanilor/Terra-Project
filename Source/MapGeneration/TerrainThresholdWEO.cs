using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

    public class TerrainThresholdWEO
    {

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

        TerrainDef terrain;
        float min = -1000f;
        float max = 1000f;
        List<TerrainDef> excludeOverwrite;

    }

}
