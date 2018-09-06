using System.Collections.Generic;
using Verse;

namespace TerraCore
{

    public class ModExt_Terrain : DefModExtension
    {

        public float fertilityStorageFactor = 1f;
        public float moistureStorageFactor = 1f;

        public WaterLevelEnum waterByKey = WaterLevelEnum.None;
        public float water = -1f;

        public MoistureLevelEnum moistureByKey = MoistureLevelEnum.None;
        public float moisture = -1f;

        public TerrainDef dryTo = null;
        MoistureLevelMinMaxEnum dryAtByKey = MoistureLevelMinMaxEnum.AlwaysMin;
		public float dryAt = -1f;

        public TerrainDef wetTo = null;
        MoistureLevelMinMaxEnum wetAtByKey = MoistureLevelMinMaxEnum.AlwaysMax;
        public float wetAt = -1f;

        public TerrainDef parchTo = null;
        FertilityLevelMinMaxEnum parchAtByKey = FertilityLevelMinMaxEnum.AlwaysMin;
        public float parchAt = -1f;

        public TerrainDef enrichTo = null;
        FertilityLevelMinMaxEnum enrichAtByKey = FertilityLevelMinMaxEnum.AlwaysMax;
        public float enrichAt = -1f;

        public TerrainDef ebbTo = null;
        WaterLevelMinMaxEnum ebbAtByKey = WaterLevelMinMaxEnum.AlwaysMin;
        public float ebbAt = -1f;

        public TerrainDef floodTo = null;
        WaterLevelMinMaxEnum floodAtByKey = WaterLevelMinMaxEnum.AlwaysMax;
        public float floodAt = -1f;

		public TerrainDef freezeTo = null;
		public TerrainDef thawTo = null;
        public float freezeThawOffset = 0f;

        public TerrainDef destoneTo = null;

        public TerrainDef plowTo = null;
        public TerrainDef normalizeTo = null;
        public float normalizeAfterDays = 15f;

        public override IEnumerable<string> ConfigErrors()
        {
            ResolveReferences();
            yield break;
        }

        private void ResolveReferences()
        {
            if (water < 0f)
            {
                water = WaterLevel.EnumToFloat(waterByKey);
            }
            if (moisture < 0f)
            {
                moisture = MoistureLevel.EnumToFloat(moistureByKey);
            }
            if (dryAt < 0f)
            {
                dryAt = MoistureLevel.ThresholdEnumToFloat(dryAtByKey);
            }
            if (wetAt < 0f)
            {
                wetAt = MoistureLevel.ThresholdEnumToFloat(wetAtByKey);
            }
            if (parchAt < 0f)
            {
                parchAt = FertilityLevel.ThresholdEnumToFloat(parchAtByKey);
            }
            if (enrichAt < 0f)
            {
                enrichAt = FertilityLevel.ThresholdEnumToFloat(enrichAtByKey);
            }
            if (ebbAt < 0f)
            {
                ebbAt = WaterLevel.ThresholdEnumToFloat(ebbAtByKey);
            }
            if (floodAt < 0f)
            {
                floodAt = WaterLevel.ThresholdEnumToFloat(floodAtByKey);
            }
        }

    }
    
}
