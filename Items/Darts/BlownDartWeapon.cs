using Microsoft.Xna.Framework;

namespace RadiantArsenal.Items.Darts
{
    public abstract class BlownDartWeapon : RadianceItem
    {
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -6);
        }
    }
}