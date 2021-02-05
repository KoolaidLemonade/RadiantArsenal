using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class NaturesBlowpipe : BlownDartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature's Blowpipe");
            Tooltip.SetDefault("Right click to fire a spray of spore clouds");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 16;
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
            item.value = 12000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.shoot = 10;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item17;

            radianceCost = 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddIngredient(ItemID.Blowpipe);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
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
                for (int i = 0; i < 2 + Main.rand.Next(3); i++)
                {
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)), ProjectileID.SporeCloud, (int)(damage * 1.5f), 0, player.whoAmI);
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