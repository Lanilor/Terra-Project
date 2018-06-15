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

    public class WorldLayer_SpecialBiomes : WorldLayer
    {

        private static readonly FloatRange BaseSizeRange = new FloatRange(0.85f, 1.0f);
        private static readonly IntVec2 TexturesInAtlas = new IntVec2(2, 2);
        private static readonly IntVec2 TexturesInAtlas_Full = new IntVec2(7, 2);
        private static readonly FloatRange BasePosOffsetRange = new FloatRange(0f, 0.06f);
        private static readonly FloatRange BasePosOffsetRange_Oasis = new FloatRange(0f, 0.15f);
        private const bool RandomizeRotation_Oasis = true;

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
            List<int> tmpNeighbors = new List<int>();
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
                Vector3 tileCenter = grid.GetTileCenter(i);
                float magnitude = tileCenter.magnitude;
                Vector3 drawPos = (tileCenter + Rand.UnitVector3 * tileData.posOffset * grid.averageTileSize).normalized * magnitude;
                WorldRendererUtility.PrintQuadTangentialToPlanetWithRodation(drawPos, tileCenter, tileData.sizeFactor * grid.averageTileSize, 0.005f, subMesh, tileData.rotVector);
                RimWorld.Planet.WorldRendererUtility.PrintTextureAtlasUVs(tileData.atlasX, tileData.atlasZ, tileData.texturesInAtlasX, tileData.texturesInAtlasZ, subMesh);
            }
			Rand.PopState();
			FinalizeMesh(MeshParts.All);
			yield break;
		}

	}

}
