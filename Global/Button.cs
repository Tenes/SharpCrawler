using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SharpCrawler
{
    public class Button
    {
        //FIELDS
        private SpriteFont ButtonFont;
        private Sprite currentSkin;
        private Sprite ButtonSkin;
        private Sprite AltButtonSkin;
        private string ButtonText;
        private Vector2 TextPosition;
        private Color ButtonColor;
        private Rectangle Hitbox;
        private Vector2 origin;
        private Func<Button, bool> OnClickEvent;
        public Func<Button, bool> GetOnClick() => this.OnClickEvent;
        //CONSTRUCTOR
        public Button(string text,float x, float y, Func<Button, bool> onClickEvent)
        {
            this.ButtonFont = Ressources.fonts["MainFont"];
            this.ButtonSkin = new Sprite("UnpressedButton", (int)x, (int)y, 0.15f);
            this.AltButtonSkin = new Sprite("PressedButton", (int)x, (int)y + 3, 0.15f);
            this.currentSkin = ButtonSkin;
            this.ButtonText = text;
            this.TextPosition = new Vector2(x, y - 20);
            this.ButtonColor = Color.Black;
            this.Hitbox = new Rectangle((int)(x - this.ButtonSkin.Width* 0.5f), (int)(y - this.ButtonSkin.Height),
                                        this.ButtonSkin.Width, this.ButtonSkin.Height);
            this.OnClickEvent = onClickEvent;
            this.origin = this.ButtonFont.MeasureString(this.ButtonText) * 0.5f;
        }
        //METHODS
        private void UpdatePosition(float x, float y)
        {
            this.ButtonSkin.SetPosition(x, y);
            this.AltButtonSkin.SetPosition(x, y + 3);
            this.TextPosition.X = x;
            this.TextPosition.Y = y - 17;
        }
        private void MouseHover(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.Hitbox))
            {
                this.ButtonColor = new Color(this.ButtonColor, 0.5f);
                this.currentSkin.SetColor(new Color(this.ButtonSkin.GetColor(), 0.5f));
            }
            else
            {
                this.ButtonColor = Color.Black;
                this.currentSkin.SetColor(new Color(this.ButtonSkin.GetColor(), 0.5f));
            }
        }
        private void MouseClick(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.Hitbox))
            {
                if(mouseState.IsMouseDown())
                {
                    this.currentSkin = this.AltButtonSkin;
                    this.TextPosition.Y += 3;
                }
                if (mouseState.IsMousePressed())
                {
                    this.currentSkin = this.ButtonSkin;
                    this.TextPosition.Y -= 3;
                    this.OnClickEvent(this);
                }
            }
        }
        
        //UPDATE & DRAW
        public void Update(Input mouseState, float x, float y)
        {
            this.UpdatePosition(x, y);
            this.MouseHover(mouseState);
            this.MouseClick(mouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.currentSkin.Draw(spriteBatch);
            spriteBatch.DrawString(this.ButtonFont, this.ButtonText, this.TextPosition, this.ButtonColor, 0, this.origin, 1f, SpriteEffects.None, 0.1f);
        }
    }
}
