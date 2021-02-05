using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Projectiles.Darts
{
    public class CrystaliceDartProj : ModProjectile
    {
        public override string Texture => "RadiantArsenal/Items/Darts/CrystaliceDart";
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
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 3);
        }
    }
}