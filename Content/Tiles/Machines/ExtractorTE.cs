using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using System.Diagnostics.Metrics;
using TerraFactory.Content.Utils;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class ExtractorTE : AbstractMachineTE
    {
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<Extractor>();
        }

        int counter = 0;
        public override void Update()
        {
            counter++;
            if (counter < 20)
                return;
            counter = 0;

            // Flags to keep track with side(s) interacted, to do the animation
            bool interactedleft = false, interactedright = false;

            // Finds the entity left and the entity right
            AbstractMachineTE machineleft = null, machineright = null;
            Point16 leftcheck = MiscUtils.GetTopLeftTileInMultitile(Position.X - 1, Position.Y + 1);
            if (MiscUtils.TryGetTileEntityAs(leftcheck.X, leftcheck.Y, out AbstractMachineTE entity1))
                if (entity1 is not InserterTE && entity1 is not ExtractorTE)
                    machineleft = entity1;
            Point16 rightcheck = MiscUtils.GetTopLeftTileInMultitile(Position.X + 2, Position.Y + 1);
            if (MiscUtils.TryGetTileEntityAs(rightcheck.X, rightcheck.Y, out AbstractMachineTE entity2))
                if (entity2 is not InserterTE && entity2 is not ExtractorTE)
                    machineright = entity2;

            // Take from the adjacent machines
            if (machineleft != null)
            {
                foreach (FastItemStack sl in machineleft.content)
                {
                    addFastItem(new FastItemStack(sl.itemID, sl.quantity));
                    interactedleft = true;
                }
                machineleft.content.Clear();
            }
            if (machineright != null)
            {
                foreach (FastItemStack sl in machineright.content)
                {
                    addFastItem(new FastItemStack(sl.itemID, sl.quantity));
                    interactedright = true;
                }
                machineright.content.Clear();
            }

            // Plays a soft anim if it has taken something this tick
            Vector2 vfxAnchor = new Vector2(Position.X + 1, Position.Y) * 16;
            if (interactedleft)
                Dust.QuickBox(vfxAnchor + new Vector2(-2*16, 0), vfxAnchor + new Vector2(-1 * 16, 3 * 16), 3, Color.Pink, null);
            if (interactedright)
                Dust.QuickBox(vfxAnchor + new Vector2(1 * 16, 0), vfxAnchor + new Vector2(2 * 16, 3 * 16), 3, Color.Pink, null);

            // TODO : put the items in the pipe(s?) connected to this extractor
            // [...]
        
        }

        public override string getDisplayName()
        {
            return "Extractor";
        }

    }
}
