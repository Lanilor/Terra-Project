using System;

namespace TerraCore
{

    public static class MoistureLevel
    {

        public static readonly float DiffThresholdMinMax = 0.01f;

        public static readonly float None = 0f;
        public static readonly float Min = 0.0001f;
        public static readonly float Max = 0.9999f;
        public static readonly float Unreached = 1f;

        public static readonly float BaseParched = 0.2f;
        public static readonly float BaseDry = 0.4f;
        public static readonly float BaseNormal = 0.6f;
        public static readonly float BaseWet = 0.8f;

        public static readonly float ThresholdMin = None;
        public static readonly float ThresholdParchedDry = 0.3f;
        public static readonly float ThresholdDryNormal = 0.5f;
        public static readonly float ThresholdNormalWet = 0.7f;
        public static readonly float ThresholdMax = Unreached;

        public static readonly float ParchedMin = ThresholdMin;
        public static readonly float ParchedMax = ThresholdParchedDry + DiffThresholdMinMax;
        public static readonly float DryMin = ThresholdParchedDry - DiffThresholdMinMax;
        public static readonly float DryMax = ThresholdDryNormal + DiffThresholdMinMax;
        public static readonly float NormalMin = ThresholdDryNormal - DiffThresholdMinMax;
        public static readonly float NormalMax = ThresholdNormalWet + DiffThresholdMinMax;
        public static readonly float WetMin = ThresholdNormalWet - DiffThresholdMinMax;
        public static readonly float WetMax = ThresholdMax;

        public static float ThresholdEnumToFloat(MoistureLevelMinMaxEnum e)
        {
            switch (e)
            {
                case MoistureLevelMinMaxEnum.AlwaysMin: return MoistureLevel.None;
                case MoistureLevelMinMaxEnum.ParchedMin: return MoistureLevel.ParchedMin;
                case MoistureLevelMinMaxEnum.ParchedMax: return MoistureLevel.ParchedMax;
                case MoistureLevelMinMaxEnum.DryMin: return MoistureLevel.DryMin;
                case MoistureLevelMinMaxEnum.DryMax: return MoistureLevel.DryMax;
                case MoistureLevelMinMaxEnum.NormalMin: return MoistureLevel.NormalMin;
                case MoistureLevelMinMaxEnum.NormalMax: return MoistureLevel.NormalMax;
                case MoistureLevelMinMaxEnum.WetMin: return MoistureLevel.WetMin;
                case MoistureLevelMinMaxEnum.WetMax: return MoistureLevel.WetMax;
                case MoistureLevelMinMaxEnum.AlwaysMax: return MoistureLevel.Unreached;
                default: return MoistureLevel.None;
            }
        }

        public static float EnumToFloat(MoistureLevelEnum e)
        {
            switch (e)
            {
                case MoistureLevelEnum.Parched: return MoistureLevel.BaseParched;
                case MoistureLevelEnum.Dry: return MoistureLevel.BaseDry;
                case MoistureLevelEnum.Normal: return MoistureLevel.BaseNormal;
                case MoistureLevelEnum.Wet: return MoistureLevel.BaseWet;
                default: return MoistureLevel.None;
            }
        }

        /*public static float MoistureEnumToFloatMin(MoistureLevelEnum e)
        {
            switch (e)
            {
                case MoistureLevelEnum.Parched: return MoistureLevel.ThresholdMin;
                case MoistureLevelEnum.Dry: return MoistureLevel.ThresholdParchedDry;
                case MoistureLevelEnum.Normal: return MoistureLevel.ThresholdDryNormal;
                case MoistureLevelEnum.Wet: return MoistureLevel.ThresholdNormalWet;
                default: return MoistureLevel.ThresholdMin;
            }
        }

        public static float MoistureEnumToFloatMax(MoistureLevelEnum e)
        {
            switch (e)
            {
                case MoistureLevelEnum.Parched: return MoistureLevel.ThresholdParchedDry;
                case MoistureLevelEnum.Dry: return MoistureLevel.ThresholdDryNormal;
                case MoistureLevelEnum.Normal: return MoistureLevel.ThresholdNormalWet;
                case MoistureLevelEnum.Wet: return MoistureLevel.ThresholdMax;
                default: return MoistureLevel.ThresholdMax;
            }
        }*/

    }
    
}
