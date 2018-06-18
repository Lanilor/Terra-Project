using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld.Planet;
using RimWorld;

namespace TerraFW
{

    public class WorldGenStep_SpecialTerrain : WorldGenStep
    {

        private const float ImpassableChangeChance = 0.8f;
        private const float ImpassableChangeRecursiveChance = 0.2f;
        private const int MaxImpassableChangeDepth = 3;

        private static List<int> tmpNeighbors = new List<int>();

        public override int SeedPart
        {
            get
            {
                return 144374476;
            }
        }

        public override void GenerateFresh(string seed)
        {
            // Get list of biomes and filter for only ones with special worker
            List<BiomeDef> biomes = DefDatabase<BiomeDef>.AllDefsListForReading;
            List<BiomeDef> specialBiomes = new List<BiomeDef>();
            for (int i = biomes.Count - 1; i >= 0; i--)
            {
                if (biomes[i].WorkerSpecial() != null)
                {
                    specialBiomes.Add(biomes[i]);
                    // Reset/init chance algorithm for world generation
                    biomes[i].WorkerSpecial().ResetChance();
                }
            }

            // Apply special biome workers to tiles
            WorldGrid grid = Find.WorldGrid;
            List<Tile> tiles = grid.tiles;
            int tilesCount = grid.TilesCount;
            for (int i = 0; i < tilesCount; i++)
            {
                // Skip this tile if it already contains a special biome
                if (specialBiomes.Contains(tiles[i].biome))
                {
                    continue;
                }
                // Check all special biome defs if this tiles biome should be converted into a new biome
                foreach (BiomeDef biome in specialBiomes)
                {
                    BiomeWorkerSpecial worker = biome.WorkerSpecial();
                    Tile currTile = tiles[i];
                    if (worker.PreRequirements(currTile) && worker.TryGenerateByChance())
                    {
                        // Update biome and biome data via mod extensions
                        currTile.biome = biome;
                        GenWorldGen.UpdateTileByBiomeModExts(currTile);
                        // Apply post generation effects (e.g. change more surrounding tiles)
                        worker.PostGeneration(i);
                    }
                }
            }

            // Change hilliness around caves to make them appear deeper in mountains
            for (int i = 0; i < tilesCount; i++)
            {
                Tile currTile = tiles[i];
                if (currTile.biome == BiomeDefOf.CaveOasis || currTile.biome == BiomeDefOf.TunnelworldCave)
                {
                    List<BiomeDef> excludeBiomes = new List<BiomeDef>(specialBiomes);
                    excludeBiomes.Add(RimWorld.BiomeDefOf.SeaIce);
                    excludeBiomes.Add(RimWorld.BiomeDefOf.Lake);
                    excludeBiomes.Add(RimWorld.BiomeDefOf.Ocean);
                    MakeImpassableHillsAroundTile(grid, i, excludeBiomes, 1);
                }
            }
        }

        private void MakeImpassableHillsAroundTile(WorldGrid grid, int tileID, List<BiomeDef> excludeBiomes, int currDepth)
        {
            bool entranceFlag = false;
            grid.GetTileNeighbors(tileID, tmpNeighbors);
            // Check if any neighbor is a cave entrance
            for (int j = 0; j < tmpNeighbors.Count; j++)
            {
                if (grid[tmpNeighbors[j]].biome == BiomeDefOf.CaveEntrance)
                {
                    entranceFlag = true;
                    break;
                }
            }
            // Change hilliness of neighbors if there is no cave entrance nearby
            if (!entranceFlag)
            {
                for (int j = 0; j < tmpNeighbors.Count; j++)
                {
                    Tile nTile = grid[tmpNeighbors[j]];
                    // Change only non-special biomes and non-baseexcluded biomes (mainly water tiles)
                    if (excludeBiomes.Contains(nTile.biome))
                    {
                        continue;
                    }
                    if (Rand.Value < ImpassableChangeChance)
                    {
                        nTile.hilliness = Hilliness.Impassable;
                        // Change recursive around this tile if conditions are met
                        if (currDepth < MaxImpassableChangeDepth && Rand.Value < ImpassableChangeRecursiveChance)
                        {
                            MakeImpassableHillsAroundTile(grid, tmpNeighbors[j], excludeBiomes, currDepth + 1);
                        }
                    }
                }
            }
        }

    }

}
