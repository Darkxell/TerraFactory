using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using static Humanizer.In;
using static Humanizer.On;

namespace TerraFactory.Content.Menu
{
    internal class TerraFactoryMenuodMenu : ModMenu
    {
        private const string menuAssetPath = "TerraFactory/Content/Menu";

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>(menuAssetPath + "/MenuLogo");

        // public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>(menuAssetPath + "/MenuSun");
        // public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>(menuAssetPath + "/MenuMoon");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Music/PeaceReturns");

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<MainMenuBackgroundStyle>();

        public override string DisplayName => "TerraFactory ModMenu";

        public override void OnSelected()
        {
            SoundEngine.PlaySound(SoundID.NPCHit53);
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("TerraFactory/Content/Menu/MetalSheet", AssetRequestMode.AsyncLoad).Value;
            spriteBatch.Draw(tex, logoDrawCenter + new Vector2(tex.Width / -2, 30), Color.White);

            return base.PreDrawLogo(spriteBatch, ref logoDrawCenter, ref logoRotation, ref logoScale, ref drawColor);
        }

        public override void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
        {
            base.PostDrawLogo(spriteBatch, logoDrawCenter, logoRotation, logoScale, drawColor);
        }
    }

}
