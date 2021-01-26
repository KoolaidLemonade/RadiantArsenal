using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace RadiantArsenal.Projectiles.Spellblades
{
    public class EnchantedSpellbladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Spellblade");
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
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X, projectile.velocity.Y, 150, default, 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat() < 0.25f)
            {
                Player player = Main.player[projectile.owner];

                Vector2 randCircle = target.Center + Vector2.One.RotatedByRandom(Math.PI * 4) * 300;

                for (int i = 0; i < 20; i++)
                {
                    Vector2 randerCircle = randCircle + Vector2.One.RotatedByRandom(Math.PI * 4) * 3;
                    int dust = Dust.NewDust(randerCircle, 0, 0, 15);
                    Main.dust[dust].velocity = -(randerCircle - randCircle) / 3;
                }
                Projectile.NewProjectile(randCircle, -(randCircle - target.Center) / 20, mod.ProjectileType("EnchantedSpellbladeProj2"), damage, knockback, player.whoAmI);
            }
            target.immune[projectile.owner] = 6;
        }
    }

    public class EnchantedSpellbladeProj2 : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.EnchantedBeam;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EnchantedBeam);
            aiType = ProjectileID.EnchantedBeam;

            projectile.penetrate = 2;
            projectile.timeLeft = 180;
            projectile.alpha = 0;
            projectile.tileCollide = false;
        }
    }
}