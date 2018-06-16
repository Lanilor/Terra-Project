using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorkerSpecial_Atoll : BiomeWorkerSpecial
    {

        public static readonly FloatRange WLSizeFactor = new FloatRange(0.95f, 1.25f);
        public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.02f);

        private const int BiomeChangeDigLengthMin = 5;
        private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(10, 20);
        private const float BiomeChangeChance = 0.3f;

        protected override float InitialGenChance
        {
            get { return 0.06f; }
        }

        protected override float GenChanceOffsetAfterFirstHit
        {
            get { return 0.055f; }
        }

        protected override float GenChancePerHitFactor
        {
            get { return 0.38f; }
        }

        public override bool MinPreRequirements(Tile tile)
        {
            return tile.WaterCovered;
        }

        public override bool PreRequirements(Tile tile)
        {
            if (!tile.WaterCovered || tile.elevation > -90f)
            {
                return false;
            }
            if (tile.temperature < 20f)
            {
                return false;
            }
            return true;
        }

        public override void PostGeneration(int tileID)
        {
            int digLengthMax = BiomeChangeDigLengthMax.RandomInRange;
            DigTilesForBiomeChange(tileID, BiomeChangeDigLengthMin, digLengthMax, 1);
        }

        protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
        {
            if (Rand.Value < BiomeChangeChance)
            {
                tile.biome = BiomeDefOf.Atoll;
                GenWorldGen.UpdateTileByBiomeModExts(tile);
            }
        }

        public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
        {
            return new WLTileGraphicData(WorldMaterials.Atoll, WLSizeFactor, WLPosOffset);
        }

    }

}
