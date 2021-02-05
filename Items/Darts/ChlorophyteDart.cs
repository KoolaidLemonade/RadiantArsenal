using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class ChlorophyteDart : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Oddly bouncy...'");
        }
        public override void SetDefaults()
        {
            item.damage = 26;
            item.knockBack = 2f;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.value = 10;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("ChlorophyteDartProj");
            item.ammo = AmmoID.Dart;
            item.consumable = true;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}