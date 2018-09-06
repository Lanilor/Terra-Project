using System;

namespace TerraCore
{

    public static class WaterLevel
    {

        public static readonly float DiffThresholdMinMax = 0.01f;

        public static readonly float None = 0f;
        public static readonly float Min = 0f;
        public static readonly float Max = 8f;

        public static readonly float BaseWet = 0.01f;
        public static readonly float BaseFlooded = 0.1f;
        public static readonly float BaseShallow = 0.35f;
        public static readonly float BaseHipDeep = 0.7f;
        public static readonly float BaseChestDeep = 1.15f;
        public static readonly float BaseSloping = 1.7f;
        public static readonly float BaseDeep = 3.00f;

        public static readonly float ThresholdMin = None;
        public static readonly float ThresholdWetFlooded = 0.02f;
        public static readonly float ThresholdFloodedShallow = 0.2f;
        public static readonly float ThresholdShallowHipDeep = 0.5f;
        public static readonly float ThresholdHipDeepChestDeep = 0.9f;
        public static readonly float ThresholdChestDeepSloping = 1.4f;
        public static readonly float ThresholdSlopingDeep = 2.0f;
        public static readonly float ThresholdMax = Max;

        public static readonly float WetMin = ThresholdMin;
        public static readonly float WetMax = ThresholdWetFlooded + DiffThresholdMinMax;
        public static readonly float FloodedMin = ThresholdWetFlooded - DiffThresholdMinMax;
        public static readonly float FloodedMax = ThresholdFloodedShallow + DiffThresholdMinMax;
        public static readonly float ShallowMin = ThresholdFloodedShallow - DiffThresholdMinMax;
        public static readonly float ShallowMax = ThresholdShallowHipDeep + DiffThresholdMinMax;
        public static readonly float HipDeepMin = ThresholdShallowHipDeep - DiffThresholdMinMax;
        public static readonly float HipDeepMax = ThresholdHipDeepChestDeep + DiffThresholdMinMax;
        public static readonly float ChestDeepMin = ThresholdHipDeepChestDeep - DiffThresholdMinMax;
        public static readonly float ChestDeepMax = ThresholdChestDeepSloping + DiffThresholdMinMax;
        public static readonly float SlopingMin = ThresholdChestDeepSloping - DiffThresholdMinMax;
        public static readonly float SlopingMax = ThresholdSlopingDeep + DiffThresholdMinMax;
        public static readonly float DeepMin = ThresholdSlopingDeep - DiffThresholdMinMax;
        public static readonly float DeepMax = ThresholdMax;

        public static float ThresholdEnumToFloat(WaterLevelMinMaxEnum e)
        {
            switch (e)
            {
                case WaterLevelMinMaxEnum.AlwaysMin: return WaterLevel.None;
                case WaterLevelMinMaxEnum.WetMin: return WaterLevel.WetMin;
                case WaterLevelMinMaxEnum.WetMax: return WaterLevel.WetMax;
                case WaterLevelMinMaxEnum.FloodedMin: return WaterLevel.FloodedMin;
                case WaterLevelMinMaxEnum.FloodedMax: return WaterLevel.FloodedMax;
                case WaterLevelMinMaxEnum.ShallowMin: return WaterLevel.ShallowMin;
                case WaterLevelMinMaxEnum.ShallowMax: return WaterLevel.ShallowMax;
                case WaterLevelMinMaxEnum.HipDeepMin: return WaterLevel.HipDeepMin;
                case WaterLevelMinMaxEnum.HipDeepMax: return WaterLevel.HipDeepMax;
                case WaterLevelMinMaxEnum.ChestDeepMin: return WaterLevel.ChestDeepMin;
                case WaterLevelMinMaxEnum.ChestDeepMax: return WaterLevel.ChestDeepMax;
                case WaterLevelMinMaxEnum.SlopingMin: return WaterLevel.SlopingMin;
                case WaterLevelMinMaxEnum.SlopingMax: return WaterLevel.SlopingMax;
                case WaterLevelMinMaxEnum.DeepMin: return WaterLevel.DeepMin;
                case WaterLevelMinMaxEnum.DeepMax: return WaterLevel.DeepMax;
                case WaterLevelMinMaxEnum.AlwaysMax: return WaterLevel.Max;
                default: return WaterLevel.None;
            }
        }

        public static float EnumToFloat(WaterLevelEnum e)
        {
            switch (e)
            {
                case WaterLevelEnum.Wet: return WaterLevel.BaseWet;
                case WaterLevelEnum.Flooded: return WaterLevel.BaseFlooded;
                case WaterLevelEnum.Shallow: return WaterLevel.BaseShallow;
                case WaterLevelEnum.HipDeep: return WaterLevel.BaseHipDeep;
                case WaterLevelEnum.ChestDeep: return WaterLevel.BaseChestDeep;
                case WaterLevelEnum.Sloping: return WaterLevel.BaseSloping;
                case WaterLevelEnum.Deep: return WaterLevel.BaseDeep;
                default: return WaterLevel.None;
            }
        }

        /*public static float WaterEnumToFloatMin(WaterLevelEnum e)
        {
            switch (e)
            {
                case WaterLevelEnum.Wet: return WaterLevel.ThresholdMin;
                case WaterLevelEnum.Flooded: return WaterLevel.ThresholdWetFlooded;
                case WaterLevelEnum.Shallow: return WaterLevel.ThresholdFloodedShallow;
                case WaterLevelEnum.HipDeep: return WaterLevel.ThresholdShallowHipDeep;
                case WaterLevelEnum.ChestDeep: return WaterLevel.ThresholdHipDeepChestDeep;
                case WaterLevelEnum.Sloping: return WaterLevel.ThresholdChestDeepSloping;
                case WaterLevelEnum.Deep: return WaterLevel.ThresholdSlopingDeep;
                default: return WaterLevel.ThresholdMin;
            }
        }

        public static float WaterEnumToFloatMax(WaterLevelEnum e)
        {
            switch (e)
            {
                case WaterLevelEnum.Wet: return WaterLevel.ThresholdWetFlooded;
                case WaterLevelEnum.Flooded: return WaterLevel.ThresholdFloodedShallow;
                case WaterLevelEnum.Shallow: return WaterLevel.ThresholdShallowHipDeep;
                case WaterLevelEnum.HipDeep: return WaterLevel.ThresholdHipDeepChestDeep;
                case WaterLevelEnum.ChestDeep: return WaterLevel.ThresholdChestDeepSloping;
                case WaterLevelEnum.Sloping: return WaterLevel.ThresholdSlopingDeep;
                case WaterLevelEnum.Deep: return WaterLevel.ThresholdMax;
                default: return WaterLevel.ThresholdMax;
            }
        }*/

    }
    
}
