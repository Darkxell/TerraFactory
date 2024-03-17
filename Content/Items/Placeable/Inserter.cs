﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraFactory.Content.Items.Placeable
{
    internal class Inserter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;

            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Machines.Inserter>());

            Item.width = 32; // The item texture's width
            Item.height = 32; // The item texture's height

            Item.maxStack = 99;
            Item.value = Item.buyPrice(silver: 20);
        }

        public override void AddRecipes()
        {
            /*CreateRecipe()
                .AddIngredient(ItemID.IronBar, 2)
                .AddTile(TileID.WorkBenches)
                .Register();*/
        }
    }
}
