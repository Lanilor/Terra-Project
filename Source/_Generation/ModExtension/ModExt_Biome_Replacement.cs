using System.Collections.Generic;
using RimWorld.Planet;
using Verse;

namespace TerraCore
{

    public class ModExt_Biome_Replacement : DefModExtension
    {

        public static readonly ModExt_Biome_Replacement Default = new ModExt_Biome_Replacement();

        public bool replaceElevation = false;
        public int elevationMin = 10;
        public int elevationMax = 100;

        public Hilliness? replaceHilliness = null;

        public TerrainDef sandReplacement;
        public TerrainDef gravelReplacement;

        public ModExt_Biome_Replacement()
        {
            ResolveReferences();
        }

        public override IEnumerable<string> ConfigErrors()
        {
            ResolveReferences();
            yield break;
        }

        private void ResolveReferences()
        {
            if (sandReplacement == null)
            {
                sandReplacement = TerrainDefOf.NormalSand;
            }
            if (gravelReplacement == null)
            {
                gravelReplacement = TerrainDefOf.NormalGravel;
            }
        }

    }
    
}
