using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.VanillaChanges.Swords
{
    public class TerraBlade : VanillaItemChange
    {
        public override void SetDefaults(Item item)
        {
            item.GetGlobalItem<RadianceGlobalItem>().radianceCost = 100;
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            return true;
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to change the time from day to night or night to day"));
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.myPlayer == player.whoAmI && Main.mouseRightRelease && player.GetModPlayer<RadiancePlayer>().radianceCurrent >= item.GetGlobalItem<RadianceGlobalItem>().radianceCost)
                {
                    Main.dayTime = !Main.dayTime;
                    player.GetModPlayer<RadiancePlayer>().ConsumeRadiance(item.GetGlobalItem<RadianceGlobalItem>().radianceCost);
                    return true;
                }
                return false;
            }
            return base.CanUseItem(item, player);
        }
    }
}