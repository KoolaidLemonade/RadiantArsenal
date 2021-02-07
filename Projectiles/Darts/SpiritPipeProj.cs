using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace RadiantArsenal.Projectiles.Darts
{
    public class SpiritPipeProj : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LostSoulFriendly;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LostSoulFriendly);
            aiType = ProjectileID.LostSoulFriendly;

            projectile.penetrate = 1;
            projectile.tileCollide = false;
        }
    }
}