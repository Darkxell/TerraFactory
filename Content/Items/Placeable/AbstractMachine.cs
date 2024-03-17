using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraFactory.Content.Items.Placeable
{
    /// <summary>
    /// A placeholder unobtainable machine to place a default abstract machine down.
    /// Has no use outside of debug.
    /// </summary>
    internal class AbstractMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 0;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;

            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Machines.AbstractMachine>());

            Item.width = 64; // The item texture's width
            Item.height = 64; // The item texture's height

            Item.maxStack = 99;
            Item.value = Item.buyPrice(copper: 1);
        }

        public override void AddRecipes()
        {
            // No recepies, debug item
        }

    }
}
