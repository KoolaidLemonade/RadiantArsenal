using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.VanillaChanges.Accessories
{
    public class StarinaBottle : VanillaItemChange
    {
        public override void SetDefaults(Item item)
        {
           //item.accessory = true;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = RadiancePlayer.ModPlayer(player);
            modPlayer.radianceRegen += 100;
            //modPlayer.radianceCurrent = modPlayer.radianceMax;
        }
    }
}