using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerraFactory.Content.Global
{
    internal class BossHealthIncrease : GlobalNPC
    {
        public const int HEALTH_MULTI = 100;

        public override bool InstancePerEntity => true;

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            base.OnSpawn(npc, source);
            if (npc.boss) {
                Main.NewText("TerraFactory bosses are vulnerable to turrets, make sure to automate ammo production...");
                npc.lifeMax *= HEALTH_MULTI;
                npc.life = npc.lifeMax;
            }
        }

    }
}
