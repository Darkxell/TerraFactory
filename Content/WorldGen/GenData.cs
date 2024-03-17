using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraFactory.Content.WorldGen
{
    public static class GenData
    {
        public static int worldWidth = 0;
        public static int worldHeight = 0;

        public static int surface = 0;

        public static int spawnX = 0;
        public static int spawnY = 0;

        public static int Caliris_start = 0;
        public static int Caliris_end = 0;
        public static int Bantia_start = 0;
        public static int Bantia_end = 0;
        public static int Erebos_start = 0;
        public static int Erebos_end = 0;

        public static int Bantia_MineshaftEntrance = 0;
        public static int Bantia_DesertEdge = 0;

        public static void print()
        {
            Console.WriteLine("Generating a new TerraFactory world, welcome!\n"
                + "Width:" + worldWidth + " / Height:" + worldHeight + "\n"
                + "Spawn X:" + spawnX + " / Spawn Y:" + spawnY + "\n"
                + "Surface height:" + surface);
        }

        /// <summary>
        /// A way to get the world size, not breaking if terraria ever adds new/custom configurations.
        /// Works with world enlargers, anything bigger than a large world will return as large.
        /// </summary>
        public static WorldSize estimateWorldSize()
        {
            if (worldWidth <= 5_000) return WorldSize.Small;
            if (worldWidth <= 7_500) return WorldSize.Medium;
            return WorldSize.Large;
        }

    }

    public enum WorldSize
    {
        Small = 1, Medium = 2, Large = 3
    }
}
