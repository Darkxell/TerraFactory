using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class GunTurret : AbstractMachine
    {
        public override void SetStaticDefaults()
        {
            machine_Width = 4;
            machine_Height = 4;
            base.SetStaticDefaults();
        }

        protected override PlacementHook getPlacementHook()
        {
            return new PlacementHook(ModContent.GetInstance<GunTurretTE>().Hook_AfterPlacement, -1, 0, false); ;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<GunTurretTE>().Kill(i, j);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            MiscUtils.TryGetTileEntityAs(i, j, out GunTurretTE gunTE);
            if (gunTE.Position.X != i || gunTE.Position.Y != j)
                return;

            if (gunTE != null)
            {
                float drawDirection = gunTE.direction.X;

                Texture2D tex = ModContent.Request<Texture2D>("TerraFactory/Content/Tiles/Machines/GunTurret_head", AssetRequestMode.AsyncLoad).Value;

                Color drawColor = Lighting.GetColor(i, j);
                Vector2 screenOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
                Vector2 drawOffset = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + screenOffset;
                drawOffset.Y -= 16f;
                drawOffset.X += drawDirection > 0 ? 32f : 32f;
                SpriteEffects sfx = drawDirection > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                spriteBatch.Draw(texture: tex,
                    position: drawOffset,
                    sourceRectangle: null,
                    color: drawColor,
                    rotation: gunTE.getRotation() + (drawDirection < 0f ? (float)Math.PI : 0f),
                    origin: new Vector2(32, 32),
                    scale: 1f,
                    effects: sfx,
                    layerDepth: 0);
            }

        }

    }
}
