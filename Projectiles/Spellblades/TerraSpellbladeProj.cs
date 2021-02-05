using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace RadiantArsenal.Projectiles.Spellblades
{
    public class TerraSpellbladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Spellblade");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
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

            if (Main.rand.NextFloat() < 0.45)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, projectile.velocity.X * 3, projectile.velocity.Y * 3, 150, default, 0.75f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;

            Player player = Main.player[projectile.owner];
        }
    }

    public class TerraSpellbladeProj2 : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.TerraBeam;
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



            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (projectile.Distance(npc.Center) < 450 && npc.active && !npc.dontTakeDamage && !npc.immortal && !npc.friendly && npc.lifeMax > 5)
                {
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            for (int i = 0; i < 6; i++)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("TerraSpellbladeProj3"), projectile.damage, projectile.knockBack, player.whoAmI);
            }
        }
    }

    public class TerraSpellbladeProj3 : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.TerraBeam;
        public override void SetDefaults()
        {
            projectile.timeLeft = 600;
            projectile.magic = true;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.hide = true;

            projectile.width = 40;
            projectile.height = 40;

            projectile.extraUpdates = 300;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            int dust = Dust.NewDust(projectile.Center, 0, 0, 107);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Vector2.Zero;
            
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];

                if (projectile.timeLeft == 600)
                {
                    if (projectile.Distance(npc.Center) < 450 && npc.active && !npc.dontTakeDamage && !npc.immortal && !npc.friendly && npc.lifeMax > 5)
                    {
                        projectile.velocity = projectile.DirectionTo(npc.Center).RotatedByRandom(MathHelper.ToRadians(180));
                    }
                }

                if (projectile.timeLeft < 500)
                {
                    if (projectile.Distance(npc.Center) < 450 && npc.active && !npc.dontTakeDamage && !npc.immortal && !npc.friendly && npc.lifeMax > 5)
                    {
                        projectile.velocity += projectile.DirectionTo(npc.Center) / 10;
                    }
                }
            }

            if (projectile.Distance(player.Center) > 600)
            {
                projectile.Kill();
            }


        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            
            for (int i = 0; i < 60; i++)
            {
                Vector2 randCircle = projectile.Center + Vector2.One.RotatedByRandom(Math.PI * 4) * 5;
                int dust = Dust.NewDust(randCircle, 0, 0, 107);
                Main.dust[dust].velocity = (Main.dust[dust].position - projectile.Center) / 7;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}