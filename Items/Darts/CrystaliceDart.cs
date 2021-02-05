using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class CrystaliceDart : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Frostburn on hit");
        }
        public override void SetDefaults()
        {
            item.damage = 7;
            item.knockBack = 2f;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.value = 10;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("CrystaliceDartProj");
            item.ammo = AmmoID.Dart;
            item.consumable = true;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceTorch);
            recipe.AddIngredient(ItemID.SnowBlock);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}