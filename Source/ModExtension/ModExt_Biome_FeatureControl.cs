using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

    public class ModExt_Biome_FeatureControl : DefModExtension
    {

        public GenFeatureTristate overwriteCaves = GenFeatureTristate.Default;

        public HiveOverwriteType overwriteHives = HiveOverwriteType.Default;
        public int additionalHivesScaling = 0;

        public RoofOverwriteType overwriteRoof = RoofOverwriteType.None;
        public RoomCalculationType roomCalculationType = RoomCalculationType.Default;

        public bool removeBeach = false;

        public bool removeRuinsSimple = false;
        public bool removeShrines = false;

        public RockChunksOverwriteType overwriteRockChunks = RockChunksOverwriteType.Default;

    }
    
}
