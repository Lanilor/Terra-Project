using System;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;
using System.Collections.Generic;

namespace TerraFW
{

    public static class WorldRendererUtility
    {

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
