using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class MiningDrill : AbstractMachine
    {
        public override void SetStaticDefaults()
        {
            machine_Width = 7;
            machine_Height = 4;
            base.SetStaticDefaults();
        }

        protected override PlacementHook getPlacementHook()
        {
            return new PlacementHook(ModContent.GetInstance<MiningDrillTE>().Hook_AfterPlacement, -1, 0, false); ;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<MiningDrillTE>().Kill(i, j);
        }

        private int[] animation = new int[] { 0, 1, 2, 3, 4, 6, 4, 6, 4, 6, 4, 3, 2, 1, 0, 5, 0, 5, 0, 5 };
        private int current_animframe = 0;

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 9)
            {
                if (++current_animframe >= animation.Length) current_animframe = 0;
                frameCounter = 0;
                frame = animation[current_animframe];
            }
        }
    }

}
