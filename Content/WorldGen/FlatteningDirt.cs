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
    internal class FlatteningDirt : GenPass
    {
        public FlatteningDirt(float loadWeight) : base("Flattening the world", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;


            // Flattens the dirt ground to be exactly at surface layer
            for (int i = 0; i < GenData.worldWidth; i++)
                for (int j = 0; j < GenData.worldHeight; j++)
                {
                    if (j < GenData.surface)
                    {
                        WorldGen.KillTile(i, j, noItem: true);
                    }
                    else
                    {
                        if (!Main.tile[i, j].HasTile)
                        {
                            WorldGen.PlaceTile(i, j, TileID.Dirt, mute: true, forced: true);
                        }
                    }
                }
            // Adds small height variation above the surface
            int plateau = 0, heightvar = 0;
            for (int i = 0; i < GenData.worldWidth; i++)
            {
                plateau--;
                if (plateau <= 0)
                {
                    plateau = WorldGen.genRand.Next(2, 9);
                    switch (heightvar)
                    {
                        case 0:
                            heightvar += WorldGen.genRand.Next(0, 2);
                            break;
                        case 5:
                            heightvar -= WorldGen.genRand.Next(0, 2);
                            break;
                        default:
                            heightvar += WorldGen.genRand.Next(0, 3) - 1;
                            break;
                    }
                }
                for (int k = 0; k < 10; k++)
                {
                    WorldGen.PlaceTile(i, GenData.surface - heightvar + k, TileID.Dirt, mute: true, forced: true);
                }
            }
        }
    }
}
