using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantArsenal.Items
{
    public class RadiancePlayer : ModPlayer
    {
        public static RadiancePlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<RadiancePlayer>();
        }

        public const int DefaultRadianceMax = 100;
        public int radianceCurrent;
        public int radianceMax;
        public int radianceMax2;
        public int radianceRegen;
        public bool radianceCapped;

        int regenTimer;
        int radianceCurrent2;
        public override void PostUpdateMiscEffects()
        {
            UpdateRadiance();
        }

        public override void Initialize()
        {
            radianceMax = DefaultRadianceMax;
        }

        private void ResetVariables()
        {
            radianceMax2 = radianceMax;
            radianceRegen = 0;
        }
        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (!radianceCapped && target.type != NPCID.TargetDummy)
            {
                radianceCurrent += 1;
                radianceCurrent2 += 1;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (!radianceCapped && target.type != NPCID.TargetDummy)
            {
                radianceCurrent += 1;
                radianceCurrent2 += 1;
            }
        }

        private void UpdateRadiance()
        {
            regenTimer++;
            if (regenTimer % 60 == 0)
            {
                radianceCurrent += radianceRegen;
                radianceCurrent2 += radianceRegen;
            }

            if (radianceCurrent != radianceCurrent2)
            {
                Color color = new Color(255, 20, 20);

                string radianceDifference = $"{radianceCurrent - radianceCurrent2} Radiance!";
                CombatText.NewText(player.getRect(), color, radianceDifference, true, true);
                radianceCurrent2 = radianceCurrent;
            }

            if (radianceCurrent >= radianceMax2)
            {
                radianceCapped = true;
            }
            else
            {
                radianceCapped = false;
            }

            radianceCurrent = Utils.Clamp(radianceCurrent, 0, radianceMax2);
        }

        public void ConsumeRadiance(int ConsumptionAmount)
        {
            if (radianceCurrent - ConsumptionAmount >= 0)
            {
                radianceCurrent -= ConsumptionAmount;
            }
        }
    }
}
