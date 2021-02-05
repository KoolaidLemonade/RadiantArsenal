using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class DartShotgun : RadianceItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to fire a crystal barrage");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 20;
            item.crit = 1;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.width = 35;
            item.height = 35;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2f;
            item.value = 400000;
            item.rare = ItemRarityID.LightPurple;
            item.autoReuse = true;
            item.shoot = 10;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item11;

            radianceCost = 4;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 3 + Main.rand.Next(3); i++)
                {
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8)), ProjectileID.CrystalStorm, damage / 2, knockBack, player.whoAmI);
                    return false;
                }
            }
            for (int i = 0; i < 3 + Main.rand.Next(3); i++)
            {
                Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)), type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 4;
                item.useAnimation = 4;
            }
            else
            {
                item.useTime = 35;
                item.useAnimation = 35;
            }
            return base.CanUseItem(player);
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            return true;
        }
    }

    public class DartShotgunGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.BigMimicHallow && Main.rand.NextFloat() < 0.25)
            {
                 Item.NewItem(npc.getRect(), mod.ItemType("DartShotgun"));
            }
        }
    }
}