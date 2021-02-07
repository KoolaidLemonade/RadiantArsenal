using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace RadiantArsenal.Items.Accessories
{
	public class SpiritLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases radiance regeneration by 1"
				+ "\nChance on hit to cause enemies to erupt into spirit energy");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 30000;
			item.rare = ItemRarityID.Green;
			item.accessory = true;
			item.defense = 5;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SpiritLanternModPlayer>().spiritLantern = true;
			player.GetModPlayer<RadiancePlayer>().radianceRegen += 1;
		}
	}

	public class SpiritLanternGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.CursedSkull || npc.type == NPCID.GiantCursedSkull && Main.rand.NextFloat() > 0.05)
            {
				Item.NewItem(npc.getRect(), mod.ItemType("SpiritLantern"));
            }
        }
    }

	public class SpiritLanternModPlayer : ModPlayer
	{
		public bool spiritLantern;
		public override void OnHitAnything(float x, float y, Entity victim)
		{
			if (spiritLantern && Main.rand.NextFloat() < 0.08)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector2 randCircle = victim.Center + new Vector2(0, 40).RotatedByRandom(Math.PI * 4);
					Projectile.NewProjectile(randCircle, (randCircle - victim.Center) / 3, mod.ProjectileType("SpiritPipeProj"), 10, 0, player.whoAmI);
				}
			}
		}

		public override void ResetEffects()
		{
			spiritLantern = false;
		}
	}
}