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

public class RadianceGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool CloneNewInstances => true;

    public int radianceCost = 0;
    public override void SetDefaults(Item item)
    {
        switch (item.type)
        {
            case ItemID.Revolver:
            case ItemID.TheUndertaker:
                radianceCost = 5;
                break;
            case ItemID.Spear:
            case ItemID.Trident:
            case ItemID.TheRottedFork:
            case ItemID.Swordfish:
            case ItemID.DarkLance:
            case ItemID.ObsidianSwordfish:
            case ItemID.TitaniumTrident:
            case ItemID.PalladiumPike:
            case ItemID.OrichalcumHalberd:
            case ItemID.MythrilHalberd:
            case ItemID.MushroomSpear:
            case ItemID.ChlorophytePartisan:
            case ItemID.NorthPole:
            case ItemID.AdamantiteGlaive:
            case ItemID.CobaltNaginata:
            case ItemID.Gungnir:

            case ItemID.BorealWoodBow:
            case ItemID.ShadewoodBow:
            case ItemID.EbonwoodBow:
            case ItemID.RichMahoganyBow:
            case ItemID.PalmWoodBow:
            case ItemID.PearlwoodBow:
            case ItemID.DemonBow:
            case ItemID.TendonBow:
            case ItemID.IceBow:

            case ItemID.LeafBlower:
                radianceCost = 10;
                break;
            case ItemID.Marrow:
            case ItemID.PulseBow:
                radianceCost = 20;
                break;

            case ItemID.NightsEdge:
            case ItemID.TrueNightsEdge:
            case ItemID.Excalibur:
            case ItemID.TrueExcalibur:
            case ItemID.TerraBlade:
                radianceCost = 100;
                break;


            //case ItemID.StarinaBottle:
            //    item.accessory = true;
            //    break;
        }
    }

    public override bool AltFunctionUse(Item item, Player player)
    {
        switch (item.type)
        {
            case ItemID.Spear:
            case ItemID.Trident:
            case ItemID.TheRottedFork:
            case ItemID.Swordfish:
            case ItemID.DarkLance:
            case ItemID.ObsidianSwordfish:
            case ItemID.TitaniumTrident:
            case ItemID.PalladiumPike:
            case ItemID.OrichalcumHalberd:
            case ItemID.MythrilHalberd:
            case ItemID.MushroomSpear:
            case ItemID.ChlorophytePartisan:
            case ItemID.NorthPole:
            case ItemID.AdamantiteGlaive:
            case ItemID.CobaltNaginata:
            case ItemID.Gungnir:
            case ItemID.LeafBlower:

            case ItemID.BorealWoodBow:
            case ItemID.ShadewoodBow:
            case ItemID.EbonwoodBow:
            case ItemID.RichMahoganyBow:
            case ItemID.PalmWoodBow:
            case ItemID.PearlwoodBow:
            case ItemID.DemonBow:
            case ItemID.TendonBow:
            case ItemID.Marrow:
            case ItemID.IceBow:
            case ItemID.PulseBow:

            case ItemID.Revolver:
            case ItemID.TheUndertaker:

            case ItemID.NightsEdge:
            case ItemID.TrueNightsEdge:
            case ItemID.Excalibur:
            case ItemID.TrueExcalibur:
            case ItemID.TerraBlade:
                return true;
            default:
                return base.AltFunctionUse(item, player);
        }
    }

    public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
        Vector2 speed = new Vector2(speedX, speedY);

        if (player.altFunctionUse == 2)
        {
            switch (item.type)
            {
                case ItemID.NightsEdge:
                case ItemID.TrueNightsEdge:
                case ItemID.Excalibur:
                case ItemID.TrueExcalibur:
                case ItemID.TerraBlade:
                    return false;
                case ItemID.Spear:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("SpearSpecialProjectile"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.Trident:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("TridentSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.TheRottedFork:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("TheRottedForkSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.Swordfish:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("SwordfishSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.DarkLance:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("DarkLanceSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.ObsidianSwordfish:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("ObsidianSwordfishSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.TitaniumTrident:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("TitaniumTridentSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.PalladiumPike:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("PalladiumPikeSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.OrichalcumHalberd:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("OrichalcumHalberdSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.MythrilHalberd:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("MythrilHalberdSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.MushroomSpear:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("MushroomSpearSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.ChlorophytePartisan:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("ChlorophytePartisanSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.NorthPole:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("NorthPoleSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.AdamantiteGlaive:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("AdamantiteGlaiveSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.CobaltNaginata:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("CobaltNaginataSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;
                case ItemID.Gungnir:
                    Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType("GungnirSpecial"), damage * 4, knockBack * 2, player.whoAmI);
                    player.HeldItem.TurnToAir();
                    return false;

                case ItemID.BorealWoodBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("BorealWoodSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.ShadewoodBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("ShadewoodSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.EbonwoodBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("EbonwoodSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.RichMahoganyBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("RichMahoganySpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.PalmWoodBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("PalmWoodSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.PearlwoodBow:
                    for (int i = 0; i <= 5; i++)
                    {
                        Vector2 perturbedSpeed5 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(40));
                        Projectile.NewProjectile(player.Center, perturbedSpeed5, ProjectileID.HolyArrow, damage * 2, knockBack * 2, player.whoAmI);
                    }
                    return false;
                case ItemID.DemonBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("DemonBowSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.TendonBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("TendonBowSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.Marrow:
                    for (int i = 0; i <= Main.rand.Next(6, 10); i++)
                    {
                        Vector2 perturbedSpeed4 = new Vector2(0, -15).RotatedByRandom(MathHelper.ToRadians(8));
                        Projectile.NewProjectile(Main.MouseWorld.X + Main.rand.NextFloat(-50, 50), player.Center.Y + Main.screenHeight/2, perturbedSpeed4.X, perturbedSpeed4.Y, mod.ProjectileType("MarrowSpecial"), (int)(damage * 1.5f), knockBack * 2, player.whoAmI);
                    }
                    return false;
                case ItemID.IceBow:
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("IceBowSpecial"), damage * 2, knockBack * 2, player.whoAmI);
                    return false;
                case ItemID.PulseBow:
                    for (int i = 0; i <= Main.rand.Next(2, 4); i++)
                    {
                        Vector2 perturbedSpeed3 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                        Projectile.NewProjectile(player.Center, perturbedSpeed3, ProjectileID.PulseBolt, (int)(damage * 1.5f), knockBack * 2, player.whoAmI);
                    }
                    return false;

                case ItemID.LeafBlower:
                    for (int i = 0; i <= 5; i++)
                    {
                        Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                        Projectile.NewProjectile(player.Center, perturbedSpeed2 / 2, mod.ProjectileType("LeafBlowerSpecial"), damage / 5, knockBack, player.whoAmI);
                    }   
                    return false;
                case ItemID.Revolver:
                case ItemID.TheUndertaker:
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BulletHighVelocity, damage, knockBack, player.whoAmI);
                    return false;
                default:
                    return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }
        }
        else
        {
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        switch (item.type)
        {
            case ItemID.Spear:
            case ItemID.Trident:
            case ItemID.TheRottedFork:
            case ItemID.Swordfish:
            case ItemID.DarkLance:
            case ItemID.ObsidianSwordfish:
            case ItemID.TitaniumTrident:
            case ItemID.PalladiumPike:
            case ItemID.OrichalcumHalberd:
            case ItemID.MythrilHalberd:
            case ItemID.MushroomSpear:
            case ItemID.ChlorophytePartisan:
            case ItemID.NorthPole:
            case ItemID.AdamantiteGlaive:
            case ItemID.CobaltNaginata:
            case ItemID.Gungnir:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to throw"));
                break;
            case ItemID.BorealWoodBow:
            case ItemID.ShadewoodBow:
            case ItemID.EbonwoodBow:
            case ItemID.RichMahoganyBow:
            case ItemID.PalmWoodBow:
            case ItemID.PearlwoodBow:
            case ItemID.DemonBow:
            case ItemID.TendonBow:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to shoot a powerful special arrow"));
                break;
            case ItemID.Marrow:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to cause arrows to rise from the ground"));
                break;
            case ItemID.IceBow:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to shoot an arrow that stuns enemies"));
                break;
            case ItemID.PulseBow:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to fire a barrage of charged arrows"));
                break;
            case ItemID.NightsEdge:
            case ItemID.TrueNightsEdge:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to change the time to night"));
                break;
            case ItemID.Excalibur:
            case ItemID.TrueExcalibur:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to change the time to day"));
                break;
            case ItemID.TerraBlade:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to change the time from day to night or night to day"));
                break;
            case ItemID.LeafBlower:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to release a gust of wind that blows enemies away"));
                break;
            case ItemID.Revolver:
            case ItemID.TheUndertaker:
                tooltips.Add(new TooltipLine(mod, "Radiance Description", "Right Click to fan the hammer"));
                break;
        }

        if (radianceCost > 0)
        {
            tooltips.Add(new TooltipLine(mod, "Radiance Cost", $"Uses {radianceCost} Radiance"));
        }
    }
    public override bool CanUseItem(Item item, Player player)
    {
        var modPlayer = player.GetModPlayer<RadiancePlayer>();

        if (player.altFunctionUse == 2)
        {
            switch (item.type)
            {
                case ItemID.NightsEdge:
                case ItemID.TrueNightsEdge:
                    if (Main.dayTime)
                    {
                        Main.dayTime = false;
                    }
                    else
                    {
                        return false;
                    }
                    item.useStyle = ItemUseStyleID.HoldingUp;
                    break;
                case ItemID.Excalibur:
                case ItemID.TrueExcalibur:
                    if (!Main.dayTime)
                    {
                        Main.dayTime = true;
                    }
                    else
                    {
                        return false;
                    }
                    item.useStyle = ItemUseStyleID.HoldingUp;
                    break;
                case ItemID.TerraBlade:
                    if (Main.myPlayer == player.whoAmI && Main.mouseRightRelease)
                    {
                        if (!Main.dayTime)
                        {
                            Main.dayTime = true;
                        }
                        else
                        {
                            Main.dayTime = false;
                        }
                    }
                    item.useStyle = ItemUseStyleID.HoldingUp;
                    break;
                case ItemID.LeafBlower:
                    item.useTime = 30;
                    item.useAnimation = 30;
                    break;
                case ItemID.Revolver:
                case ItemID.TheUndertaker:
                    item.useTime = 3;
                    item.useAnimation = 3;
                    break;
            }

            if (modPlayer.radianceCurrent >= radianceCost)
            {
                return true;
            }
            return false;            
        }
        else
        {
            switch (item.type)
            {
                case ItemID.NightsEdge:
                case ItemID.TrueNightsEdge:
                case ItemID.Excalibur:
                case ItemID.TrueExcalibur:
                case ItemID.TerraBlade:
                    item.useStyle = ItemUseStyleID.SwingThrow;
                    break;
                case ItemID.LeafBlower:
                    item.useTime = 7;
                    item.useAnimation = 7;
                    break;
                case ItemID.Revolver:
                    item.useTime = 22;
                    item.useAnimation = 22;
                    break;
                case ItemID.TheUndertaker:
                    item.useTime = 23;
                    item.useAnimation = 23;
                    break;
            }

            return base.CanUseItem(item, player);
        }
    }

    public override void HoldItem(Item item, Player player)
    {
        var modPlayer = player.GetModPlayer<RadiancePlayer>();

        if (player.altFunctionUse == 2 && player.itemAnimation == player.itemAnimationMax - 1)
        {
            modPlayer.radianceCurrent -= radianceCost;
        }
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        switch (item.type)
        {
            case ItemID.StarinaBottle:
                var modPlayer = RadiancePlayer.ModPlayer(player);
                modPlayer.radianceRegen = 100;
                break;
        }
    }
}
