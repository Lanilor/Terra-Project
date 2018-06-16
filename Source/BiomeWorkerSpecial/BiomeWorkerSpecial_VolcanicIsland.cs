using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorkerSpecial_VolcanicIsland : BiomeWorkerSpecial
    {

        public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);
        public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.15f);

        private const int BiomeChangeDigLengthMin = 4;
        private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(6, 18);
        private const float BiomeChangeChance = 0.3f;
        private const float ArchipelagoBiomeChance = 0.6f;

        protected override float InitialGenChance
        {
            get { return 0.05f; }
        }

        protected override float GenChanceOffsetAfterFirstHit
        {
            get { return 0.044f; }
        }

        protected override float GenChancePerHitFactor
        {
            get { return 0.42f; }
        }

        public override bool MinPreRequirements(Tile tile)
        {
            return tile.WaterCovered;
        }

        public override bool PreRequirements(Tile tile)
        {
            if (!tile.WaterCovered || tile.elevation > -60f)
            {
                return false;
            }
            if (tile.temperature < 0f || tile.temperature >= 25f)
            {
                return false;
            }
            return true;
        }

        public override void PostGeneration(int tileID)
        {
            int digLengthMax = BiomeChangeDigLengthMax.RandomInRange;
            DigTilesForBiomeChange(tileID, BiomeChangeDigLengthMin, digLengthMax, 2);
        }

        protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
        {
            if (Rand.Value < BiomeChangeChance)
            {
                if (Rand.Value < ArchipelagoBiomeChance)
                {
                    tile.biome = BiomeDefOf.Archipelago;
                }
                else
                {
                    tile.biome = BiomeDefOf.VolcanicIsland;
                }
                GenWorldGen.UpdateTileByBiomeModExts(tile);
            }
        }

        public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
        {
            return new WLTileGraphicData(WorldMaterials.VolcanicIsland, WLSizeFactor, WLPosOffset);
        }

    }

}
