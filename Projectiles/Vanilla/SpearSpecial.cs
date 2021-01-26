using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RadiantArsenal.Projectiles.Vanilla
{
    public abstract class SpearSpecial : ModProjectile
    {
        public bool isStuck;

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.scale = 1.25f;
            projectile.penetrate = -1;
            projectile.melee = true;
        }
        public override void AI()
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            drawOriginOffsetX = -texture.Width / 2 + texture.Width / 2 * 0.09f;

            Player player = Main.player[projectile.owner];

            projectile.timeLeft = 2;
            projectile.ai[0]++;

            if (!isStuck)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135);
                projectile.velocity.Y += 0.1f;

                if (projectile.type == mod.ProjectileType("ChlorophytePartisanSpecial") && projectile.ai[0] == 10)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileID.SporeCloud, 30, 1f, player.whoAmI);
                    projectile.ai[0] = 0;
                }
                if (projectile.type == mod.ProjectileType("NorthPoleSpecial") && projectile.ai[0] == 3)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(160)), ProjectileID.NorthPoleSnowflake, 30, 1f, player.whoAmI);
                    Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(-160)), ProjectileID.NorthPoleSnowflake, 30, 1f, player.whoAmI);
                    projectile.ai[0] = 0;
                }
                if (projectile.type == mod.ProjectileType("MushroomSpearSpecial") && projectile.ai[0] == 8)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileID.Mushroom, 30, 1f, player.whoAmI);
                    projectile.ai[0] = 0;
                }
            }
            else
            {
                projectile.velocity *= 0;
                projectile.friendly = false;
                Rectangle rectangle = new Rectangle((int)projectile.position.X - projectile.width * 5 / 2, (int)projectile.position.Y - projectile.height * 5 / 2, projectile.width * 5, projectile.height * 5);
                if (player.Hitbox.Intersects(rectangle))
                {
                    projectile.Kill();
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            isStuck = true;
            return false;
        }
    }

    internal class AdamantiteGlaiveSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.AdamantiteGlaive;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Glaive");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.AdamantiteGlaive, 1, true, 0, true, false);
        }
    }

    internal class ChlorophytePartisanSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ChlorophytePartisan;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Partisan");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.ChlorophytePartisan, 1, true, 0, true, false);
        }
    }
    internal class CobaltNaginataSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.CobaltNaginata;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Naginata");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.CobaltNaginata, 1, true, 0, true, false);
        }
    }

    internal class DarkLanceSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.DarkLance;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Lance");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.DarkLance, 1, true, 0, true, false);
        }
    }
    internal class GungnirSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Gungnir;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gungnir");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.Gungnir, 1, true, 0, true, false);
        }
    }
    internal class MushroomSpearSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.MushroomSpear;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Spear");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.MushroomSpear, 1, true, 0, true, false);
        }
    }
    internal class MythrilHalberdSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.MythrilHalberd;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Halberd");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.MythrilHalberd, 1, true, 0, true, false);
        }
    }
    internal class NorthPoleSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.NorthPoleWeapon;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("North Pole");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.NorthPole, 1, true, 0, true, false);
        }
    }
    internal class ObsidianSwordfishSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ObsidianSwordfish;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Swordfish");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.ObsidianSwordfish, 1, true, 0, true, false);
        }
    }
    internal class OrichalcumHalberdSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.OrichalcumHalberd;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Halberd");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.OrichalcumHalberd, 1, true, 0, true, false);
        }
    }
    internal class PalladiumPikeSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.PalladiumPike;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Pike");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.PalladiumPike, 1, true, 0, true, false);
        }
    }
    //god fucking damnit "spear" bitchass who the fuck just names a weapon "spear" boring ass
    internal class SpearSpecialProjectile : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Spear;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spear");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.Spear, 1, true, 0, true, false);
        }
    }
    internal class SwordfishSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Swordfish;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Swordfish");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.Swordfish, 1, true, 0, true, false);
        }
    }
    internal class TheRottedForkSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.TheRottedFork;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Rotted Fork");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.TheRottedFork, 1, true, 0, true, false);
        }
    }
    internal class TitaniumTridentSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.TitaniumTrident;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Trident");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.TitaniumTrident, 1, true, 0, true, false);
        }
    }
    internal class TridentSpecial : SpearSpecial
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Trident;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trident");
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Item.NewItem(player.Center, ItemID.Trident, 1, true, 0, true, false);
        }
    }
}