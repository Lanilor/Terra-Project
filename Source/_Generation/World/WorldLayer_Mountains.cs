using System;
using System.Collections;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace TerraCore
{

    public class WorldLayer_Mountains : WorldLayer
    {

        private static readonly IntVec2 TexturesInAtlas = new IntVec2(7, 2);

		public override IEnumerable Regenerate()
		{
			IEnumerator enumerator = base.Regenerate().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object result = enumerator.Current;
					yield return result;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			Rand.PushState();
			Rand.Seed = Find.World.info.Seed;
			WorldGrid grid = Find.WorldGrid;
			int tilesCount = grid.TilesCount;
            for (int i = 0; i < tilesCount; i++)
            {
                Tile tile = grid[i];
                if (tile.hilliness == Hilliness.Impassable || tile.biome == BiomeDefOf.CaveOasis || tile.biome == BiomeDefOf.CaveEntrance || tile.biome == BiomeDefOf.TunnelworldCave)
                {
                    // Draw impassable mountains first
                    Material material = WorldMaterials.HillinessImpassable;
                    int atlasX = 0;
                    int atlasZ = 0;
                    int rotDir = 0;
                    grid.GetTileGraphicDataFromNeighbors(i, out atlasX, out atlasZ, out rotDir, (Tile tileFrom, Tile neighbor) => (neighbor.hilliness == Hilliness.Impassable || neighbor.biome == BiomeDefOf.CaveOasis || neighbor.biome == BiomeDefOf.CaveEntrance || neighbor.biome == BiomeDefOf.TunnelworldCave));
                    WorldRendererUtility.DrawTileTangentialToPlanetWithRodation(grid, GetSubMesh(material), i, atlasX, atlasZ, TexturesInAtlas, rotDir);

                    // Draw cave system afterwards if a cave exists on that tile
                    if (tile.hilliness != Hilliness.Impassable)
                    {
                        material = WorldMaterials.CaveSystem;
                        grid.GetTileGraphicDataFromNeighbors(i, out atlasX, out atlasZ, out rotDir, (Tile tileFrom, Tile neighbor) => (neighbor.biome == BiomeDefOf.CaveOasis || neighbor.biome == BiomeDefOf.CaveEntrance || neighbor.biome == BiomeDefOf.TunnelworldCave));
                        WorldRendererUtility.DrawTileTangentialToPlanetWithRodation(grid, GetSubMesh(material), i, atlasX, atlasZ, TexturesInAtlas, rotDir);
                    }
                }
            }
			Rand.PopState();
			FinalizeMesh(MeshParts.All);
			yield break;
		}

	}

}
