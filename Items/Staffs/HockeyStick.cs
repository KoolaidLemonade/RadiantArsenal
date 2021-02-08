using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Staffs
{
    public class HockeyStick : RadianceItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to counter strike");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 7;
            item.crit = 4;
            item.noUseGraphic = true;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.melee = true;
            item.width = 20;
            item.height = 30;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.channel = true;
            item.knockBack = 6f;
            item.value = 20000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("HockeyStickProj");

            radianceCost = 20;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("HockeyStickProj")] >= 1 
                || player.ownedProjectileCounts[mod.ProjectileType("HockeyStickProj2")] >= 1 
                || player.ownedProjectileCounts[mod.ProjectileType("HockeyStickProj3")] >= 1)
            {
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("HockeyStickProj2"), damage, knockBack, player.whoAmI);
                return false;
            }
            Projectile.NewProjectile(player.Center, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("HockeyStickProj3")] >= 1)
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

        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                player.immune = true;
                player.immuneTime = 30;
                player.velocity += player.DirectionTo(Main.MouseWorld) * 8;
            }

            base.HoldItem(player);
        }
    }

    public class HockeyStickGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<HockeyStick>());
                nextSlot++;
            }
        }
    }
}