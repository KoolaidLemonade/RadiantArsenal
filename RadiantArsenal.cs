using Microsoft.Xna.Framework;
using RadiantArsenal.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace RadiantArsenal
{
    public class RadiantArsenal : Mod
    {
        public RadiantArsenal() { }

        private UserInterface _radianceBarUserInterface;
        internal RadianceBar RadianceBar;
        public override void Load()
        {
            RadianceBar = new RadianceBar();
            _radianceBarUserInterface = new UserInterface();
            _radianceBarUserInterface.SetState(RadianceBar);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _radianceBarUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "RadiantArsenal: Radiance Bar",
                    delegate {
                        _radianceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
