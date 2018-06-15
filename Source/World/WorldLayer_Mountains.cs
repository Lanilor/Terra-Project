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

        List<Vector3> tmpVerts = new List<Vector3>();

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
                    //Vector3 rotVector = Vector3.up;
                    int rotVector = 0;
                    grid.GetTileGraphicDataFromNeighbors(i, out atlasX, out atlasZ, out rotVector, (Tile tileFrom, Tile neighbor) => (neighbor.hilliness == Hilliness.Impassable || neighbor.biome == BiomeDefOf.CaveOasis || neighbor.biome == BiomeDefOf.CaveEntrance || neighbor.biome == BiomeDefOf.TunnelworldCave));
                    DrawTile(grid, i, material, atlasX, atlasZ, 1.10f, rotVector);

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
			FinalizeMesh(MeshParts.All);
			yield break;
		}

        private void DrawTile(WorldGrid grid, int tileID, Material material, int atlasX, int atlasZ, float sizeFactor, int rotDir)
        {
            LayerSubMesh subMesh = GetSubMesh(material);
            int totalVertCount = subMesh.verts.Count;
            grid.GetTileVertices(tileID, tmpVerts);
            int tileVertCount = tmpVerts.Count;
            Log.Warning("id: " + tileID + ", dir: " + rotDir);

            int startIndex = 0;
            float maxHeightVal = -999f;
            for (int i = 0; i < tileVertCount; i++)
            {
                if (tmpVerts[i].y >= maxHeightVal)
                {
                    startIndex = i;
                    maxHeightVal = tmpVerts[i].y;
                }
            }

            Vector3 v1 = tmpVerts[0] - tmpVerts[1];
            Vector3 v2 = tmpVerts[0] - tmpVerts[2];
            Vector3 vc = Vector3.Cross(v1, v2).normalized;

            List<int> tmpNeighbors = new List<int>();
            grid.GetTileNeighbors(tileID, tmpNeighbors);

            for (int i = 0; i < tileVertCount; i++)
            {
                Log.Message(i + ": " + tmpVerts[i]);
                int vertIndex = i + rotDir;
                if (vertIndex < 0)
                {
                    vertIndex += tileVertCount;
                }
                if (vertIndex >= tileVertCount)
                {
                    vertIndex -= tileVertCount;
                }
                if (vertIndex >= tileVertCount)
                {
                    vertIndex -= tileVertCount;
                }

                int elevTiles = 0;
                float elevAvg = 0;
                List<Vector3> tmpV2 = new List<Vector3>();
                foreach (int tid in tmpNeighbors) {
                    grid.GetTileVertices(tid, tmpV2);
                    if (tmpV2.Contains(tmpVerts[vertIndex]))
                    {
                        if (grid[tid].hilliness == Hilliness.Impassable)
                        {
                            elevTiles++;
                            elevAvg += grid[tid].elevation;
                        }
                        else
                        {
                            elevAvg = 0;
                            break;
                        }
                    }
                }
                elevAvg /= elevTiles;

                subMesh.verts.Add(tmpVerts[vertIndex] + tmpVerts[vertIndex].normalized * 0.012f + tmpVerts[vertIndex].normalized * elevAvg / 200);
                Vector2 posAtlasUV = (GenGeo.RegularPolygonVertexPosition(tileVertCount, i) + Vector2.one) / 2f;
                posAtlasUV.x = (posAtlasUV.x + atlasX) / TexturesInAtlas.x;
                posAtlasUV.y = (posAtlasUV.y + atlasZ) / TexturesInAtlas.z;
                subMesh.uvs.Add(posAtlasUV);
                if (i < tileVertCount - 2)
                {
                    subMesh.tris.Add(totalVertCount + i + 2);
                    subMesh.tris.Add(totalVertCount + i + 1);
                    subMesh.tris.Add(totalVertCount);
                }
            }

            /*
            LayerSubMesh subMesh = GetSubMesh(material);
            Vector3 tileCenter = grid.GetTileCenter(tileID);
            Vector3 drawPos = tileCenter.normalized * tileCenter.magnitude;
            WorldRendererUtility.PrintQuadTangentialToPlanetWithRodation(drawPos, tileCenter, sizeFactor * grid.averageTileSize, 0.005f, subMesh, rotVector);
            RimWorld.Planet.WorldRendererUtility.PrintTextureAtlasUVs(atlasX, atlasZ, TexturesInAtlas.x, TexturesInAtlas.z, subMesh);
            */
        }

	}

}
