using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using TerraFactory.Content.Utils;
using Steamworks;
using Microsoft.Xna.Framework;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class InserterTE : AbstractMachineTE
    {
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<Inserter>();
        }

        /// <summary>
        /// The target amount of each resource this inserter tries to put in each machine.<br/>
        /// The inserter will also not pull from machines having less than the target amount<br/>
        /// It will also not pull resources if it already has more than the target amount in stock itself
        /// </summary>
        private int TARGET_AMOUNT = 5;

        int counter = 0;
        public override void Update()
        {
            counter++;
            if (counter < 30)
                return;
            counter = 0;

            // Flags to keep track with side(s) interacted, to do the animation
            bool interactedleft = false, interactedright = false;

            // Finds the entity left and the entity right
            AbstractMachineTE machineleft = null, machineright = null;
            Point16 leftcheck = MiscUtils.GetTopLeftTileInMultitile(Position.X - 1, Position.Y);
            if (MiscUtils.TryGetTileEntityAs(leftcheck.X, leftcheck.Y, out AbstractMachineTE entity1))
                if (entity1 is not InserterTE)
                    machineleft = entity1;
            Point16 rightcheck = MiscUtils.GetTopLeftTileInMultitile(Position.X + 2, Position.Y);
            if (MiscUtils.TryGetTileEntityAs(rightcheck.X, rightcheck.Y, out AbstractMachineTE entity2))
                if (entity2 is not InserterTE)
                    machineright = entity2;

            // Takes what's above the target amount in adjacent machines
            // But only if it needs to
            if (machineleft != null)
                foreach (FastItemStack sl in machineleft.content)
                    if (sl.quantity > TARGET_AMOUNT && countContent(sl.itemID) < TARGET_AMOUNT)
                    {
                        int transfer = Math.Min(sl.quantity - TARGET_AMOUNT, TARGET_AMOUNT - countContent(sl.itemID));
                        sl.quantity -= transfer;
                        addFastItem(new FastItemStack(sl.itemID, transfer));
                        interactedleft = true;
                    }

            if (machineright != null)
                foreach (FastItemStack sr in machineright.content)
                    if (sr.quantity > TARGET_AMOUNT && countContent(sr.itemID) < TARGET_AMOUNT)
                    {
                        int transfer = Math.Min(sr.quantity - TARGET_AMOUNT, TARGET_AMOUNT - countContent(sr.itemID));
                        sr.quantity -= transfer;
                        addFastItem(new FastItemStack(sr.itemID, transfer));
                        interactedright = true;
                    }

            // Puts what the adjacent machines need in them
            foreach (FastItemStack stk in content)
            {
                if (stk.quantity <= 0) continue;

                if (machineleft != null)
                {
                    int currentmachineleft = machineleft.countContent(stk.itemID);
                    if (currentmachineleft < TARGET_AMOUNT)
                    {
                        int tomove = Math.Min(stk.quantity, TARGET_AMOUNT - currentmachineleft);
                        if (tomove >= 1)
                        {
                            machineleft.addFastItem(new FastItemStack(stk.itemID, tomove));
                            stk.quantity -= tomove;
                            interactedleft = true;
                        }
                    }
                }

                if (machineright != null)
                {
                    int currentmachineright = machineright.countContent(stk.itemID);
                    if (currentmachineright < TARGET_AMOUNT)
                    {
                        int tomove = Math.Min(stk.quantity, TARGET_AMOUNT - currentmachineright);
                        if (tomove >= 1)
                        {
                            machineright.addFastItem(new FastItemStack(stk.itemID, tomove));
                            stk.quantity -= tomove;
                            interactedright = true;
                        }
                    }
                }
            }

            // Removes the empty stacks from the inserter
            content.RemoveAll(stk => stk.quantity <= 0);

            // Spawns dust on the interacted containers.
            // TODO : replace this with a cool mechanical arm vfx
            Vector2 vfxAnchor = new Vector2(Position.X + 1, Position.Y + 1) * 16;
            if (interactedleft && machineleft != null)
            {
                Dust.QuickDustLine(vfxAnchor, vfxAnchor - new Vector2(48, 0), 8, Color.WhiteSmoke);
            }

            if (interactedright && machineright != null)
            {
                Dust.QuickDustLine(vfxAnchor, vfxAnchor + new Vector2(48, 0), 8, Color.WhiteSmoke);
            }
        }
        public override string getDisplayName()
        {
            return "Inserter";
        }
    }
}
