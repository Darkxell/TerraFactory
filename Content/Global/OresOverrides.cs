using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraFactory.Content.Global
{
    internal class OresOverrides : GlobalTile
    {


        public static int[] ORES_IDS = new int[] {
            // ---- VANILLA ORE IDS ----
            6, 7, 8, 9, /* iron, copper, gold, silver */
            166, 167, 168, 169, /* tin, lead, tungsten, platinum */
            221, 222, 223, /* palladium, orichalcum, titanium */
            107, 108, 111, /* cobalt, mythril, adamantite */
            37, 58, 22, 204, /* meteorite, hellstone, demonite, crimtane */
            211, 408 /* chlorophyte, luminite */
            // ---- TERRAFACTORY ORE IDS ----

        };

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (ORES_IDS.Contains(type))
            {
                bool mustfail = true;
                //base.KillTile(i, j, type, ref mustfail, ref effectOnly, ref noItem);
            }
            else
            {
                //base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
            }
        }

    }
}
