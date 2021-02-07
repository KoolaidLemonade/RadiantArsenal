using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Projectiles.Darts
{
    public class LuminiteDartProj : ModProjectile
    {
        public override string Texture => "RadiantArsenal/Items/Darts/LuminiteDart";
        public override void SetDefaults()
        {
            projectile.height = 8;
            projectile.width = 8;
            projectile.ranged = true;
            projectile.light = 1f;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.aiStyle = 0;
            projectile.friendly = true;

            drawOffsetX = -4;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.ai[0]++;
            int dust = Dust.NewDust(projectile.position, 0, 0, 246);
            Main.dust[dust].velocity = Vector2.Zero;
            Main.dust[dust].noGravity = true;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            if (projectile.ai[0] >= 120)
            {
                projectile.tileCollide = false;
                projectile.extraUpdates = 100;
                projectile.velocity = -(projectile.Center - player.Center) / 20;
            }
            else
            {
                int dust2 = Dust.NewDust(projectile.position, 0, 0, 246);
                Main.dust[dust2].velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-160));
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(projectile.position, 0, 0, 246);
                Main.dust[dust3].velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(160));
                Main.dust[dust3].noGravity = true;

                projectile.velocity /= 1.025f;
            }
            if (projectile.ai[0] == 120)
            {
                projectile.damage *= 2;
                projectile.friendly = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            projectile.friendly = false;
        }
    }
}