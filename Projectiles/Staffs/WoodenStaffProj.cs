using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace RadiantArsenal.Projectiles.Staffs
{
    public class WoodenStaffProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden Staff");
        }

        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.scale = 1.5f;

            drawHeldProjInFrontOfHeldItemAndArms = true;
            drawOffsetX = 15;
            drawOriginOffsetY = 15;
        }

        public override void AI()
        {
            projectile.timeLeft = 2;
            Player player = Main.player[projectile.owner];
            player.heldProj = projectile.whoAmI;

            if (!player.channel)
            {
                projectile.Kill();
            }

            float xDiff = Main.MouseWorld.X - player.Center.X;
            float yDiff = Main.MouseWorld.Y - player.Center.Y;
            double angle = Math.Atan2(yDiff, xDiff) * 180 / Math.PI;
            float radius = 15;

            Vector2 newCenter = new Vector2((float)(player.Center.X + radius * Math.Cos(angle * Math.PI / 180)), 
                (float)(player.Center.Y + radius * Math.Sin(angle * Math.PI / 180)));

            projectile.Center = newCenter;
            projectile.rotation += player.direction == -1 ? -0.35f : 0.35f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (player.direction == -1)
            {
                hitDirection = -1;
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Projectile.NewProjectile(player.Center, player.DirectionTo(Main.MouseWorld) * 10, mod.ProjectileType("WoodenStaffProj3"), projectile.damage, projectile.knockBack, player.whoAmI);
            for (int i = 0; i < 40; i++)
            {
                Vector2 randCircle = projectile.Center - new Vector2(4, 4) + Vector2.One.RotatedByRandom(Math.PI * 4) * 5;
                int dust = Dust.NewDust(randCircle, 0, 0, 246);
                Main.dust[dust].velocity = (Main.dust[dust].position - projectile.Center) / 4;
                Main.dust[dust].noGravity = true;
            }
        }
    }

    public class WoodenStaffProj2 : ModProjectile
    {
        public override string Texture => "RadiantArsenal/Projectiles/Staffs/WoodenStaffProj";

        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.timeLeft = 15;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.scale = 1.5f;

            drawHeldProjInFrontOfHeldItemAndArms = true;
            drawOffsetX = 15;
            drawOriginOffsetY = 15;
        }

        public override void AI()
        {
            projectile.rotation += 0.75f;

            Player player = Main.player[projectile.owner];
            player.heldProj = projectile.whoAmI;

            float xDiff = Main.MouseWorld.X - player.Center.X;
            float yDiff = Main.MouseWorld.Y - player.Center.Y;
            double angle = Math.Atan2(yDiff, xDiff) * 180 / Math.PI;
            float radius = 20;

            Vector2 newCenter = new Vector2((float)(player.Center.X + radius * Math.Cos(angle * Math.PI / 180)),
                (float)(player.Center.Y + radius * Math.Sin(angle * Math.PI / 180)));

            projectile.Center = newCenter;

            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.getRect().Intersects(projectile.getRect()))
                {
                    player.immune = true;
                    player.immuneTime = 30;
                    player.velocity += player.DirectionTo(npc.Center);
                }
            }

            if (projectile.timeLeft >= 15)
            {
                for (int i = 0; i < 120; i++)
                {
                    Vector2 randCircle = projectile.Center - new Vector2(4, 4) + Vector2.One.RotatedByRandom(Math.PI * 4) * 5;
                    int dust = Dust.NewDust(randCircle, 0, 0, 246);
                    Main.dust[dust].velocity = Main.dust[dust].position - projectile.Center;
                    Main.dust[dust].noGravity = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (player.direction == -1)
            {
                hitDirection = -1;
            }
        }
    }
    
    public class WoodenStaffProj3 : ModProjectile
    {
        public override string Texture => "RadiantArsenal/Projectiles/Staffs/WoodenStaffProj";

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.melee = true;
            projectile.timeLeft = 600;
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.scale = 1.5f;
            projectile.penetrate = -1;

            drawOffsetX = -4;
            drawOriginOffsetY = -4;
        }
    }
}