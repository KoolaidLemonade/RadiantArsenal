using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items.Darts
{
    public class SpiritPipe : RadianceItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to summon homing wisps");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 54;
            item.crit = 1;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2f;
            item.value = 130000;
            item.rare = ItemRarityID.Yellow;
            item.autoReuse = true;
            item.shoot = 10;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item17;

            radianceCost = 20;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 2 + Main.rand.Next(3); i++)
                {
                    Projectile.NewProjectile(new Vector2(Main.MouseWorld.X + Main.rand.Next(-50, 50), (player.Center.Y - Main.screenHeight / 2) - 30), new Vector2(0, 10), mod.ProjectileType("SpiritPipeProj"), damage, knockBack, player.whoAmI);
                }
                return false;
            }
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 40;
                item.useAnimation = 40;
            }
            else
            {
                item.useTime = 22;
                item.useAnimation = 22;
            }
            return base.CanUseItem(player);
        }
    }

    public class SpiritPipeGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Necromancer || npc.type == NPCID.RaggedCaster || npc.type == NPCID.DungeonSpirit && Main.rand.NextFloat() < 0.05)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SpiritPipe"));
            }
        }
    }
}