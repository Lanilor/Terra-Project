using RimWorld;
using Verse;

namespace TerraCore
{

    [DefOf]
    public static class RoofDefOf
    {

        static RoofDefOf()
		{
            DefOfHelper.EnsureInitializedInCtor(typeof(RoofDefOf));
		}

        public static RoofDef RoofRockUncollapsable;

    }

}
