using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Terraria.DataStructures;

namespace RadiantArsenal.Items
{
	public class RadianceDrop : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiance");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
		}

        public override bool OnPickup(Player player)
        {
            player.GetModPlayer<RadiancePlayer>().AddRadiance(10);

            Color color = new Color(255, 246, 141);
            CombatText.NewText(player.getRect(), color, 10);
            return false;
        }
        public override bool ItemSpace(Player player)
        {
            return true;
        }
    }

	public class RadianceDropGlobalNPC : GlobalNPC
	{
        public override void NPCLoot(NPC npc)
        {
            Player player = Main.LocalPlayer;
            if (Main.rand.NextFloat() < 0.0833 && player.GetModPlayer<RadiancePlayer>().radianceCurrent < player.GetModPlayer<RadiancePlayer>().radianceMax2)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("RadianceDrop"));
            }
        }
    }
}