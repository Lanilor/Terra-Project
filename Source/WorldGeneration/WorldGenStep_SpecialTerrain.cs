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

        public override void GenerateFresh(string seed)
        {
            Rand.Seed = GenText.StableStringHash(seed);
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
            List<int> tmpNeighbors = new List<int>();
            for (int i = 0; i < tilesCount; i++)
            {
                Tile currTile = tiles[i];
                if (currTile.biome == BiomeDefOf.CaveOasis || currTile.biome == BiomeDefOf.TunnelworldCave)
                {
                    bool entranceFlag = false;
                    grid.GetTileNeighbors(i, tmpNeighbors);
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
                            // Change only non-special biomes
                            if (specialBiomes.Contains(nTile.biome) || nTile.biome == RimWorld.BiomeDefOf.SeaIce || nTile.biome == RimWorld.BiomeDefOf.Lake || nTile.biome == RimWorld.BiomeDefOf.Ocean)
                            {
                                continue;
                            }
                            nTile.hilliness = Hilliness.Impassable;
                        }
                    }
                }
            }

            Rand.RandomizeStateFromTime();
        }

    }

}
