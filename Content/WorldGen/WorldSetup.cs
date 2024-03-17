using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.WorldGen;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace TerraFactory
{
    public class WorldSetup : GenPass
    {
        public WorldSetup(float loadWeight) : base("Making space for an epic factory", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            GenData.worldWidth = Main.maxTilesX;
            GenData.worldHeight = Main.maxTilesY;

            // GenVars.worldSurface = 500;
            // GenVars.worldSurfaceLow = GenVars.worldSurface - 50;
            // GenVars.worldSurfaceHigh = GenVars.worldSurface + 50;
            GenData.surface = (int)GenVars.worldSurface;

            GenData.spawnX = GenData.worldWidth / 2;
            GenData.spawnY = GenData.surface - 5;
            Main.spawnTileX = GenData.spawnX;
            Main.spawnTileY = GenData.spawnY;

            GenData.Caliris_start = 0;
            GenData.Caliris_end = GenData.worldWidth / 4;
            GenData.Bantia_start = GenData.Caliris_end + 80;
            GenData.Bantia_end = GenData.worldWidth - (GenData.worldWidth / 5 * 2);
            GenData.Erebos_start = GenData.Bantia_end + 80;
            GenData.Erebos_end = GenData.worldWidth - 450;

            GenData.Bantia_MineshaftEntrance = GenData.spawnX + WorldGen.genRand.Next(120, 200);
            GenData.Bantia_DesertEdge = GenData.spawnX - (GenData.spawnX - GenData.Bantia_start) / 3 + WorldGen.genRand.Next(-30, 30);

            GenData.print();
        }
    }
}
