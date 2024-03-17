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
    internal class MechanicalFurnace : AbstractMachine
    {
        public override void SetStaticDefaults()
        {
            machine_Width = 7;
            machine_Height = 8;
            base.SetStaticDefaults();
        }

        protected override PlacementHook getPlacementHook()
        {
            return new PlacementHook(ModContent.GetInstance<MechanicalFurnaceTE>().Hook_AfterPlacement, -1, 0, false); ;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<MechanicalFurnaceTE>().Kill(i, j);
        }
    }
}
