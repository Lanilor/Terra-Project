using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;

namespace TerraFW
{

    public class BiomeWorkerSpecial_DeepRavine : BiomeWorkerSpecial
    {

        private static readonly IntRange biomeChangeDigLenth = new IntRange(2, 10);

        protected override float InitialGenChance
        {
            get { return 0.05f; }
        }

        protected override float GenChanceOffsetAfterFirstHit
        {
            get { return 0.043f; }
        }

        protected override float GenChancePerHitFactor
        {
            get { return 0.6f; }
        }

        public override bool PreRequirements(Tile tile)
        {
            if (tile.WaterCovered)
            {
                return false;
            }
            if (tile.hilliness == Hilliness.Mountainous || tile.hilliness == Hilliness.Impassable)
            {
                return false;
            }
            if (tile.temperature < 10f)
            {
                return false;
            }
            if (tile.rainfall > 600f)
            {
                return false;
            }
            return true;
        }

        public override void PostGeneration(int tileID)
        {
            int digLength = biomeChangeDigLenth.RandomInRange;
            DigTilesForBiomeChange(tileID, digLength, 1);
        }

        protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
        {
            tile.biome = BiomeDefOf.DeepRavine;
            GenWorldGen.UpdateTileByBiomeModExts(tile);
        }

        public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
        {
            int atlasX;
            int atlasZ;
            int rotDir;
            grid.GetTileGraphicDataFromNeighbors(tileID, out atlasX, out atlasZ, out rotDir, (Tile tileFrom, Tile neighbor) => (tileFrom.biome == neighbor.biome));
            return new WLTileGraphicData(WorldMaterials.DeepRavine, atlasX, atlasZ, rotDir);
        }

    }

}
