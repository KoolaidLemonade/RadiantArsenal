using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Projectiles.Darts
{
    public class ChlorophyteDartProj : ModProjectile
    {
        public override string Texture => "RadiantArsenal/Items/Darts/ChlorophyteDart";
        public override void SetDefaults()
        {
            projectile.height = 8;
            projectile.width = 8;
            projectile.ranged = true;
            projectile.light = 1f;
            projectile.timeLeft = 2400;
            projectile.penetrate = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;

            projectile.extraUpdates = 5;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(projectile.Center, 0, 0, 107);
            Main.dust[dust].velocity = Vector2.Zero;
            Main.dust[dust].noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            projectile.friendly = false;
            projectile.velocity = -projectile.velocity;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.friendly = true;
            projectile.velocity = -projectile.velocity;
            projectile.penetrate--;
            return false;
        }
    }
}