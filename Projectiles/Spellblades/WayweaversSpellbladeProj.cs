using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace RadiantArsenal.Projectiles.Spellblades
{
    public class WayweaversSpellbladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayweaver's Spellblade");
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

            if (Main.rand.NextFloat() < 0.5)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 204, projectile.velocity.X * 3, projectile.velocity.Y * 3, 0, default);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }
    }

    public class WayweaversSpellbladeProj2 : ModProjectile
    {
        public override string Texture => "Terraria/Item_0";
        public override void SetDefaults()
        {
            projectile.timeLeft = 3600;
            projectile.magic = true;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.hide = true;

            projectile.width = 10;
            projectile.height = 10;

            projectile.extraUpdates = 100;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            int dust = Dust.NewDust(projectile.Center, 0, 0, 204);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Vector2.Zero;

            if (projectile.Distance(player.Center) > 800)
            {
                projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            projectile.tileCollide = true;
            
            for (int i = 0; i < 120; i++)
            {
                int dust = Dust.NewDust(projectile.Center, 0, 0, 204);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity = Vector2.Zero;
            }
        }
    }
}