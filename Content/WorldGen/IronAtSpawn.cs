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
using Terraria.DataStructures;

namespace TerraFactory
{

    public class IronAtSpawn : GenPass
    {
        public IronAtSpawn(float loadWeight) : base("Adding a starter lead patch", loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = Name;

            int patchX = GenData.spawnX - WorldGen.genRand.Next(20,150);
            int patchY = GenData.spawnY;

            StructureHelper.Generator.GenerateStructure(
                                "Content/Structures/bantia-ironpatch",
                                new Point16(patchX, patchY),
                                ModContent.GetInstance<TerraFactory>());
        }
    }


}
