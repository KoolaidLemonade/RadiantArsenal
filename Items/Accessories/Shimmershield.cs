using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace RadiantArsenal.Items.Accessories
{
	public class Shimmershield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases radiance regeneration by 2"
				+ "\nGrants immunity to knockback and fire blocks"
				+ "\nErupt into crystal shards when hit");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 300000;
			item.rare = ItemRarityID.LightPurple;
			item.accessory = true;
			item.defense = 8;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ShimmershieldModPlayer>().Shimmershield = true;
			player.GetModPlayer<RadiancePlayer>().radianceRegen += 2;
			player.noKnockback = true;
			player.fireWalk = true;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 6);
			recipe.AddIngredient(ItemID.ObsidianShield);
			recipe.AddIngredient(ModContent.ItemType<StarBand>());
			recipe.AddIngredient(ItemID.CrystalShard, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }

	public class ShimmershieldModPlayer : ModPlayer
	{
		public bool Shimmershield;
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (Shimmershield)
            {
                for (int i = 0; i < 12; i++)
                {
					Projectile.NewProjectile(player.Center, Vector2.One.RotatedByRandom(Math.PI * 4) * 5, ProjectileID.CrystalStorm, 18, 1f, player.whoAmI);
                }
            }
        }

        public override void ResetEffects()
		{
			Shimmershield = false;
		}
	}
}