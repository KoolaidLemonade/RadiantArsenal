using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.VanillaChanges.Guns
{
    public class TheUndertaker : VanillaItemChange
    {
        public override void SetDefaults(Item item)
        {
            item.GetGlobalItem<RadianceGlobalItem>().radianceCost = 5;
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            return true;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 3;
                item.useAnimation = 3;

                if (player.GetModPlayer<RadiancePlayer>().radianceCurrent >= item.GetGlobalItem<RadianceGlobalItem>().radianceCost)
                {
                    return true;
                }

                return false;
            }

            item.useTime = 23;
            item.useAnimation = 23;

            return base.CanUseItem(item, player);
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BulletHighVelocity, damage, knockBack, player.whoAmI);
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to fan the hammer"));
        }
    }
}