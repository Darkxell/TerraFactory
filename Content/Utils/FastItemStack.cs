using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraFactory.Content.Utils
{
    /// <summary>
    /// A very small structure to describe a stack of items. Only contains the ID of the item and an integer stack size.
    /// Fast, meant for machine TileEntities.
    /// </summary>
    public class FastItemStack
    {
        public int itemID, quantity;

        public FastItemStack(int itemID, int quantity) { 
            this.itemID = itemID;
            this.quantity = quantity;
        }

        /// <returns>A visual representation of this item stack, to be used in chat display.</returns>
        public string toString() {
            return quantity + "x[i:" + itemID + "]";
        }
    }
}
