using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class LuminiteDart : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Monkey business'");
        }
        public override void SetDefaults()
        {
            item.damage = 40;
            item.knockBack = 2f;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.value = 50;
            item.rare = ItemRarityID.Purple;
            item.shoot = mod.ProjectileType("LuminiteDartProj");
            item.ammo = AmmoID.Dart;
            item.consumable = true;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}