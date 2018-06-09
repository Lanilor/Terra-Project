using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

	public static class Rot4Utility
    {

        public static Rot4 RandomButExclude(Rot4 exclude)
        {
            Rot4 next;
            do
            {
                next = Rot4.Random;
            }
            while (next == exclude);
            return next;
        }

    }

}
