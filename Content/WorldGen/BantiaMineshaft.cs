using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.WorldGen;
using Terraria;
using Terraria.DataStructures;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TerraFactory
{
    internal class BantiaMineshaft : GenPass
    {
        public BantiaMineshaft(float loadWeight) : base("Abandoning mines", loadWeight)
        {
        }

        /// <summary>
        /// The coordinates of the start of the mineshaft. Set by the currently applying pass
        /// </summary>
        int mineshaftX, mineshaftY;

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            mineshaftX = GenData.Bantia_MineshaftEntrance;
            mineshaftY = GenData.surface - 14;

            spawnMineshaftBlock(0, 0, "Content/Structures/mineshaft-entrance");
            spawnMineshaftBlock(1, 0, "Content/Structures/mineshaft-elevator-top");

            spawnMineshaftBlock(1, 1, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(1, 2, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(1, 3, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(1, 4, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(1, 5, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(1, 6, "Content/Structures/mineshaft-chainwell");

            for (int i = -10; i < 5; i++)
                spawnMineshaftBlock(i, 7, i == 1 ? "Content/Structures/mineshaft-corridor-chainwell" : getRandCorridor());

            spawnMineshaftBlock(5, 7, "Content/Structures/mineshaft-gemroom-x2");
            spawnMineshaftBlock(-13, 7, "Content/Structures/mineshaft-machineroom-x3");

            spawnMineshaftBlock(-14, 8, getRandCorridor());
            spawnMineshaftBlock(-15, 8, getRandCorridor());
            spawnMineshaftBlock(-16, 8, "Content/Structures/mineshaft-chainwelltop");
            spawnMineshaftBlock(-17, 8, getRandCorridor());
            spawnMineshaftBlock(-18, 8, getRandCorridor());
            spawnMineshaftBlock(-19, 8, getRandCorridor());
            spawnMineshaftBlock(-20, 8, getRandCorridor());

            List<string> orerooms = new List<string>();
            orerooms.Add("Content/Structures/mineshaft-patch-copper");
            orerooms.Add("Content/Structures/mineshaft-patch-tin");
            orerooms.Add("Content/Structures/mineshaft-patch-iron");
            suffleList(orerooms);

            spawnMineshaftBlock(-22, 8, orerooms[0]);

            spawnMineshaftBlock(-16, 9, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(-16, 10, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(-16, 11, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(-16, 12, "Content/Structures/mineshaft-chainwell");
            spawnMineshaftBlock(-16, 13, "Content/Structures/mineshaft-corridor-chainwell");
            spawnMineshaftBlock(-17, 13, getRandCorridor());
            spawnMineshaftBlock(-15, 13, getRandCorridor());
            spawnMineshaftBlock(-14, 13, getRandCorridor());
            spawnMineshaftBlock(-13, 13, getRandCorridor());

            spawnMineshaftBlock(-19, 13, orerooms[1]);
            spawnMineshaftBlock(-12, 13, orerooms[2]);
        }

        private void spawnMineshaftBlock(int x, int depth, string block)
        {
            StructureHelper.Generator.GenerateStructure(
               block,
               new Point16(mineshaftX + 16 * x, mineshaftY + depth * 16),
               ModContent.GetInstance<TerraFactory>());
        }

        private string getRandCorridor()
        {
            switch (WorldGen.genRand.Next(0, 5))
            {
                case 0: return "Content/Structures/mineshaft-corridor-diamonds";
                case 1: return "Content/Structures/mineshaft-corridor-loot";
                case 2: return "Content/Structures/mineshaft-corridor-rail";
                case 3: return "Content/Structures/mineshaft-corridor-trap";
                default: return "Content/Structures/mineshaft-corridor-barrels";
            }
        }

        private void suffleList(List<string> toshuffle)
        {
            int n = toshuffle.Count;
            while (n > 1)
            {
                n--;
                int k = WorldGen.genRand.Next(n + 1);
                string value = toshuffle[k];
                toshuffle[k] = toshuffle[n];
                toshuffle[n] = value;
            }
        }

    }
}
