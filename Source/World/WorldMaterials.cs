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

        // WorldLayer_Mountains
        public static readonly Material HillinessImpassable = MaterialPool.MatFrom("World/HillinessImpassable", ShaderDatabase.WorldOverlayTransparentLit, 3511);
        public static readonly Material CaveSystem = MaterialPool.MatFrom("World/CaveSystem", ShaderDatabase.WorldOverlayTransparentLit, 3512);

        // WorldLayer_SpecialBiomes
        public static readonly Material SpikeMountains = MaterialPool.MatFrom("World/LayerIcon/SpikeMountains", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material Oasis = MaterialPool.MatFrom("World/LayerIcon/Oasis", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        //public static readonly Material CaveOasis = MaterialPool.MatFrom("World/LayerIcon/CaveOasis", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        //public static readonly Material TunnelworldCave = MaterialPool.MatFrom("World/LayerIcon/TunnelworldCave", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        //public static readonly Material CaveEntrance = MaterialPool.MatFrom("World/LayerIcon/CaveEntrance", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material InfestedMountains = MaterialPool.MatFrom("World/LayerIcon/InfestedMountains", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material DeepRavine = MaterialPool.MatFrom("World/LayerIcon/DeepRavine", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material Archipelago = MaterialPool.MatFrom("World/LayerIcon/Archipelago", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material VolcanicIsland = MaterialPool.MatFrom("World/LayerIcon/VolcanicIsland", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material TundraSkerries = MaterialPool.MatFrom("World/LayerIcon/TundraSkerries", ShaderDatabase.WorldOverlayTransparentLit, 3515);
        public static readonly Material Atoll = MaterialPool.MatFrom("World/LayerIcon/Atoll", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    }

}
