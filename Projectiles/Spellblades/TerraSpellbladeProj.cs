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

            float num132 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
                float num133 = projectile.localAI[0];
                
                if (num133 == 0f)
                {
                    projectile.localAI[0] = num132;
                    num133 = num132;
                }

                float num134 = projectile.position.X;
                float num135 = projectile.position.Y;
                float num136 = 300f;
                bool flag3 = false;
                int num137 = 0;
                
                if (projectile.ai[1] == 0f)
                {
                    for (int num138 = 0; num138 < 200; num138++)
                    {
                        if (Main.npc[num138].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num138 + 1)))
                        {
                            float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
                            float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
                            float num141 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num139) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num140);
                            
                            if (num141 < num136 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
                            {
                                num136 = num141;
                                num134 = num139;
                                num135 = num140;
                                flag3 = true;
                                num137 = num138;
                            }
                        }
                    }

                    if (flag3)
                    {
                        projectile.ai[1] = (float)(num137 + 1);
                    }
                    flag3 = false;
                }

                if (projectile.ai[1] > 0f)
                {
                    int num142 = (int)(projectile.ai[1] - 1f);

                    if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                    {
                        float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                        float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                        if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num143) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num144) < 1000f)
                        {
                            flag3 = true;
                            num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                            num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                        }
                    }

                    else
                    {
                        projectile.ai[1] = 0f;
                    }
                }

                if (!projectile.friendly)
                {
                    flag3 = false;
                }

                if (flag3)
                {
                    float num145 = num133;
                    Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num146 = num134 - vector10.X;
                    float num147 = num135 - vector10.Y;
                    float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                    num148 = num145 / num148;
                    num146 *= num148;
                    num147 *= num148;
                    int num149 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                    projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
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