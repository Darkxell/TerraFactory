using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerraFactory.Content.Tiles.HardOres
{
    /// <summary>
    /// An unbreakable pitch black block, meant for world borders
    /// </summary>
    internal class TrueVoid : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;

            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileNoAttach[Type] = true;

            DustType = DustID.Asphalt;
            HitSound = SoundID.Tink;
            MineResist = 10f;
            MinPick = 10_000;

            AddMapEntry(Color.Black);
        }
    }
}
