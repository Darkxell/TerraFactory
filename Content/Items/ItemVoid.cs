using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraFactory.Content.Items
{
    internal class ItemVoid : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Gray;

            Item.width = 40;
            Item.height = 40;

            Item.maxStack = 1;
            Item.value = Item.buyPrice(silver: 1);
        }

    }
}
