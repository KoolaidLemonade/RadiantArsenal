/*using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Staffs
{
    public class WoodenStaff : RadianceItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to counter strike");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 30;
            item.crit = 1;
            item.noUseGraphic = true;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.melee = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.channel = true;
            item.knockBack = 10f;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WoodenStaffProj");

            radianceCost = 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 12);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("WoodenStaffProj")] >= 1 
                || player.ownedProjectileCounts[mod.ProjectileType("WoodenStaffProj2")] >= 1 
                || player.ownedProjectileCounts[mod.ProjectileType("WoodenStaffProj3")] >= 1)
            {
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("WoodenStaffProj2"), damage, knockBack, player.whoAmI);
                return false;
            }
            Projectile.NewProjectile(player.Center, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("WoodenStaffProj3")] >= 1)
            {
                return false;
            }

            if (player.altFunctionUse == 2)
            {
                item.useTime = 20;
                item.useAnimation = 20;
            }
            else
            {
                item.useTime = 6;
                item.useAnimation = 6;
            }
            return base.CanUseItem(player);
        }
    }
}*/