using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Mono.Cecil;

namespace TerraFactory.Content.Items.Weapons
{
    internal class DebugSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.damage = 100;
            Item.knockBack = 4.5f;
            Item.width = 40;
            Item.height = 40;
            Item.scale = 1f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(copper: 50);
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ProjectileID.SwordBeam;
            Item.noMelee = true; // Melee hits do nothing, only projectile does
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.

            position = player.MountedCenter;

            velocity = Main.MouseWorld - position;
            velocity.Normalize();
            velocity *= 7f;

            Projectile.NewProjectile(
                spawnSource: source,
                position: position,
                velocity: velocity,
                Type: type,Damage: damage, KnockBack: knockback,
                Owner: player.whoAmI);

            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }

    }
}
