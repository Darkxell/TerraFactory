using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.Global;
using TerraFactory.Content.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class MechanicalFurnaceTE : AbstractMachineTE
    {

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<MechanicalFurnace>();
        }

        int counter = 0;
        public override void Update()
        {
            if (counter < 180)
            {
                counter++;
                return;
            }
            counter = 0;

            // Smelts down the base 8 prehardmode ores into bars
            // Only makes 1 bar per smelt maximum
            bool smelted = false;
            foreach (FastItemStack stk in content)
            {
                switch (stk.itemID)
                {
                    case ItemID.CopperOre:
                        if (stk.quantity >= 3)
                        {
                            stk.quantity -= 3;
                            addFastItem(new FastItemStack(ItemID.CopperBar, 1));
                            smelted = true;
                        }
                        break;
                    case ItemID.TinOre:
                        if (stk.quantity >= 3)
                        {
                            stk.quantity -= 3;
                            addFastItem(new FastItemStack(ItemID.TinBar, 1));
                            smelted = true;
                        }
                        break;
                    case ItemID.IronOre:
                        if (stk.quantity >= 3)
                        {
                            stk.quantity -= 3;
                            addFastItem(new FastItemStack(ItemID.IronBar, 1));
                            smelted = true;
                        }
                        break;
                    case ItemID.LeadOre:
                        if (stk.quantity >= 3)
                        {
                            stk.quantity -= 3;
                            addFastItem(new FastItemStack(ItemID.LeadBar, 1));
                            smelted = true;
                        }
                        break;

                    case ItemID.SilverOre:
                        if (stk.quantity >= 4)
                        {
                            stk.quantity -= 4;
                            addFastItem(new FastItemStack(ItemID.SilverBar, 1));
                            smelted = true;
                        }
                        break;
                    case ItemID.TungstenOre:
                        if (stk.quantity >= 4)
                        {
                            stk.quantity -= 4;
                            addFastItem(new FastItemStack(ItemID.TungstenBar, 1));
                            smelted = true;
                        }
                        break;
                    case ItemID.GoldOre:
                        if (stk.quantity >= 4)
                        {
                            stk.quantity -= 4;
                            addFastItem(new FastItemStack(ItemID.GoldBar, 1));
                            smelted = true;
                        }
                        break;
                    case ItemID.PlatinumOre:
                        if (stk.quantity >= 4)
                        {
                            stk.quantity -= 4;
                            addFastItem(new FastItemStack(ItemID.PlatinumBar, 1));
                            smelted = true;
                        }
                        break;
                }
                if (smelted) break;
            }

            // Removes the empty stacks from the furnace
            content.RemoveAll(stk => stk.quantity <= 0);
        }

        public override string getDisplayName()
        {
            return "Mechanical furnace";
        }

        public override int[] getItemInputsList()
        {
            return new int[] { ItemID.IronOre, ItemID.CopperOre, ItemID.GoldOre, ItemID.SilverOre,
                ItemID.TinOre, ItemID.LeadOre, ItemID.TungstenOre, ItemID.PlatinumOre };
        }
    }

}
