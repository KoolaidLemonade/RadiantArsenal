using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace RadiantArsenal.Projectiles.Spellblades
{
    public class AquasteelSpellbladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquasteel Spellblade");
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
            target.immune[projectile.owner] = 6;
        }
    }
}