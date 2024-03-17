using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerraFactory.Content.Tiles.HardOres
{
    internal class HardLead : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 230; //  https://terraria.wiki.gg/wiki/Metal_Detector
            Main.tileShine[Type] = Main.tileShine[TileID.Lead];
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(152, 171, 198), name);

            DustType = DustID.Lead;
            HitSound = SoundID.Tink;
            MineResist = 10f;
            MinPick = 10_000;
        }
    }
}
