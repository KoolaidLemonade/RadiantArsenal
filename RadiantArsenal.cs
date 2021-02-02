using Microsoft.Xna.Framework;
using RadiantArsenal.Items;
using RadiantArsenal.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace RadiantArsenal
{
    public class RadiantArsenal : Mod
    {
        public RadiantArsenal() { }

        private UserInterface _radianceBarUserInterface;
        internal RadianceBar RadianceBar;

        public void AutoloadVanillaItemChange()
        {
            if (Code == null)
            {
                return;
            }

            foreach (Type item in Code.GetTypes())
            {
                if (!item.IsAbstract && item.IsSubclassOf(typeof(VanillaItemChange)) && !(item.GetConstructor(new Type[0]) == null))
                {
                    VanillaItemChange itemChange = (VanillaItemChange)Activator.CreateInstance(item);

                    if (itemChange.Type == 0 && ItemID.Search.ContainsName(item.Name))
                    {
                        itemChange.Type = ItemID.Search.GetId(item.Name);
                    }

                    if (itemChange.Type <= 0 || VanillaItemChange.VanillaItemChanges.Keys.Contains(itemChange.Type))
                    {
                        throw new Exception("Invalid Type");
                    }

                    VanillaItemChange.VanillaItemChanges.Add(itemChange.Type, itemChange);
                }
            }
        }

        public override void Load()
        {
            AutoloadVanillaItemChange();

            RadianceBar = new RadianceBar();
            _radianceBarUserInterface = new UserInterface();
            _radianceBarUserInterface.SetState(RadianceBar);
        }

        public override void Unload()
        {
            VanillaItemChange.VanillaItemChanges.Clear();
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
