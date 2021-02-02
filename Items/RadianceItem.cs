using Microsoft.Xna.Framework;
using RadiantArsenal.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items
{
    public abstract class RadianceItem : ModItem
    {
        public override bool CloneNewInstances => true;
        public int radianceCost = 0;
        public virtual void SafeSetDefaults()
        {
            
        }
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (radianceCost > 0)
            {
                tooltips.Add(new TooltipLine(mod, "Radiance Cost", $"Uses {radianceCost} Radiance"));
            }
        }

        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<RadiancePlayer>();

            if (player.altFunctionUse == 2)
            {
                if (modPlayer.radianceCurrent >= radianceCost)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return base.CanUseItem(player);
            }
        }
        public override void HoldItem(Player player)
        {
            var modPlayer = player.GetModPlayer<RadiancePlayer>();

            if (player.altFunctionUse == 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                modPlayer.radianceCurrent -= radianceCost;
            }
        }
    }
}
