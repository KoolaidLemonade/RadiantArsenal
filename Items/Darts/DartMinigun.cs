using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class DartMinigun : RadianceItem
    {
        bool altAttack;
        int attackCount;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to charge and then hold left click to unleash a deadly spray of darts");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 8;
            item.crit = 1;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2f;
            item.value = 54000;
            item.rare = ItemRarityID.Orange;
            item.autoReuse = true;
            item.shoot = ProjectileID.Seed;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item11;

            radianceCost = 80;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (attackCount > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)), type, damage, knockBack, player.whoAmI);
                }
                attackCount--;
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)), type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 180;
                item.useAnimation = 180;
            }
            else if (attackCount == 0)
            {
                item.useTime = 7;
                item.useAnimation = 7;
            }
            else if (attackCount > 0)
            {
                item.useTime = 4;
                item.useAnimation = 4;
            }
            return base.CanUseItem(player);
        }

        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.itemAnimation > 0)
            {
                altAttack = true;
            }
            else
            {
                altAttack = false;
            }

            if (altAttack)
            {
                attackCount = 50;

                int dust = Dust.NewDust(player.Center + new Vector2(60, 0).RotatedByRandom(Math.PI * 4), 0, 0, 246);
                Main.dust[dust].velocity = -(Main.dust[dust].position - player.Center) / 5;
                Main.dust[dust].noGravity = true;

                player.velocity.X /= 1.06f;
            }

            base.HoldItem(player);
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (attackCount > 0)
            {
                return false;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }

    public class DartMinigunGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.rand.NextFloat() < 0.01 && NPCID.Search.TryGetName(npc.type, out string Name) && (Name.Contains("AngryBones") || Name.Contains("BlueArmoredBones")))
            {
                Item.NewItem(npc.getRect(), mod.ItemType("DartMinigun"));
            }
        }
    }
}