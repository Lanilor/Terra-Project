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

        private static readonly FloatRange BaseSizeRange = new FloatRange(0.9f, 1.1f);
        private static readonly IntVec2 TexturesInAtlas = new IntVec2(2, 2);
        private static readonly FloatRange BasePosOffsetRange_Oasis = new FloatRange(0f, 0.06f);

        //private static readonly FloatRange BasePosOffsetRange_SmallHills = new FloatRange(0f, 0.37f);
        //private static readonly FloatRange BasePosOffsetRange_LargeHills = new FloatRange(0f, 0.2f);
        //private static readonly FloatRange BasePosOffsetRange_ImpassableMountains = new FloatRange(0f, 0.06f);

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
                Material material;
                FloatRange floatRange;
                // Get data for the biome
                if (tile.biome == BiomeDefOf.Oasis)
                {
                    material = WorldMaterials.Oasis;
                    floatRange = BasePosOffsetRange_Oasis;
                }
                else
                {
                    continue;
                }
                // Draw mesh
                LayerSubMesh subMesh = GetSubMesh(material);
                Vector3 vector = grid.GetTileCenter(i);
                Vector3 posForTangents = vector;
                float magnitude = vector.magnitude;
                vector = (vector + Rand.UnitVector3 * floatRange.RandomInRange * grid.averageTileSize).normalized * magnitude;
                WorldRendererUtility.PrintQuadTangentialToPlanet(vector, posForTangents, BaseSizeRange.RandomInRange * grid.averageTileSize, 0.005f, subMesh, false, false, false);
                WorldRendererUtility.PrintTextureAtlasUVs(Rand.Range(0, TexturesInAtlas.x), Rand.Range(0, TexturesInAtlas.z), TexturesInAtlas.x, TexturesInAtlas.z, subMesh);
            }
			Rand.PopState();
			base.FinalizeMesh(MeshParts.All);
			yield break;
		}

	}

}
