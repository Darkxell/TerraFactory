using Microsoft.Xna.Framework.Input;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using TerraFactory.Content.WorldGen;

namespace TerraFactory
{

    internal class WorldGenTerraFactory : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            // Removes all non reset generation passes
            // Reset needs to stay because it sets important variables in GenVars
            List<string> passwhitelist = new List<string> { "Reset", "Terrain" };
            tasks.RemoveAll(entry => !passwhitelist.Contains(entry.Name));

            // Add all TerraFactory gen passes
            tasks.Add(new WorldSetup(100f));
            tasks.Add(new FlatteningDirt(100f));
            tasks.Add(new PlanetWalls(100f));

            // Bantia generation steps
            tasks.Add(new BantiaHill(100f));
            tasks.Add(new BantiaGrass(100f));
            tasks.Add(new BantiaDesert(100f));
            tasks.Add(new BantiaMineshaft(100f));
            tasks.Add(new IronAtSpawn(100f));
            tasks.Add(new BantiaTrees(100f));
            tasks.Add(new FillChests(100f));

            // Caliris generation steps
            // ---

            // Erebos generation steps
            // ---

            // Post generation touches
            tasks.Add(new HardenOres(100f));
        }

        public override void PostUpdateWorld()
        {
            if (MiscUtils.JustPressed(Keys.D1))
                TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
        }

        private void TestMethod(int x, int y)
        {
            Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);

            // Code to test placed here:
            // WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.CobaltBrick);
        }



    }
}
