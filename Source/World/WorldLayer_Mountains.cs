using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using RimWorld.Planet;
using System.Collections;

namespace TerraFW
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
                    Vector3 rotVector = Vector3.up;
                    grid.GetTileGraphicDataFromNeighbors(i, out atlasX, out atlasZ, out rotVector, (Tile tileFrom, Tile neighbor) => (neighbor.hilliness == Hilliness.Impassable || neighbor.biome == BiomeDefOf.CaveOasis || neighbor.biome == BiomeDefOf.CaveEntrance || neighbor.biome == BiomeDefOf.TunnelworldCave));
                    DrawTile(grid, i, material, atlasX, atlasZ, 1.14f, rotVector);

                    // Draw cave system afterwards if a cave exists on that tile
                    if (tile.hilliness != Hilliness.Impassable)
                    {
                        material = WorldMaterials.CaveSystem;
                        grid.GetTileGraphicDataFromNeighbors(i, out atlasX, out atlasZ, out rotVector, (Tile tileFrom, Tile neighbor) => (neighbor.biome == BiomeDefOf.CaveOasis || neighbor.biome == BiomeDefOf.CaveEntrance || neighbor.biome == BiomeDefOf.TunnelworldCave));
                        DrawTile(grid, i, material, atlasX, atlasZ, 1.0f, rotVector);
                    }
                }
            }
			Rand.PopState();
			base.FinalizeMesh(MeshParts.All);
			yield break;
		}

        private void DrawTile(WorldGrid grid, int tileID, Material material, int atlasX, int atlasZ, float sizeFactor, Vector3 rotVector)
        {
            LayerSubMesh subMesh = GetSubMesh(material);
            Vector3 tileCenter = grid.GetTileCenter(tileID);
            Vector3 drawPos = tileCenter.normalized * tileCenter.magnitude;
            WorldRendererUtility.PrintQuadTangentialToPlanetWithRodation(drawPos, tileCenter, sizeFactor * grid.averageTileSize, 0.005f, subMesh, rotVector);
            RimWorld.Planet.WorldRendererUtility.PrintTextureAtlasUVs(atlasX, atlasZ, TexturesInAtlas.x, TexturesInAtlas.z, subMesh);
        }

	}

}
