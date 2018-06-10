using System;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;

namespace TerraFW
{

    [StaticConstructorOnStartup]
    public static class WorldMaterials
    {

        public static readonly Material Oasis = MaterialPool.MatFrom("World/Oasis", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    }

}
