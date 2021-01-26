using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RadiantArsenal.Projectiles.Vanilla
{
    public class LeafBlowerSpecial : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.WoodenArrowFriendly;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leaf Blower");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 50;
            projectile.height = 50;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.extraUpdates = 2;
            projectile.magic = true;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                Rectangle rectangle2 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                if (rectangle.Intersects(rectangle2) && !npc.boss && !npc.immortal && !npc.dontTakeDamage)
                {
                    npc.velocity = projectile.velocity;
                }
            }

            if (Main.rand.Next(8) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 16, projectile.velocity.X * 2, projectile.velocity.Y * 2, 30);
            }
        }
    }
}