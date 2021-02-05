using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class WayweaversDartRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayweaver's Dart Rifle");
            Tooltip.SetDefault("'Dominion over Galaxies'");
        }
        public override void SetDefaults()
        {
            item.damage = 32;
            item.crit = 1;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2f;
            item.value = 1000000;
            item.rare = ItemRarityID.Purple;
            item.autoReuse = true;
            item.shoot = 10;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item11;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 1 + Main.rand.Next(3); i++)
            {
                Vector2 randCircle = player.Center + Vector2.One.RotatedByRandom(Math.PI * 4) * 100;
                Projectile.NewProjectile(randCircle, -(randCircle - Main.MouseWorld) / 15, type, damage, knockBack, player.whoAmI);

                for (int j = 0; j < 60; j++)
                {
                    Vector2 randerCircle = randCircle + Vector2.One.RotatedByRandom(Math.PI * 4) * 5;
                    int dust = Dust.NewDust(randerCircle, 0, 0, 246);
                    Main.dust[dust].velocity = -(Main.dust[dust].position - randCircle) / 10;
                    Main.dust[dust].noGravity = true;
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.FragmentNebula, 4);
            recipe.AddIngredient(ItemID.FragmentSolar, 4);
            recipe.AddIngredient(ItemID.FragmentStardust, 4);
            recipe.AddIngredient(ItemID.FragmentVortex, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}