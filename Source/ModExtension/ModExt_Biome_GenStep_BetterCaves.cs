using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TerraFW
{

    public class ModExt_Biome_GenStep_BetterCaves : DefModExtension
    {

        public float startTunnelsPer10k = 2.0f;
        public int maxStartTunnelsPerRockGroup = 6;
        public int minRocksToGenerateAnyTunnel = 600;
        public float tunnelStartWidthFactorMin = 0.6f;
        public float tunnelStartWidthFactorMax = 0.9f;
        public float directionChangeSpeed = 8f;
        public int allowBranchingAfterSteps = 30;
        public int minDistToOutsideForBranching = 15;
        public float branchChance = 0.015f;
        public float branchChanceTypeRoom = 0.15f;
        public float branchChanceTypeTunnel = 0.25f;
        public float minTunnelWidth = 3.4f;
        public float widthOffsetPerCellMin = 0.019f;
        public float widthOffsetPerCellMax = 0.044f;
        public float widthOffsetPerCellTunnelFactor = 0.2f;
        public float branchWidthFactorMin = 0.6f;
        public float branchWidthFactorMax = 0.9f;
        public float branchRoomFixedWidthMin = 20f;
        public float branchRoomFixedWidthMax = 30f;
        public float branchTunnelFixedWidthMin = 4.5f;
        public float branchTunnelFixedWidthMax = 7f;
        public int branchRoomMaxLength = 25;

        public float terrainPatchMakerFrequencyCaveWater = 0.08f;
        public List<TerrainThresholdWEO> terrainPatchMakerCaveWater = null;
        public float terrainPatchMakerFrequencyCaveGravel = 0.16f;
        public List<TerrainThresholdWEO> terrainPatchMakerCaveGravel = null;

    }
    
}
