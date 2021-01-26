using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace RadiantArsenal.Projectiles.Spellblades
{
    public class VineborneSpellbladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vineborne Spellblade");
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
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, projectile.velocity.X, projectile.velocity.Y, 150, default, 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];

            float numberProjectiles = 6 + Main.rand.Next(3);

            if (projectile.damage - target.defense >= target.life)
            {

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp((float)(-Math.PI * 4f), (float)(Math.PI * 4f), i / (numberProjectiles - 1))) * 3;

                    Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("VineborneSpellbladeProj2"), damage, 0, player.whoAmI);
                }
            }

            target.immune[projectile.owner] = 6;
        }
    }

    public class VineborneSpellbladeProj2 : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.SporeCloud;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SporeCloud);
            aiType = ProjectileID.SporeCloud;

            projectile.timeLeft = 240;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];

            int numberProjectiles = 8 + Main.rand.Next(3);

            if (projectile.damage - target.defense >= target.life)
            {

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp((float)(-Math.PI * 4f), (float)(Math.PI * 4f), i / (numberProjectiles - 1))) * 4;

                    Projectile.NewProjectile(target.Center, perturbedSpeed.RotatedByRandom(Math.PI * 4), ProjectileID.SporeCloud, damage, 0, player.whoAmI);
                }
            }
        }
    }
}