using RimWorld.Planet;
using Verse;

namespace TerraCore
{

    public class BiomeWorkerSpecial_CaveEntrance : BiomeWorkerSpecial
    {

        //public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);
        //public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.06f);

        private const int BiomeChangeDigLengthMin = 3;
        private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(8, 30);
        private const float TunnelBiomeChance = 0.85f;
        private const float OasisBiomeChance = 0.15f;

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
            get { return 0.9f; }
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
            int digLengthMax = BiomeChangeDigLengthMax.RandomInRange;
            DigTilesForBiomeChange(tileID, BiomeChangeDigLengthMin, digLengthMax, 2);
        }

        protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
        {
            float randVal = Rand.Value;
            if (randVal < OasisBiomeChance || end)
            {
                tile.biome = BiomeDefOf.CaveOasis;
            }
            else if (randVal < TunnelBiomeChance)
            {
                tile.biome = BiomeDefOf.TunnelworldCave;
            }
            else
            {
                tile.biome = BiomeDefOf.CaveEntrance;
            }
            GenWorldGen.UpdateTileByBiomeModExts(tile);
        }

        /*public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
        {
            return new WLTileGraphicData(WorldMaterials.CaveEntrance, WLSizeFactor, WLPosOffset, false);
        }*/

    }

}
