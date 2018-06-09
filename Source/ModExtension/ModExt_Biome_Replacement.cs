using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld.Planet;

namespace TerraFW
{

    public class ModExt_Biome_Replacement : DefModExtension
    {

        public static readonly ModExt_Biome_Replacement defaultValues = new ModExt_Biome_Replacement();

        public bool replaceElevation = false;
        public int elevationMin = 10;
        public int elevationMax = 100;

        public Hilliness? replaceHilliness = null;

        public TerrainDef sandReplacement = TerrainDefOf.NormalSand;
        public TerrainDef gravelRelacement = TerrainDefOf.NormalGravel;

    }
    
}
