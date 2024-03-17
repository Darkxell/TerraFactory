using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using System.Diagnostics.Metrics;
using Terraria.ID;
using TerraFactory.Content.Utils;
using TerraFactory.Content.Global;
using Microsoft.Xna.Framework;
using TerraFactory.Content.Tiles.HardOres;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class MiningDrillTE : AbstractMachineTE
    {
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<MiningDrill>();
        }

        int counter = 0;
        public override void Update()
        {
            counter++;
            if (counter > 60)
            {
                counter = 0;
                Vector2 oreleft = new Vector2(Position.X, Position.Y + 4);
                Vector2 oreright = new Vector2(Position.X + 7, Position.Y + 5);

                if (getOreBelow() > 0)
                {
                    addFastItem(new FastItemStack(ItemID.LeadOre, 1));
                    Dust.QuickBox(oreleft * 16, oreright * 16, 2, Color.DarkGreen, null);
                }
                else
                {
                    Dust.QuickBox(oreleft * 16, oreright * 16, 2, Color.DarkRed, null);
                }
            }
        }

        private int[] DrillableBlocks = new int[] { ModContent.TileType<HardLead>() };

        private int getOreBelow()
        {
            int id1 = Main.tile[Position.X, Position.Y + 4].TileType;
            int id2 = Main.tile[Position.X + 6, Position.Y + 4].TileType;
            // If ores are different consider there's no ore
            if (id1 != id2)
                return -1;
            if (DrillableBlocks.Contains(id1))
                return id1;
            return -1;
        }

        public override string getDisplayName()
        {
            return "Mining drill";
        }

    }
}
