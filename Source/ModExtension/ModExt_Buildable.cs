using System.Collections.Generic;
using Verse;

namespace TerraCore
{

    public class ModExt_Buildable : DefModExtension
    {

        public MoistureLevelEnum moistureByKey = MoistureLevelEnum.None;
        public float moisture = -1f;
        public WaterLevelEnum waterByKey = WaterLevelEnum.None;
        public float water = -1f;

        public override IEnumerable<string> ConfigErrors()
        {
            ResolveReferences();
            yield break;
        }

        private void ResolveReferences()
        {
            if (moisture < 0f)
            {
                moisture = MoistureLevel.EnumToFloat(moistureByKey);
            }
            if (water < 0f)
            {
                water = WaterLevel.EnumToFloat(waterByKey);
            }
        }

    }
    
}
