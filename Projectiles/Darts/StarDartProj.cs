using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Projectiles.Darts
{
    public class StarDartProj : ModProjectile
    {
        public override string Texture => "RadiantArsenal/Items/Darts/StarDart";
        public override void SetDefaults()
        {
            projectile.height = 8;
            projectile.width = 8;
            projectile.ranged = true;
            projectile.light = 1f;
            projectile.timeLeft = 600;
            projectile.penetrate = 1;
            projectile.aiStyle = 1;
            projectile.friendly = true;

            drawOffsetX = -2;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, 0, 0, 246);
            Main.dust[dust].velocity = Vector2.Zero;
            Main.dust[dust].noGravity = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.NextFloat() < 0.4f)
            {
                Vector2 spawnPos = new Vector2(target.Center.X + Main.rand.NextFloat(-450, 450), player.Center.Y - Main.screenHeight / 2);
                Projectile.NewProjectile(spawnPos, (target.Center - spawnPos) / 40, mod.ProjectileType("StarseekerSpellbladeProj2"), projectile.damage, projectile.knockBack, player.whoAmI);
            }
        }
    }
}