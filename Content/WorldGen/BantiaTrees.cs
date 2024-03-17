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
    internal class BantiaTrees : GenPass
    {
        public BantiaTrees(float loadWeight) : base("Planting trees on Bantia", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            // Adds large background trees with structures
            for (int i = GenData.Bantia_DesertEdge; i < GenData.Bantia_MineshaftEntrance - 50; i++)
            {
                if (WorldGen.genRand.Next(0, 3) == 0)
                {
                    int tree = WorldGen.genRand.Next(0, 2);
                    switch (tree)
                    {
                        case 0:
                            StructureHelper.Generator.GenerateStructure(
                                "Content/Structures/bantia-tree-51x55-green",
                                new Point16(i, GenData.surface - 55),
                                ModContent.GetInstance<TerraFactory>());
                            i += 51;
                            break;
                        case 1:
                            StructureHelper.Generator.GenerateStructure(
                                "Content/Structures/bantia-tree-22x44-orange",
                                new Point16(i, GenData.surface - 44),
                                ModContent.GetInstance<TerraFactory>());
                            i += 22;
                            break;
                    }
                }
            }

            // Adds vanilla type trees on the foreground
            for (int i = GenData.Bantia_DesertEdge; i < GenData.Bantia_MineshaftEntrance - 10; i++)
                for (int j = GenData.surface - 5; j < GenData.surface + 5; j++)
                {
                    if (WorldGen.genRand.Next(0, 5) == 0)
                    {
                        int saplingID = TileID.Saplings;
                        switch (WorldGen.genRand.Next(0, 4))
                        {
                            case 0: saplingID = TileID.VanityTreeSakuraSaplings; break;
                            case 1: saplingID = TileID.VanityTreeWillowSaplings; break;
                        }
                        bool sapling = WorldGen.PlaceObject(i, j, saplingID, mute: true, style: 0);
                        bool grown = WorldGen.AttemptToGrowTreeFromSapling(i, j, false);
                        if (sapling && !grown)
                        {
                            WorldGen.KillTile(i, j, noItem: true);
                        }
                    }
                    if (WorldGen.genRand.Next(0, 40) == 0)
                    {
                        WorldGen.PlaceObject(i, j, TileID.Sunflower, mute: true, style: 0);
                    }
                }

            // Adds boreal trees on the right hills
            // Not a lot of them, the terrain there is usually too hilly for saplings to grow
            for (int i = GenData.Bantia_MineshaftEntrance + 50; i < GenData.Bantia_end - 50; i++)
                for (int j = 0; j < GenData.surface + 5; j++)
                {
                    if (WorldGen.genRand.Next(0, 5) == 0)
                    {
                        int saplingID = TileID.Saplings;
                        bool sapling = WorldGen.PlaceObject(i, j, saplingID, mute: true, style: 0);
                        bool grown = WorldGen.AttemptToGrowTreeFromSapling(i, j, false);
                        if (sapling && !grown)
                        {
                            WorldGen.KillTile(i, j, noItem: true);
                        }
                    }
                }

        }
    }
}
