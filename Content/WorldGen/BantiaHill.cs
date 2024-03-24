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

namespace TerraFactory;
internal class BantiaHill : GenPass
{

    public BantiaHill(float loadWeight) : base("Subducting Bantia's plates", loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = Name;

        float curveheight = GenData.surface - 20;
        float curveVelocity = -2f;

        float snowlayer = GenData.surface / 5 * 4;
        float icelayer = GenData.surface / 3;

        // Snow/ice gradients
        float gradient_snow = 20, gradient_ice = 80;

        // memory of the most flat position generated
        // Uses the fuirthest to the east in case of equality
        int mostflatX = GenData.Bantia_MineshaftEntrance + 100;
        float mostflatvalue = 100f, mostflatheight = curveheight;

        // places the hilly terrain
        for (int i = GenData.Bantia_MineshaftEntrance; i < GenData.Bantia_end; i++)
        {
            for (int j = 0; j < Math.Max(GenData.surface, snowlayer + gradient_snow); j++)
            {
                // Place the tiles below the curves for this column
                if (j > curveheight)
                {
                    if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == TileID.Stone) 
                        continue;

                    if (j < icelayer)
                    {
                        WorldGen.PlaceTile(i, j, TileID.IceBlock, mute: true, forced: true);
                    }
                    else if (j < icelayer + gradient_ice)
                    {
                        float blend = ((float)(j - icelayer)) / gradient_ice;
                        WorldGen.PlaceTile(i, j, blend < WorldGen.genRand.NextFloat() ? TileID.IceBlock : TileID.SnowBlock, mute: true, forced: true);
                    }
                    else if (j < snowlayer)
                    {
                        WorldGen.PlaceTile(i, j, TileID.SnowBlock, mute: true, forced: true);
                    }
                    else if (j < snowlayer + gradient_snow)
                    {
                        float blend = ((float)(j - snowlayer)) / gradient_snow;
                        WorldGen.PlaceTile(i, j, blend < WorldGen.genRand.NextFloat() ? TileID.SnowBlock : TileID.Dirt, mute: true, forced: true);
                    }
                    else
                    {
                        WorldGen.PlaceTile(i, j, TileID.Dirt, mute: true, forced: true);
                    }
                }
            }

            // Deepens the snow after marching forward
            if (i >= GenData.Bantia_MineshaftEntrance + 100)
            {
                snowlayer += 0.5f;
                icelayer += 0.2f;

                if (i > GenData.Bantia_MineshaftEntrance + 200) {
                    if (i < GenData.Bantia_MineshaftEntrance + 210)
                        snowlayer += 3f;
                    else if (i < GenData.Bantia_MineshaftEntrance + 220)
                        snowlayer += 10f;
                    else if (i < GenData.Bantia_MineshaftEntrance + 230)
                        snowlayer += 4f;
                } 
                    
                
            }

            // Change the curve velocity 
            if (i > GenData.Bantia_MineshaftEntrance + 20 && i < GenData.Bantia_MineshaftEntrance + 70)
            {
                // At the start but after the initial bump, just slowly go up
                curveVelocity = WorldGen.genRand.NextFloat() * -2f;

            }
            else if (i > GenData.Bantia_end - 100)
            {
                // When nearing the world edge, start going much steeper
                curveVelocity -= 0.1f;
            }
            else
            {
                // Between the mineshaft and the start of the steep hill, ondulate
                if (curveVelocity >= 1f)
                    curveVelocity -= 0.2f;
                else if (curveVelocity <= -1f)
                    curveVelocity += 0.2f;
                else
                    curveVelocity += (WorldGen.genRand.NextFloat() - 0.5f) / 2f;
            }

            curveheight += curveVelocity;
            if (curveheight >= GenData.surface)
                curveheight = GenData.surface;


            if ((Math.Abs(curveVelocity) <= 0.1f || Math.Abs(curveVelocity) <= mostflatvalue) && mostflatX < i)
            {
                mostflatvalue = Math.Abs(curveVelocity);
                mostflatX = i;
                mostflatheight = curveheight;
            }
        }


        // Adds the ice dungeon location
        int icedungeonX = mostflatX - 29, icedungeonY = (int)mostflatheight - 29;
        StructureHelper.Generator.GenerateStructure(
            "Content/Structures/snow-outpost",
            new Point16(icedungeonX, icedungeonY),
            ModContent.GetInstance<TerraFactory>());
        icedungeonY += 53;
        icedungeonX -= 10;
        // Adds the ice caves below
        int layers = 0;
        switch (GenData.estimateWorldSize())
        {
            case WorldSize.Small: layers = 3; break;
            case WorldSize.Medium: layers = 5; break;
            case WorldSize.Large: layers = 7; break;
        }
        for (int i = 0; i < layers; i++)
        {
            StructureHelper.Generator.GenerateStructure(
                "Content/Structures/icecaves-cave",
                new Point16(icedungeonX, icedungeonY + 35 * i),
                ModContent.GetInstance<TerraFactory>());
        }
        // Connects the ice caves together with ice wells
        StructureHelper.Generator.GenerateStructure(
                "Content/Structures/icecaves-shaft",
                new Point16(icedungeonX + 15, icedungeonY),
                ModContent.GetInstance<TerraFactory>());
        StructureHelper.Generator.GenerateStructure(
                "Content/Structures/icecaves-shaft",
                new Point16(icedungeonX + 15, icedungeonY + 5),
                ModContent.GetInstance<TerraFactory>());
        for (int i = 0; i < layers; i++)
        {
            int wellposition = WorldGen.genRand.Next(10, 90);
            for (int j = 0; j < 4; j++)
            {
                StructureHelper.Generator.GenerateStructure(
                    "Content/Structures/icecaves-shaft",
                    new Point16(icedungeonX + wellposition, icedungeonY + (35 * (i + 1)) + (j * 5 - 10)),
                    ModContent.GetInstance<TerraFactory>());
            }
        }
        // Adds walls behind the ice caves
        int endX = icedungeonX + 111, endY = icedungeonY + (layers + 1) * 35;
        for (int i = icedungeonX; i < endX; i++) for (int j = icedungeonY; j < endY; j++)
            {
                if (Main.tile[i, j].WallType == 0)
                    WorldGen.PlaceWall(i, j, WallID.IceUnsafe, mute: true);
            }

    }
}
