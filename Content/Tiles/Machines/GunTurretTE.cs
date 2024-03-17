using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria.ID;
using TerraFactory.Content.Utils;

namespace TerraFactory.Content.Tiles.Machines
{
    internal class GunTurretTE : AbstractMachineTE
    {

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<GunTurret>();
        }

        /// <summary>
        /// Always normalized vector describing the direction of the last shot fired by this turret<br/>
        /// By default, is straight left
        /// </summary>
        public Vector2 direction = new Vector2(-1, 0);

        /// <returns>The display rotation for this gun turret, matching the lastTarget angle</returns>
        public float getRotation()
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        int counter = 0;
        public int ammo = 0, max_ammo = 20;

        public override void Update()
        {
            counter++;
            if (counter < 10)
                return;
            counter = 0;

            // Try to craft ammo with lead
            if (ammo <= 0)
                foreach (FastItemStack stk in content)
                    if (stk.itemID == ItemID.LeadBar && stk.quantity >= 1)
                    {
                        stk.quantity--;
                        ammo += max_ammo;
                    }
            content.RemoveAll(stk => stk.quantity <= 0);

            // If no ammo left, we can't shoot...
            if (ammo <= 0) return;

            bool fire = SearchForTargets(out NPC target);
            if (fire)
            {
                Vector2 turretCenter = getTurretFirePoint();
                direction = target.Center - turretCenter;
                direction.Normalize();

                ammo--;
                Projectile.NewProjectile(
                    spawnSource: null,
                    position: turretCenter,
                    velocity: direction * 16,
                    Type: ProjectileID.SwordBeam, Damage: 800, KnockBack: 5,
                    Owner: Main.myPlayer);
            }
        }

        /// <summary>
        /// Scans the NPCs around the turret, and  outs the closest valid target to the turret's estimated center.<br/>
        /// Verifies line of sight, and priorises bosses over normal enemies no matter the range.
        /// </summary>
        /// <returns>true if a target has been found.</returns>
        private bool SearchForTargets(out NPC target)
        {
            // Maximum distance to the target to be considered set here
            target = null;
            float distanceFromTarget = 70 * 16;
            Vector2 turretCenter = getTurretFirePoint();
            bool foundTarget = false;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, turretCenter);
                    bool inRange = between < distanceFromTarget;
                    bool lineOfSight = Collision.CanHitLine(turretCenter, 1, 1, npc.position, npc.width, npc.height);

                    if (lineOfSight && inRange)
                    {
                        distanceFromTarget = between;
                        target = npc;
                        foundTarget = true;
                    }
                }
            }
            return foundTarget;
        }

        private Vector2 getTurretFirePoint()
        {
            return new Vector2((Position.X + 2f) * 16, (Position.Y + 0.2f) * 16);
        }

        public override string getDisplayName()
        {
            return "Gun turret";
        }
    }
}
