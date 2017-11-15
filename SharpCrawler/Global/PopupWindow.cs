using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class PopupWindow
    {
        //FIELDS
        private Sprite Box;
        private SpriteFont TextFont;
        private Vector2 TextPosition;
        private string Text;
        private Button Close;
        private Color TextColor;
        private bool Activated;
        public bool IsActivated()
        {
            return this.Activated;
        }
        public void SwitchPopup()
        {
            this.Activated = !this.Activated;
        }

        //CONSTRUCTOR
        public PopupWindow(ContentManager content, string text, int x, int y, Color alertColor, Func<Button, bool> onClickEvent)
        {
            this.Box = new Sprite("Popup", x, y);
            this.TextFont = content.Load<SpriteFont>("font/MainFont");
            this.TextPosition = Vector2.Subtract(this.Box.GetPositionVector(), this.TextFont.MeasureString(text) / 2);
            this.TextPosition.Y -= 40;
            this.Text = text;
            this.Close = new Button(content, "font/MainFont", "Close", x, y + 100, onClickEvent);
            this.TextColor = alertColor;
            this.Activated = false;
        }
        public PopupWindow(ContentManager content, string text, int x, int y, Color alertColor)
        {
            this.Box = new Sprite("Popup", x, y);
            this.TextFont = content.Load<SpriteFont>("font/MainFont");
            this.TextPosition = Vector2.Subtract(this.Box.GetPositionVector(), this.TextFont.MeasureString(text) / 2);
            this.TextPosition.Y -= 40;
            this.Text = text;
            this.Close = new Button(content, "font/MainFont", "Close", x, y + 100, (thisButton) => {
                    this.SwitchPopup();
                    return true;
                });
            this.TextColor = alertColor;
            this.Activated = false;
        }
        //METHODS


        //UPDATE & DRAW
        public void Update(GameTime gameTime, Input keyboardState = null, Input mouseState = null)
        {
            this.Close.Update(mouseState);
            if (keyboardState.IsKeyPressed(Keys.Enter))
            {
                this.Close.GetOnClick()(this.Close);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            this.Box.Draw(spriteBatch);
            spriteBatch.DrawString(this.TextFont, this.Text, this.TextPosition, this.TextColor);
            this.Close.Draw(spriteBatch);
        }
    }
}
