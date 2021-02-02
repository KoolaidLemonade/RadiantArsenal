using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.VanillaChanges.Bows
{
    public class IceBow : VanillaItemChange
    {
        public override void SetDefaults(Item item)
        {
            item.GetGlobalItem<RadianceGlobalItem>().radianceCost = 10;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<RadiancePlayer>().radianceCurrent >= item.GetGlobalItem<RadianceGlobalItem>().radianceCost)
            {
                return true;
            }

            return false;
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            return true;
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY);

            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("IceBowSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to shoot an arrow that stuns enemies"));
        }
    }
}