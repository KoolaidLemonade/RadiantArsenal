using Microsoft.Xna.Framework;
using Terraria;

namespace RadiantArsenal.Items.VanillaChanges.Spears
{
    public class Spear : SpearWeaponMasterClass
    {
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY);

            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(player.Center, speed * 3, mod.ProjectileType(GetType().Name + "SpecialProjectile"), damage * 4, knockBack * 2, player.whoAmI);
                player.HeldItem.TurnToAir();
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}