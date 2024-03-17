using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using TerraFactory.Content.Utils;
using TerraFactory.Content.Items;

namespace TerraFactory.Content.Tiles.Machines
{

    public class AbstractMachineTE : ModTileEntity
    {
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<AbstractMachine>();
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            ModTile tile = ModContent.GetModTile(type);

            if (tile == null || tile is not AbstractMachine)
                return -1;

            AbstractMachine machinetile = (AbstractMachine)tile;

            Point16 tileOrigin = new Point16(machinetile.getOriginX(), machinetile.getOriginY());
            int placedEntity = Place(i - tileOrigin.X, j - tileOrigin.Y);
            return placedEntity;
        }

        /// <summary>
        /// Inventory of this machine, in FastItemStack format.<br/>
        /// Only handles base resources, avoid puting complex item instances (such as weapons) inside machines.<br/>
        /// Added data outside the base ID and stack size will be wiped.
        /// </summary>
        public List<FastItemStack> content = new List<FastItemStack>();

        /// <summary>
        /// Adds a fastItemStack to this machine's content
        /// </summary>
        public void addFastItem(FastItemStack item)
        {
            foreach (FastItemStack stack in content)
                if (stack.itemID == item.itemID)
                {
                    stack.quantity += item.quantity;
                    return;
                }
            content.Add(item);
        }

        /// <returns>The ammount of the specific param item in this machine. 0 if it contains none.</returns>
        public int countContent(int ItemID)
        {
            int toreturn = 0;
            foreach (FastItemStack stack in content)
                if (stack.itemID == ItemID) toreturn += stack.quantity;
            return toreturn;
        }

        /// <returns>A short human readable name that describes what this machine is</returns>
        public virtual string getDisplayName()
        {
            return "Abstract machine";
        }

        /// <returns>
        /// An array of item IDs that are accepted by this machine as inputs.
        /// Doesn't garentee the machine will do anything with it, but allows players and inserters to put this type in it.
        /// </returns>
        public virtual int[] getItemInputsList()
        {
            return new int[] { ModContent.ItemType<ItemVoid>() };
        }

    }
}
