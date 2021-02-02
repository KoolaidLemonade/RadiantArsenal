using Microsoft.Xna.Framework;
using RadiantArsenal.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Reflection;

namespace RadiantArsenal.Items
{
    public class RadianceGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        public int radianceCost = 0;

        public override void SetDefaults(Item item)
        {
            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                vanillaItem.SetDefaults(item);
            }
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                return vanillaItem.AltFunctionUse(item, player);
            }

            return base.AltFunctionUse(item, player);
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                return vanillaItem.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                vanillaItem.ModifyTooltips(item, tooltips);
            }

            if (radianceCost > 0)
            {
                tooltips.Add(new TooltipLine(mod, "Radiance Cost", $"Uses {radianceCost} Radiance"));
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                return vanillaItem.CanUseItem(item, player);
            }

            return base.CanUseItem(item, player);
        }

        public override void HoldItem(Item item, Player player)
        {
            var modPlayer = player.GetModPlayer<RadiancePlayer>();

            if (player.altFunctionUse == 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                modPlayer.ConsumeRadiance(radianceCost);
            }

            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                vanillaItem.HoldItem(item, player);
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (VanillaItemChange.VanillaItemChanges.TryGetValue(item.type, out VanillaItemChange vanillaItem))
            {
                vanillaItem.UpdateAccessory(item, player, hideVisual);
            }
        }
    }
}