using Microsoft.Xna.Framework;
using RadiantArsenal.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Reflection;

namespace RadiantArsenal.Items
{
    public class VanillaItemChange
    {
        public static Dictionary<int, VanillaItemChange> VanillaItemChanges = new Dictionary<int, VanillaItemChange>();

        public Mod mod => ModLoader.GetMod("RadiantArsenal");

        public virtual int Type
        {
            get;
            set;
        }

        /// <summary>
        /// This is where you set all your item's properties, such as width, damage, shootSpeed, defense, etc. 
        /// For those that are familiar with tAPI, this has the same function as .json files.
        /// </summary>
        public virtual void SetDefaults(Item item) { }

        public virtual bool AltFunctionUse(Item item, Player player) => false;

        public virtual bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) => true;

        public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips) { }

        public virtual bool CanUseItem(Item item, Player player) => true;

        public virtual void HoldItem(Item item, Player player) { }

        public virtual void UpdateAccessory(Item item, Player player, bool hideVisual) { }
    }
}