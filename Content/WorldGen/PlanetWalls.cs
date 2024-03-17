using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.ID;
using System.Diagnostics;
using TerraFactory.Content.WorldGen;
using TerraFactory.Content.Tiles.HardOres;

namespace TerraFactory
{
    internal class PlanetWalls : GenPass
    {

        public PlanetWalls(float loadWeight) : base("Creating different planets", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            // Barrier between bantia and caliris
            for (int i = GenData.Caliris_end; i < GenData.Bantia_start; i++)
                for (int j = 0; j < GenData.worldHeight; j++)
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<TrueVoid>(), mute: true, forced: true);
                }
            // Barrier between bantia and erebos
            for (int i = GenData.Bantia_end; i < GenData.Erebos_start; i++)
                for (int j = 0; j < GenData.worldHeight; j++)
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<TrueVoid>(), mute: true, forced: true);
                }
            // Barrier between erebos and the right side ocean
            for (int i = GenData.Erebos_end; i < GenData.worldWidth; i++)
                for (int j = 0; j < GenData.worldHeight; j++)
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<TrueVoid>(), mute: true, forced: true);
                }
        }
    }
}
