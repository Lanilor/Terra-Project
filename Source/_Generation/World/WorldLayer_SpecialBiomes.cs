using System;
using System.Collections;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace TerraCore
{

    public class WorldLayer_SpecialBiomes : WorldLayer
    {

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
                BiomeWorkerSpecial specialWorker = tile.biome.WorkerSpecial();
                if (specialWorker == null)
                {
                    continue;
                }
                WLTileGraphicData tileData = specialWorker.GetWLTileGraphicData(grid, i);
                if (tileData == null)
                {
                    continue;
                }
                // Draw mesh
                LayerSubMesh subMesh = GetSubMesh(tileData.material);
                if (tileData.drawAsQuad)
                {
                    Vector3 tileCenter = grid.GetTileCenter(i);
                    Vector3 drawPos = (tileCenter + Rand.UnitVector3 * tileData.posOffset * grid.averageTileSize).normalized * tileCenter.magnitude;
                    WorldRendererUtility.PrintQuadTangentialToPlanetWithRodation(drawPos, tileCenter, tileData.sizeFactor * grid.averageTileSize, 0.005f, subMesh, tileData.rotVector);
                    RimWorld.Planet.WorldRendererUtility.PrintTextureAtlasUVs(tileData.atlasX, tileData.atlasZ, tileData.texturesInAtlasX, tileData.texturesInAtlasZ, subMesh);
                }
                else
                {
                    IntVec2 texturesInAtlas = new IntVec2(tileData.texturesInAtlasX, tileData.texturesInAtlasZ);
                    WorldRendererUtility.DrawTileTangentialToPlanetWithRodation(grid, subMesh, i, tileData.atlasX, tileData.atlasZ, texturesInAtlas, tileData.rotDir);
                }
            }
			Rand.PopState();
			FinalizeMesh(MeshParts.All);
			yield break;
		}

	}

}
