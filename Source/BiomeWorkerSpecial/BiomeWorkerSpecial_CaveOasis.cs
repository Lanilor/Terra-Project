using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraFW
{

    public class BiomeWorkerSpecial_CaveOasis : BiomeWorkerSpecial
    {

        //public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);
        //public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.06f);

        private static readonly IntRange biomeChangeDigLenth = new IntRange(8, 30);
        private const float tunnelBiomeChance = 0.85f;
        private const float entranceBiomeChance = 0.15f;

        protected override float InitialGenChance
        {
            get { return 0.07f; }
        }

        protected override float GenChanceOffsetAfterFirstHit
        {
            get { return 0.03f; }
        }

        protected override float GenChancePerHitFactor
        {
            get { return 0.93f; }
        }

        public override bool PreRequirements(Tile tile)
        {
            if (tile.WaterCovered)
            {
                return false;
            }
            if (tile.hilliness == Hilliness.Flat)
            {
                return false;
            }
            return true;
        }

        public override void PostGeneration(int tileID)
        {
            int digLength = biomeChangeDigLenth.RandomInRange;
            DigTilesForBiomeChange(tileID, digLength, 2);
        }

        protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
        {
            float randVal = Rand.Value;
            if (randVal < entranceBiomeChance || end)
            {
                tile.biome = BiomeDefOf.CaveEntrance;
            }
            else if (randVal < tunnelBiomeChance)
            {
                tile.biome = BiomeDefOf.TunnelworldCave;
            }
            else
            {
                tile.biome = BiomeDefOf.CaveOasis;
            }
            GenWorldGen.UpdateTileByBiomeModExts(tile);
        }

        /*public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
        {
            return new WLTileGraphicData(WorldMaterials.CaveOasis, WLSizeFactor, WLPosOffset, false);
        }*/

    }

}
