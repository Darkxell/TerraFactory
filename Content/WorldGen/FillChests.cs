using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.Items;
using TerraFactory.Content.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TerraFactory
{
    internal class FillChests : GenPass
    {
        public FillChests(float loadWeight) : base("Burrying treasure", loadWeight)
        {
        }

        // counter to generate sequential chests
        int chestcounter_desert = 0, chestcounter_ice = 0;

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null) continue;
                Tile chestTile = Main.tile[chest.x, chest.y];

                // Mineshaft barrels
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == (short)ChestTypeOffset.Barrel)
                {
                    addItem(chest, ItemID.Rope, WorldGen.genRand.Next(30, 50));
                    addItem(chest, ItemID.Torch, WorldGen.genRand.Next(5, 30));
                    addItem(chest, ItemID.WoodenArrow, WorldGen.genRand.Next(50, 200));
                    addItem(chest, ItemID.LesserHealingPotion, WorldGen.genRand.Next(3, 12));
                }

                // Mineshaft regular loot
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == (short)ChestTypeOffset.Chest)
                {
                    addItem(chest, ItemID.Grenade, WorldGen.genRand.Next(5, 50));
                    addItem(chest, ItemID.Torch, WorldGen.genRand.Next(5, 30));
                    addItem(chest, ItemID.Shuriken, WorldGen.genRand.Next(50, 200));
                    addItem(chest, ItemID.LesserHealingPotion, WorldGen.genRand.Next(3, 12));
                }

                // Mineshaft machine room loot
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == (short)ChestTypeOffset.SteampunkChest)
                {
                    addItem(chest, ModContent.ItemType<Cookbook>(), 1);
                    addItem(chest, ModContent.ItemType<MechanicalFurnace>(), 3);
                    addItem(chest, ModContent.ItemType<MiningDrill>(), 1);
                    addItem(chest, ModContent.ItemType<GunTurret>(), 1);
                    addItem(chest, ModContent.ItemType<Inserter>(), 5);
                    addItem(chest, ItemID.Extractinator, 1);
                    addItem(chest, ItemID.GoldCoin, WorldGen.genRand.Next(5, 8));
                }

                // Desert outpost loot
                if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == (short)ChestTypeOffset2.SandstoneChest)
                {
                    chestcounter_desert++;
                    if (chestcounter_desert == 1)
                    {
                        for (int i = 0; i < 2; i++)
                            addItem(chest, ItemID.SuspiciousLookingEye, WorldGen.genRand.Next(3, 5));
                        addItem(chest, ItemID.EnchantedSword, 1);
                        addItem(chest, ItemID.AmberStaff, 1);
                        addItem(chest, ItemID.AbigailsFlower, 1);
                        addItem(chest, ItemID.Musket, 1);
                        addItem(chest, ItemID.EndlessMusketPouch, 1);
                        addItem(chest, ItemID.BlandWhip, 1);
                    }
                    else
                    {
                        addItem(chest, ItemID.GladiatorHelmet, 1);
                        addItem(chest, ItemID.GladiatorBreastplate, 1);
                        addItem(chest, ItemID.GladiatorLeggings, 1);
                        addItem(chest, ItemID.SandBoots, 1);
                        addItem(chest, ItemID.LuckyHorseshoe, 1);
                    }
                }

                // Frost outpost loot
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == (short)ChestTypeOffset.FrozenChest)
                {
                    chestcounter_ice++;
                    if (chestcounter_ice == 1)
                    {
                        addItem(chest, ItemID.SlimeCrown, WorldGen.genRand.Next(3, 5));
                        addItem(chest, ItemID.IceBlade, 1);
                        addItem(chest, ItemID.IceBoomerang, 1);
                        addItem(chest, ItemID.SnowballCannon, 1);
                        addItem(chest, ItemID.WandofFrosting, 1);
                    }
                    else
                    {
                        addItem(chest, ItemID.BlizzardinaBottle, 1);
                        addItem(chest, ItemID.IceMirror, 1);
                        addItem(chest, ItemID.FlinxFurCoat, 1);
                        addItem(chest, ItemID.FlinxStaff, 1);
                    }
                }
            }
        }

        /// <returns>True if an item was added, false if it failed (meaning the chest is most likely full, or quantity was <=0)</returns>
        protected bool addItem(Chest chest, int item, int quantity)
        {
            if (quantity <= 0)
                return false;

            int[] chestshuffle = Enumerable.Range(0, Chest.maxItems).ToArray();
            chestshuffle = chestshuffle.OrderBy(x => WorldGen.genRand.Next()).ToArray();

            for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                if (chest.item[chestshuffle[inventoryIndex]].type == ItemID.None)
                {
                    chest.item[chestshuffle[inventoryIndex]].SetDefaults(item);
                    chest.item[chestshuffle[inventoryIndex]].stack = quantity;
                    return true;
                }
            return false;
        }
    }

    enum ChestTypeOffset
    {
        Chest = 0 * 36,
        GoldChest = 1 * 36,
        LockedGoldChest = 2 * 36,
        ShadowChest = 3 * 36,
        LockedShadowChest = 4 * 36,
        Barrel = 5 * 36,
        TrashCan = 6 * 36,
        EbonwoodChest = 7 * 36,
        RichMahoganyChest = 8 * 36,
        PearlwoodChest = 9 * 36,
        IvyChest = 10 * 36,
        FrozenChest = 11 * 36,
        LivingWoodChest = 12 * 36,
        SkywareChest = 13 * 36,
        ShadewoodChest = 14 * 36,
        WebCoveredChest = 15 * 36,
        LizhardChest = 16 * 36,
        WaterChest = 17 * 36,
        JungleChest = 18 * 36,
        CorruptionChest = 19 * 36,
        CrimsonChest = 20 * 36,
        HallowedChest = 21 * 36,
        IceChest = 22 * 36,
        LockedJungleChest = 23 * 36,
        LockedCorruptionChest = 24 * 36,
        LockedCrimsonChest = 25 * 36,
        LockedHallowChest = 26 * 36,
        LockedIceChest = 27 * 36,
        DynastyChest = 28 * 36,
        HoneyChest = 29 * 36,
        SteampunkChest = 30 * 36,
        PalmWoodChest = 31 * 36,
        MushroomChest = 32 * 36,
        BorealWoodChest = 33 * 36,
        SlimeChest = 34 * 36,
        GreenDungeonChest = 35 * 36,
        LockedGreenDungeonChest = 36 * 36,
        PinkDungeonChest = 37 * 36,
        LockedPinkDungeonChest = 38 * 36,
        BlueDungeonChest = 39 * 36,
        LockedBlueDungeonChest = 40 * 36,
        BoneChest = 41 * 36,
        CactusChest = 42 * 36,
        FleshChest = 43 * 36,
        ObsidianChest = 44 * 36,
        PumpkinChest = 45 * 36,
        SpookyChest = 46 * 36,
        GlassChest = 47 * 36,
        MartianChest = 48 * 36,
        MeteoriteChest = 49 * 36,
        GraniteChest = 50 * 36,
        MarbleChest = 51 * 36,
        CrystalChestUnused = 52 * 36,
        GoldenChestUnused = 53 * 36
    }

    enum ChestTypeOffset2
    {
        CrystalChest = 0 * 36,
        GoldenChest = 1 * 36,
        LesionChest = 2 * 36,
        SpiderChest = 3 * 36,
        DeadManChest = 4 * 36,
        SolarChest = 5 * 36,
        VortexChest = 6 * 36,
        NebulaChest = 7 * 36,
        StardustChest = 8 * 36,
        GolfChest = 9 * 36,
        SandstoneChest = 10 * 36,
        BambooChest = 11 * 36,
        DesertChest = 12 * 36,
        DesertChestLocked = 13 * 36,
        ReefChest = 14 * 36,
        BallonChest = 15 * 36,
        AshWoodChest = 16 * 36
    }
}
