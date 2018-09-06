using System;

namespace TerraCore
{

    public static class FertilityLevel
    {

        public static readonly float DiffThresholdMinMax = 0.02f;

        public static readonly float None = 0f;
        public static readonly float Min = 0.0001f;
        public static readonly float Max = 1.9999f;
        public static readonly float Unreached = 2f;

        public static readonly float BaseSand = 0.10f;
        public static readonly float BaseLaterite = 0.45f;
        public static readonly float BaseBarren = 0.75f;
        public static readonly float BaseNormal = 1.0f;
        public static readonly float BaseFertile = 1.2f;
        public static readonly float BaseRich = 1.35f;

        public static readonly float ThresholdMin = None;
        public static readonly float ThresholdSandLaterite = 0.30f;
        public static readonly float ThresholdLateriteBarren = 0.60f;
        public static readonly float ThresholdBarrenNormal = 0.90f;
        public static readonly float ThresholdNormalFertile = 1.10f;
        public static readonly float ThresholdFertileRich = 1.25f;
        public static readonly float ThresholdMax = Unreached;

        public static readonly float SandMin = ThresholdMin;
        public static readonly float SandMax = ThresholdSandLaterite + DiffThresholdMinMax;
        public static readonly float LateriteMin = ThresholdSandLaterite - DiffThresholdMinMax;
        public static readonly float LateriteMax = ThresholdLateriteBarren + DiffThresholdMinMax;
        public static readonly float BarrenMin = ThresholdLateriteBarren - DiffThresholdMinMax;
        public static readonly float BarrenMax = ThresholdBarrenNormal + DiffThresholdMinMax;
        public static readonly float NormalMin = ThresholdBarrenNormal - DiffThresholdMinMax;
        public static readonly float NormalMax = ThresholdNormalFertile + DiffThresholdMinMax;
        public static readonly float FertileMin = ThresholdNormalFertile - DiffThresholdMinMax;
        public static readonly float FertileMax = ThresholdFertileRich + DiffThresholdMinMax;
        public static readonly float RichMin = ThresholdFertileRich - DiffThresholdMinMax;
        public static readonly float RichMax = ThresholdMax;

        public static float ThresholdEnumToFloat(FertilityLevelMinMaxEnum e)
        {
            switch (e)
            {
                case FertilityLevelMinMaxEnum.AlwaysMin: return FertilityLevel.None;
                case FertilityLevelMinMaxEnum.SandMin: return FertilityLevel.SandMin;
                case FertilityLevelMinMaxEnum.SandMax: return FertilityLevel.SandMax;
                case FertilityLevelMinMaxEnum.LateriteMin: return FertilityLevel.LateriteMin;
                case FertilityLevelMinMaxEnum.LateriteMax: return FertilityLevel.LateriteMax;
                case FertilityLevelMinMaxEnum.BarrenMin: return FertilityLevel.BarrenMin;
                case FertilityLevelMinMaxEnum.BarrenMax: return FertilityLevel.BarrenMax;
                case FertilityLevelMinMaxEnum.NormalMin: return FertilityLevel.NormalMin;
                case FertilityLevelMinMaxEnum.NormalMax: return FertilityLevel.NormalMax;
                case FertilityLevelMinMaxEnum.FertileMin: return FertilityLevel.FertileMin;
                case FertilityLevelMinMaxEnum.FertileMax: return FertilityLevel.FertileMax;
                case FertilityLevelMinMaxEnum.RichMin: return FertilityLevel.RichMin;
                case FertilityLevelMinMaxEnum.RichMax: return FertilityLevel.RichMax;
                case FertilityLevelMinMaxEnum.AlwaysMax: return FertilityLevel.Unreached;
                default: return FertilityLevel.None;
            }
        }

        public static float FertilityEnumToFloat(FertilityLevelEnum e)
        {
            switch (e)
            {
                case FertilityLevelEnum.Sand: return FertilityLevel.BaseSand;
                case FertilityLevelEnum.Laterite: return FertilityLevel.BaseLaterite;
                case FertilityLevelEnum.Barren: return FertilityLevel.BaseBarren;
                case FertilityLevelEnum.Normal: return FertilityLevel.BaseNormal;
                case FertilityLevelEnum.Fertile: return FertilityLevel.BaseFertile;
                case FertilityLevelEnum.Rich: return FertilityLevel.BaseRich;
                default: return FertilityLevel.None;
            }
        }

        /*public static float FertilityEnumToFloatMin(FertilityLevelEnum e)
        {
            switch (e)
            {
                case FertilityLevelEnum.Sand: return FertilityLevel.ThresholdMin;
                case FertilityLevelEnum.Laterite: return FertilityLevel.ThresholdSandLaterite;
                case FertilityLevelEnum.Barren: return FertilityLevel.ThresholdLateriteBarren;
                case FertilityLevelEnum.Normal: return FertilityLevel.ThresholdBarrenNormal;
                case FertilityLevelEnum.Fertile: return FertilityLevel.ThresholdNormalFertile;
                case FertilityLevelEnum.Rich: return FertilityLevel.ThresholdFertileRich;
                default: return FertilityLevel.ThresholdMin;
            }
        }

        public static float FertilityEnumToFloatMax(FertilityLevelEnum e)
        {
            switch (e)
            {
                case FertilityLevelEnum.Sand: return FertilityLevel.ThresholdSandLaterite;
                case FertilityLevelEnum.Laterite: return FertilityLevel.ThresholdLateriteBarren;
                case FertilityLevelEnum.Barren: return FertilityLevel.ThresholdBarrenNormal;
                case FertilityLevelEnum.Normal: return FertilityLevel.ThresholdNormalFertile;
                case FertilityLevelEnum.Fertile: return FertilityLevel.ThresholdFertileRich;
                case FertilityLevelEnum.Rich: return FertilityLevel.ThresholdMax;
                default: return FertilityLevel.ThresholdMax;
            }
        }*/

    }
    
}
