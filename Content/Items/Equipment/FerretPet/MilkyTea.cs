using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraFactory.Content.Items.Equipment.FerretPet
{
    internal class MilkyTea : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LizardEgg);

            Item.width = 30;
            Item.height = 30;

            Item.shoot = ModContent.ProjectileType<FerretPetProjectile>();
            Item.buffType = ModContent.BuffType<FerretBuff>();
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
                player.AddBuff(Item.buffType, 3600);
            return true;
        }
    }
}
