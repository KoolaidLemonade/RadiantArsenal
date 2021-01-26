using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using RadiantArsenal.Items;

namespace RadiantArsenal.UI
{
	internal class RadianceBar : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 66, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(310, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(38, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(170, 0f);

			barFrame = new UIImage(ModContent.GetTexture("RadiantArsenal/UI/RadianceFrame"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(450, 0f);

			text = new UIText("0/0", 0.65f); // text to show stat
			text.Width.Set(50, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(175, 0f);
			text.Left.Set(15, 0f);

			gradientA = new Color(235, 230, 103); 
			gradientB = new Color(213, 145, 116); 

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			area.Top.Set(40 + Main.LocalPlayer.statManaMax2 * 1.37f, 0f);

			var modPlayer = Main.LocalPlayer.GetModPlayer<RadiancePlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.radianceCurrent / modPlayer.radianceMax2; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 6;
			hitbox.Width -= 14;
			hitbox.Y += 8;
			hitbox.Height -= 20;

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int top = hitbox.Top;
			int bottom = hitbox.Bottom;
			int steps = (int)((bottom - top) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (bottom - top);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(hitbox.X, top + i, hitbox.Width, 1), Color.Lerp(gradientA, gradientB, percent));
			}
		}
		public override void Update(GameTime gameTime)
		{
			Player player = Main.LocalPlayer;

			var modPlayer = Main.LocalPlayer.GetModPlayer<RadiancePlayer>();
			// Setting the text per tick to update and show our resource values.
			text.SetText($"{modPlayer.radianceCurrent} / {modPlayer.radianceMax2}");
			base.Update(gameTime);
		}
	}
}
