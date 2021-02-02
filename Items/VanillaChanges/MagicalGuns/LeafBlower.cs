using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.VanillaChanges.MagicalGuns
{
    public class LeafBlower : VanillaItemChange
    {
        public override void SetDefaults(Item item)
        {
            item.GetGlobalItem<RadianceGlobalItem>().radianceCost = 10;
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            return true;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 30;
                item.useAnimation = 30;

                if (player.GetModPlayer<RadiancePlayer>().radianceCurrent >= item.GetGlobalItem<RadianceGlobalItem>().radianceCost)
                {
                    return true;
                }

                return false;
            }

            item.useTime = 7;
            item.useAnimation = 7;

            return base.CanUseItem(item, player);
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i <= 5; i++)
                {
                    Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(player.Center, perturbedSpeed2 / 2, mod.ProjectileType("LeafBlowerSpecial"), damage / 5, knockBack, player.whoAmI);
                }
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to release a gust of wind that blows enemies away"));
        }
    }
}