using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace RadiantArsenal.Items.VanillaChanges.Bows
{
    public class PearlwoodBow : BowWeaponMasterClass
    {
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i <= 5; i++)
                {
                    Vector2 perturbedSpeed5 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(40));
                    Projectile.NewProjectile(player.Center, perturbedSpeed5, ProjectileID.HolyArrow, damage * 2, knockBack * 2, player.whoAmI);
                }
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}