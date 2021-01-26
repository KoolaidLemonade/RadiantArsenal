using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace RadiantArsenal.Projectiles.Spellblades
{
    public class HallowedSpellbladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Spellblade");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.magic = true;
            projectile.light = 1f;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ignoreWater = true;

            drawHeldProjInFrontOfHeldItemAndArms = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            player.heldProj = projectile.whoAmI;

            projectile.alpha += 5;

            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.Kill();
                }
            }

            if (Main.rand.NextFloat() < 0.25)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, projectile.velocity.X, projectile.velocity.Y, 150, default, 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }
    }
    public class HallowedSpellbladeProj2 : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.SwordBeam;
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;

            projectile.penetrate = 2;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;

            drawOffsetX = -13;
        }

        public override void AI()
        {
            projectile.ai[0]++;

            projectile.timeLeft = 60;
            projectile.rotation = MathHelper.ToRadians(-45);

            Player player = Main.player[projectile.owner];

            projectile.Center = player.Center - new Vector2(0, 60);     
            
            if (projectile.ai[0] % 20 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC npc = Main.npc[i];

                    if (projectile.Distance(npc.Center) < 450 && npc.active && !npc.dontTakeDamage && !npc.immortal && !npc.friendly && npc.lifeMax > 5)
                    {
                        projectile.Kill();
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];

            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("HallowedSpellbladeProj3"), projectile.damage, projectile.knockBack, player.whoAmI);

            for (int i = 0; i < 120; i++)
            {
                Vector2 newCenter = player.Center - new Vector2(0, 60);
                Vector2 randCircle = newCenter + Vector2.One.RotatedByRandom(Math.PI * 4) * 12;
                int dust = Dust.NewDust(randCircle, 0, 0, 246);
                Main.dust[dust].velocity = Vector2.Zero + -((Main.dust[dust].position - newCenter) / 10);
                Main.dust[dust].noGravity = true;
            }
        }
    }

    public class HallowedSpellbladeProj3 : ModProjectile
    {
        public override string Texture => "Terraria/Item_0";
        public override void SetDefaults()
        {
            projectile.timeLeft = 3600;
            projectile.magic = true;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.penetrate = 5;
            projectile.friendly = true;
            projectile.hide = true;

            projectile.width = 10;
            projectile.height = 10;

            projectile.extraUpdates = 100;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (Main.rand.NextFloat() < 0.5)
            {
                int dust = Dust.NewDust(projectile.Center, 0, 0, 246);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity = Vector2.Zero;
            }

            if (projectile.timeLeft == 3600)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC npc = Main.npc[i];

                    if (projectile.Distance(npc.Center) < 450 && projectile.Distance(npc.Center) > 50 && npc.active && !npc.dontTakeDamage && !npc.immortal && !npc.friendly && npc.lifeMax > 5)
                    {
                        projectile.velocity = projectile.DirectionTo(npc.Center);
                    }
                }
            }

            if (projectile.Distance(player.Center) > 600)
            {
                projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.timeLeft += 1200;

            Player player = Main.player[projectile.owner];

            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];

                if (projectile.Distance(npc.Center) < 450 && projectile.Distance(npc.Center) > 50 && npc.active && !npc.dontTakeDamage && !npc.immortal && !npc.friendly && npc.lifeMax > 5)
                {
                    projectile.velocity = projectile.DirectionTo(npc.Center);
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("HallowedSpellbladeProj2"), projectile.damage, projectile.knockBack, player.whoAmI);

            for (int i = 0; i < 120; i++)
            {
                Vector2 newCenter = player.Center - new Vector2(8, 60);
                Vector2 randCircle = newCenter + Vector2.One.RotatedByRandom(Math.PI * 4) * 12;
                int dust = Dust.NewDust(randCircle, 0, 0, 246);
                Main.dust[dust].velocity = Vector2.Zero + ((Main.dust[dust].position - newCenter) / 10);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
