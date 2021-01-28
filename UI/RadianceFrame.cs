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
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			if (Main.dedServ) return;

			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 66, 1f);
			area.Top.Set(310, 0f); 
			area.Width.Set(38, 0f); 
			area.Height.Set(170, 0f);

			barFrame = new UIImage(ModContent.GetTexture("RadiantArsenal/UI/RadianceFrame"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(450, 0f);

			text = new UIText("0/0", 0.65f); 
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

			float quotient = (float)modPlayer.radianceCurrent / modPlayer.radianceMax2; 
			quotient = Utils.Clamp(quotient, 0f, 1f); 

			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 6;
			hitbox.Width -= 14;
			hitbox.Y += 8;
			hitbox.Height -= 20;

			int top = hitbox.Top;
			int bottom = hitbox.Bottom;
			int steps = (int)((bottom - top) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				float percent = (float)i / (bottom - top);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(hitbox.X, top + i, hitbox.Width, 1), Color.Lerp(gradientA, gradientB, percent));
			}
		}
		public override void Update(GameTime gameTime)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<RadiancePlayer>();
			text.SetText($"{modPlayer.radianceCurrent} / {modPlayer.radianceMax2}");
			base.Update(gameTime);
		}
	}
}
