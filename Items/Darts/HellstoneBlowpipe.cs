using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class HellstoneBlowpipe : RadianceItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to spray fire");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 26;
            item.crit = 1;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2f;
            item.value = 27000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.shoot = 10;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item17;

            radianceCost = 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                float numberProjectiles = 3 + Main.rand.Next(3);
                float rotation = MathHelper.ToRadians(15);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectile(player.Center, perturbedSpeed * 5, ProjectileID.MolotovFire, damage, knockBack, player.whoAmI);
                }
                return false;
            }
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 30;
                item.useAnimation = 30;
            }
            else
            {
                item.useTime = 22;
                item.useAnimation = 22;
            }
            return base.CanUseItem(player);
        }
    }
}