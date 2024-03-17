using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraFactory.Content.WorldGen;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TerraFactory
{
    internal class BantiaDesert : GenPass
    {

        public BantiaDesert(float loadWeight) : base("Drying Bantia", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            // Replaces all dirt on the desert side with sand
            for (int i = GenData.Bantia_start; i < GenData.Bantia_DesertEdge - 2; i++)
                for (int j = 0; j < GenData.worldHeight - 200; j++)
                {
                    if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == TileID.Dirt)
                    {
                        WorldGen.PlaceTile(i, j, TileID.Sand, mute: true, forced: true);
                    }
                }

            // Adds the desert outpost structure
            StructureHelper.Generator.GenerateStructure(
              "Content/Structures/desert-outpost",
              new Point16(GenData.Bantia_DesertEdge - 150, GenData.surface - 32),
              ModContent.GetInstance<TerraFactory>());

            // Adds surface cacti
            for (int i = GenData.Bantia_start; i < GenData.Bantia_DesertEdge - 2; i++)
                for (int j = GenData.surface - 5; j < GenData.surface + 5; j++)
                {
                    if (WorldGen.genRand.Next(0, 20) == 0)
                        WorldGen.PlantCactus(i, j);
                }


        }
    }
}
