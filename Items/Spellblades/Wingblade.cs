﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Spellblades
{
    public class Wingblade : RadianceItem
    {
        bool isActive;
        float activeTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wingblade");
            Tooltip.SetDefault("Right click to summon spellblade" + "\nCauses fire to rain from the sky" + "\nGrants infinite wingtime while active" + "\nLasts 12 seconds");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 80;
            item.crit = 1;
            item.noUseGraphic = true;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.magic = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2f;
            item.value = 200000;
            item.rare = ItemRarityID.Cyan;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WanderReaverProj");
            item.UseSound = SoundID.Item1;

            radianceCost = 100;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BetsyWings);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BetsyWings);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            if (isActive)
            {
                return false;
            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            else
            {
                Vector2 perturbedSpeed = player.DirectionTo(Main.MouseWorld).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(player.Center, perturbedSpeed * 3, mod.ProjectileType("WingbladeProj"), damage, knockBack, player.whoAmI);

                return false;
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 20;
                item.useAnimation = 20;
                item.noUseGraphic = false;
                item.useStyle = ItemUseStyleID.HoldingUp;
            }
            else
            {
                item.noUseGraphic = true;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.useTime = 12;
                item.useAnimation = 12;

                if (!isActive)
                {
                    return false;
                }
            }
            return base.CanUseItem(player);
        }
        public override void UpdateInventory(Player player)
        {
            if (player.altFunctionUse == 2 && player.itemAnimation > 0 && player.HeldItem.type == mod.ItemType("Wingblade"))
            {
                isActive = true;
            }

            if (isActive)
            {
                activeTimer++;

                //vanilla code kekw
                int num = 0;
                num += player.bodyFrame.Y / 56;
                if (num >= Main.OffsetsPlayerHeadgear.Length)
                {
                    num = 0;
                }
                Vector2 value = new Vector2(player.width / 2, player.height / 2) + Main.OffsetsPlayerHeadgear[num] + (player.MountedCenter - player.Center);
                float num2 = -11.5f * player.gravDir;
                if (player.gravDir == -1f)
                {
                    num2 -= 3f;
                }
                Vector2 vector = new Vector2(3 * player.direction - ((player.direction == 1) ? 1 : 0), num2) + Vector2.UnitY * player.gfxOffY + value;
                Vector2 vector2 = new Vector2(3 * player.shadowDirection[1] - ((player.direction == 1) ? 1 : 0), num2) + value;
                Vector2 value2 = Vector2.Zero;

                if (player.fullRotation != 0f)
                {
                    vector = vector.RotatedBy(player.fullRotation, player.fullRotationOrigin);
                    vector2 = vector2.RotatedBy(player.fullRotation, player.fullRotationOrigin);
                }
                float num4 = 0f;
                Vector2 vector3 = player.position + vector + value2;
                Vector2 vector4 = player.oldPosition + vector2 + value2;
                vector4.Y -= num4 / 2f;
                vector3.Y -= num4 / 2f;
                float num5 = 1f;

                DelegateMethods.v3_1 = Main.hslToRgb(Main.rgbToHsl(player.eyeColor).X, 1f, 0.5f).ToVector3() * 0.5f * num5;
                if (player.velocity != Vector2.Zero)
                {
                    Utils.PlotTileLine(player.Center, player.Center + player.velocity * 2f, 4f, DelegateMethods.CastLightOpen);
                }
                else
                {
                    Utils.PlotTileLine(player.Left, player.Right, 4f, DelegateMethods.CastLightOpen);
                }

                int num6 = (int)Vector2.Distance(vector3, vector4) / 3 + 1;
                if (Vector2.Distance(vector3, vector4) % 3f != 0f)
                {
                    num6++;
                }

                for (float num7 = 1f; num7 <= (float)num6; num7 += 1f)
                {
                    if (player.mount.Active)
                    {
                        return;
                    }

                    Dust obj = Main.dust[Dust.NewDust(player.Center, 0, 0, 6)];
                    obj.position = Vector2.Lerp(vector4, vector3, num7 / (float)num6);
                    obj.noGravity = true;
                    obj.velocity = player.velocity;
                    obj.customData = this;
                    obj.scale = num5;
                }

                if (Main.rand.NextFloat() < 0.2f)
                {
                    Vector2 spawnPos = new Vector2(player.Center.X + Main.rand.NextFloat(-450, 450), player.Center.Y - Main.screenHeight / 2);
                    Projectile.NewProjectile(spawnPos, new Vector2(0, 10).RotatedByRandom(MathHelper.ToRadians(25)), ProjectileID.MolotovFire3, (int)(item.damage / 1.5f), item.knockBack, player.whoAmI);
                }
                player.wingTimeMax = 999999;
                player.wingTime = player.wingTimeMax;
            }
            if (activeTimer >= 720)
            {
                for (int i = 0; i < 720; i++)
                {
                    Vector2 randCircle = player.Center + Vector2.One.RotatedByRandom(Math.PI * 4) * 100;
                    int dust = Dust.NewDust(randCircle, 0, 0, 6);
                    Main.dust[dust].velocity = ((Main.dust[dust].position - player.Center) / 10) + player.velocity;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1.4f;
                }
                Main.PlaySound(SoundID.MaxMana, player.Center);
                activeTimer = 0;    
                isActive = false;
            }

            if (activeTimer == 1)
            {
                for (int i = 0; i < 120; i++)
                {
                    Vector2 randCircle = player.Center + Vector2.One.RotatedByRandom(Math.PI * 4) * 100;
                    int dust = Dust.NewDust(randCircle, 0, 0, 6);
                    Main.dust[dust].velocity = (-(Main.dust[dust].position - player.Center) / 10) + player.velocity;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                }
                Main.PlaySound(SoundID.MaxMana, player.Center);
            }
        }
    }
}