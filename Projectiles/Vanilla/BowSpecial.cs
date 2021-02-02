using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace RadiantArsenal.Projectiles.Vanilla
{
    public abstract class BowSpecial : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.light = 1f;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
            projectile.penetrate = 3;
            drawOriginOffsetY = -25;
        }
    }

    internal class BorealWoodBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.FrostArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boreal Wood Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 229, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Frostburn, 180, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 229, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }

    internal class PalmWoodBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.WoodenArrowFriendly;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palm Wood Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Wet, 1200, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }

    internal class RichMahoganyBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ChlorophyteArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rich Mahogany Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 256, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Venom, 180, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 256, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }

    internal class EbonwoodBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.CursedArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebonwood Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 74, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.CursedInferno, 180, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 74, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }

    internal class ShadewoodBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.IchorArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadewood Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 57, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Ichor, 180, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 57, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }
    internal class DemonBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.CursedArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 74, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.CursedInferno, 180, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 74, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }
    internal class TendonBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.IchorArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tendon Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 57, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Ichor, 180, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 57, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }
    internal class MarrowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.BoneArrowFromMerchant;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marrow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            projectile.width = 20;
            projectile.height = 20;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
        }
    }
    internal class IceBowSpecial : BowSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.FrostArrow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Bow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 229, 0f, 0f, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity *= 0;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 229, 0f, 0f, 0);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}

