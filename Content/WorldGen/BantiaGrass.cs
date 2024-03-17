using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.WorldGen;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace TerraFactory
{
    internal class BantiaGrass : GenPass
    {
        public BantiaGrass(float loadWeight) : base("Grassifying Bantia", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            for (int i = GenData.Bantia_DesertEdge; i < GenData.Bantia_end; i++)
                for (int j = 0; j < GenData.surface + 5; j++)
                {
                    if (isExposedDirt(i, j))
                    {
                        WorldGen.PlaceTile(i, j, TileID.Grass, mute: true, forced: true);
                        // int wallID = WorldGen.genRand.NextBool() ? WallID.GrassUnsafe : WallID.FlowerUnsafe;
                        //TODO : find a cool looking way to place grasswall bushes
                    }
                }


        }

        /// <summary>
        /// Predicate that returns true if the block at target location is a dirt block with access to air nearby
        /// </summary>
        private bool isExposedDirt(int x, int y)
        {
            if (!WorldGen.InWorld(x, y, fluff: 2))
                return false;
            if (!(Main.tile[x, y].TileType == TileID.Dirt))
                return false;
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;
                    if (!Main.tile[x + dx, y + dy].HasTile) return true;
                }
            return false;
        }
    }
}
