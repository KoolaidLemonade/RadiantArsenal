using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class StarDart : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Chance to rain stars on hit");
        }
        public override void SetDefaults()
        {
            item.damage = 5;
            item.knockBack = 2f;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.value = 10;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("StarDartProj");
            item.ammo = AmmoID.Dart;
            item.consumable = true;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}