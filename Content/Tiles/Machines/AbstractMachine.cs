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
using TerraFactory.Content.Utils;
using TerraFactory.Content.UserInterface;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class AbstractMachine : ModTile
    {
        public int machine_Width = 4, machine_Height = 4;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileNoAttach[Type] = true;

            TileObjectData.newTile.Origin = new Point16(getOriginX(), getOriginY());
            TileObjectData.newTile.Width = machine_Width;
            TileObjectData.newTile.Height = machine_Height;
            TileObjectData.newTile.CoordinateHeights = new int[machine_Height];
            for (int i = 0; i < machine_Height; i++) TileObjectData.newTile.CoordinateHeights[i] = 16;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 0;
            TileObjectData.newTile.DrawYOffset = 0;

            TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.WaterDeath = false;
            TileObjectData.newTile.LavaDeath = false;

            TileObjectData.newTile.StyleLineSkip = 9; // This needs to be added to work for modded tiles.

            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorInvalidTiles = new int[] {
                TileID.MagicalIceBlock,
                TileID.Boulder,
                TileID.BouncyBoulder,
                TileID.LifeCrystalBoulder,
                TileID.RollingCactus
            };

            TileObjectData.newTile.HookPostPlaceMyPlayer = getPlacementHook();
            TileObjectData.newTile.UsesCustomCanPlace = true;

            TileObjectData.addTile(Type);

            DustType = DustID.Iron;

            AddMapEntry(new Color(16, 183, 252), Language.GetText("TerraFactoryMachine"));
        }

        public int getOriginX()
        {
            return machine_Width / 2;
        }

        public int getOriginY()
        {
            return machine_Height - 1;
        }

        protected virtual PlacementHook getPlacementHook()
        {
            return new PlacementHook(ModContent.GetInstance<AbstractMachineTE>().Hook_AfterPlacement, -1, 0, false); ;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<AbstractMachineTE>().Kill(i, j);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;

            int style = TileObjectData.GetTileStyle(Main.tile[i, j]);
            player.cursorItemIconID = TileLoader.GetItemDropFromTypeAndStyle(Type, style);
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }

        public override bool RightClick(int i, int j)
        {
            if (MiscUtils.TryGetTileEntityAs(i, j, out AbstractMachineTE entity))
            {

                if (entity is GunTurretTE)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Turret ammo : " + ((GunTurretTE)entity).ammo + "/" + ((GunTurretTE)entity).max_ammo);
                    Main.NewText(sb.ToString());
                }

                // Opens the Machine side UI
                GlobalMachineUI ui = ModContent.GetInstance<GlobalMachineUI>();
                if (!ui.IsShowing())
                {
                    ui.ShowMachineUI(entity);
                }
                else
                {
                    if (ui.IsShowing(entity))
                        ui.HideMachineUI();
                    else
                        ui.ShowMachineUI(entity);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            int frame = Main.tileFrame[Type];
            frameYOffset = machine_Height * 16 * frame;
        }
    }
}
