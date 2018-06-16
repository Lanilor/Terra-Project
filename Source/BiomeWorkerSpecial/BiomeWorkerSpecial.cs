using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;

namespace TerraFW
{

    public abstract class BiomeWorkerSpecial : BiomeWorker
    {

        protected float genChance = 0f;

        protected virtual float InitialGenChance
        {
            get {
                return 0f;
            }
        }

        protected virtual float GenChanceOffsetAfterFirstHit
        {
            get {
                return 0f;
            }
        }

        protected virtual float GenChancePerHitFactor
        {
            get {
                return 0f;
            }
        }

        protected virtual float GenChancePerHitOffset
        {
            get {
                return 0f;
            }
        }

        public override float GetScore(Tile tile)
        {
            return -100f;
        }

        public virtual bool MinPreRequirements(Tile tile)
        {
            return !tile.WaterCovered;
        }

        public virtual bool PreRequirements(Tile tile)
        {
            return false;
        }

        public virtual void PostGeneration(int tileID) { }

        public void ResetChance()
        {
            genChance = InitialGenChance;
        }

        public bool TryGenerateByChance()
        {
            if (Rand.Value < genChance)
            {
                if (genChance == InitialGenChance)
                {
                    genChance -= GenChanceOffsetAfterFirstHit;
                }
                genChance = genChance * GenChancePerHitFactor - GenChancePerHitOffset;
                return true;
            }
            return false;
        }

        protected virtual void ChangeTileAfterSuccessfulDig(Tile tile, bool end) { }

        protected void DigTilesForBiomeChange(int startTileID, int digLengthMin, int digLengthMax, int maxDirChange, bool digBothDirections = true)
        {
            WorldGrid worldGrid = Find.WorldGrid;
            bool goOtherWay = false;
            int currTileID = startTileID;
            int dirBase = Rand.RangeInclusive(0, 5);
            for (int i = 0; i < digLengthMax; i++)
            {
                // Get good neighbor tile for next step
                int dir = GenWorldGen.NextRandomDigDir(dirBase, maxDirChange);
                currTileID = worldGrid.GetTileNeighborByDirection6WayInt(currTileID, dir);
                Tile tile = worldGrid[currTileID];
                // Check if prerequirements for the biome are still met
                if (!((i < digLengthMin && MinPreRequirements(tile)) || PreRequirements(tile)))
                {
                    // Try to dig in the other way from the start first, otherwise abort
                    if (goOtherWay)
                    {
                        break;
                    }
                    else
                    {
                        currTileID = startTileID;
                        dirBase = GenWorldGen.InvertDigDir(dirBase);
                        i = -1;
                        goOtherWay = true;
                        continue;
                    }
                }
                // Set new biome (only when there is no special biome on the tile)
                if (tile.biome.WorkerSpecial() == null)
                {
                    bool endTile = (i == digLengthMax - 1);
                    ChangeTileAfterSuccessfulDig(tile, endTile);
                }
                // Go the other way if end is reached
                if (digBothDirections && i == digLengthMax - 1 && !goOtherWay)
                {
                    currTileID = startTileID;
                    dirBase = GenWorldGen.InvertDigDir(dirBase);
                    i = -1;
                    goOtherWay = true;
                }
            }
        }

        public virtual WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
        {
            return null;
        }

    }

}
