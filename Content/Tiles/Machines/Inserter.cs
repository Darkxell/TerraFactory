using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class Inserter : AbstractMachine
    {

        public override void SetStaticDefaults()
        {
            machine_Width = 2;
            machine_Height = 2;
            base.SetStaticDefaults();
        }

        protected override PlacementHook getPlacementHook()
        {
            return new PlacementHook(ModContent.GetInstance<InserterTE>().Hook_AfterPlacement, -1, 0, false); ;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<InserterTE>().Kill(i, j);
        }
    }
}
