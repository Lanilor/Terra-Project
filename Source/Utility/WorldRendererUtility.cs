using System;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using RimWorld.Planet;

namespace TerraFW
{

    public static class WorldRendererUtility
    {

        private static List<Vector3> tmpVerts = new List<Vector3>();

        public static void DrawTileTangentialToPlanetWithRodation(WorldGrid grid, LayerSubMesh subMesh, int tileID, int atlasX, int atlasZ, IntVec2 texturesInAtlas, int rotDir)
        {
            int totalVertCount = subMesh.verts.Count;
            grid.GetTileVertices(tileID, tmpVerts);
            int tileVertCount = tmpVerts.Count;
            if (tileVertCount != 6) { Log.Error("id: " + tileID + ", tileVertCount: " + tileVertCount); }
            if (rotDir < 0) { Log.Error("id: " + tileID + ", dir: " + rotDir); }

            if (rotDir < 0)
            {
                rotDir += tileVertCount;
            }
            for (int i = 0; i < tileVertCount; i++)
            {
                int vertIndex = (i + rotDir) % tileVertCount;
                subMesh.verts.Add(tmpVerts[vertIndex] + tmpVerts[vertIndex].normalized * 0.012f);
                Vector2 posAtlasUV = (GenGeo.RegularPolygonVertexPosition(tileVertCount, i) + Vector2.one) / 2f;
                posAtlasUV.x = (posAtlasUV.x + atlasX) / texturesInAtlas.x;
                posAtlasUV.y = (posAtlasUV.y + atlasZ) / texturesInAtlas.z;
                subMesh.uvs.Add(posAtlasUV);
                if (i < tileVertCount - 2)
                {
                    subMesh.tris.Add(totalVertCount + i + 2);
                    subMesh.tris.Add(totalVertCount + i + 1);
                    subMesh.tris.Add(totalVertCount);
                }
            }
        }

        public static void PrintQuadTangentialToPlanetWithRodation(Vector3 pos, Vector3 posForTangents, float size, float altOffset, LayerSubMesh subMesh, Vector3 rotVec)
        {
            Vector3 first;
            Vector3 second;
            GetTangentsToPlanetWithRotation(posForTangents, out first, out second, rotVec);
            Vector3 normalized = posForTangents.normalized;
            float d = size * 0.5f;
            Vector3 v1 = pos - first * d - second * d + normalized * altOffset;
            Vector3 v2 = pos - first * d + second * d + normalized * altOffset;
            Vector3 v3 = pos + first * d + second * d + normalized * altOffset;
            Vector3 v4 = pos + first * d - second * d + normalized * altOffset;
            int count = subMesh.verts.Count;
            subMesh.verts.Add(v1);
            subMesh.verts.Add(v2);
            subMesh.verts.Add(v3);
            subMesh.verts.Add(v4);
            subMesh.tris.Add(count);
            subMesh.tris.Add(count + 1);
            subMesh.tris.Add(count + 2);
            subMesh.tris.Add(count);
            subMesh.tris.Add(count + 2);
            subMesh.tris.Add(count + 3);
        }

        public static void GetTangentsToPlanetWithRotation(Vector3 pos, out Vector3 first, out Vector3 second, Vector3 rotVec)
        {
            Quaternion rotation = Quaternion.LookRotation(pos.normalized, rotVec);
            first = rotation * Vector3.up;
            second = rotation * Vector3.right;
        }

    }

}
