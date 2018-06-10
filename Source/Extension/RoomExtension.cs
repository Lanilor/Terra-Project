using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

	public static class RoomExtension
    {

        public static bool OutdoorsByRCType(this Room room, RoomCalculationType rcType)
        {
            if (rcType == RoomCalculationType.LessSky)
            {
                if (room.Group.AnyRoomTouchesMapEdge)
                {
                    return true;
                }
                else
                {
                    float openRoof = (float)room.OpenRoofCount;
                    float roofRate = openRoof / (float)room.CellCount;
                    float skyRate = 0.05f / (0.003f * openRoof + 0.1f);
                    return roofRate >= skyRate;
                }
            }
            else if (rcType == RoomCalculationType.NoSky)
            {
                return room.Group.AnyRoomTouchesMapEdge;
            }
            else
            {
                return room.PsychologicallyOutdoors;
            }
        }

    }

}
