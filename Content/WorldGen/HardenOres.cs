using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.Tiles.HardOres;
using TerraFactory.Content.WorldGen;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TerraFactory
{
    internal class HardenOres : GenPass
    {
        public HardenOres(float loadWeight) : base("Hardening ores", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            for (int i = GenData.Bantia_DesertEdge; i < GenData.Bantia_end; i++)
                for (int j = 0; j < GenData.surface + 5; j++)
                {

                    if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == TileID.Lead)
                    {
                        WorldGen.PlaceTile(i, j, ModContent.TileType<HardLead>(), mute: true, forced: true);
                    }




                }
        }
    }
}
