using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SharpCrawler
{
    public class Button
    {
        //FIELDS
        private SpriteFont ButtonFont;
        private Sprite ButtonSkin;
        private string ButtonText;
        private Vector2 TextPosition;
        private Color ButtonColor;
        private Rectangle Hitbox;
        private Func<Button, bool> OnClickEvent;
        public Func<Button, bool> GetOnClick()
        {
            return this.OnClickEvent;
        }
        //CONSTRUCTOR
        public Button(ContentManager Content, string fontPath, string text,float x, float y, Func<Button, bool> onClickEvent)
        {
            this.ButtonFont = Content.Load<SpriteFont>(fontPath);
            this.ButtonSkin = new Sprite("Button", (int)x, (int)y, 0.1f);
            if(text.Split(' ').Count() < 3)
            {
                this.ButtonText = text;
                this.TextPosition = Vector2.Subtract(this.ButtonSkin.GetPositionVector(), this.ButtonFont.MeasureString(text) / 2);
                if(ButtonText.Length > 10)
                {
                    this.ButtonText = this.ButtonText.Replace(' ', '\n');
                    this.TextPosition = new Vector2(this.ButtonSkin.GetPositionVector().X - this.ButtonSkin.Width / 2 + 15, this.ButtonSkin.GetPositionVector().Y - 10);
                }
            }
            else
            {
                for (int i = 0; i < text.Split(' ').Count(); i++)
                {
                    this.ButtonText += $"{text.Split(' ')[i]} ";
                    if ((i + 1) % 2 == 0)
                        this.ButtonText += "\n";
                }
                this.TextPosition = new Vector2(this.ButtonSkin.GetPositionVector().X - this.ButtonSkin.Width/2 + 15, this.ButtonSkin.GetPositionVector().Y - 10);
            }
            this.ButtonColor = Color.Black;
            this.Hitbox = new Rectangle((int)(x - this.ButtonSkin.Width/2), (int)(y - this.ButtonSkin.Height/2),
                                        this.ButtonSkin.Width, this.ButtonSkin.Height);
            this.OnClickEvent = onClickEvent;

        }
        //METHODS
        private void MouseHover(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.Hitbox))
            {
                this.ButtonColor = new Color(this.ButtonColor, 0.5f);
                this.ButtonSkin.SetColor(new Color(this.ButtonSkin.GetColor(), 0.5f));
            }
            else
            {
                this.ButtonColor = Color.Black;
                this.ButtonSkin.SetColor(new Color(this.ButtonSkin.GetColor(), 0.5f));
            }
        }
        private void MouseClick(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.Hitbox))
            {
                if (mouseState.IsMousePressed())
                {
                    this.OnClickEvent(this);
                }
            }
        }
        
        //UPDATE & DRAW
        public void Update(Input mouseState)
        {
            MouseHover(mouseState);
            MouseClick(mouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.ButtonSkin.Draw(spriteBatch);
            spriteBatch.DrawString(this.ButtonFont, this.ButtonText, this.TextPosition, this.ButtonColor);
        }
    }
}
