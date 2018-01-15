using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SharpCrawler
{
    public class ClickableText
    {
        //FIELDS
        private SpriteFont textFont;
        private string text;
        private Vector2 textPosition;
        private Color textColor;
        private Rectangle hitbox;
        private Vector2 origin;
        private Func<ClickableText, bool> OnClickEvent;
        public Func<ClickableText, bool> GetOnClick() => this.OnClickEvent;
        //CONSTRUCTOR
        public ClickableText(string text,float x, float y, Func<ClickableText, bool> onClickEvent)
        {
            this.textFont = Ressources.fonts["MainFont"];
            this.text = text;
            this.textPosition = new Vector2(x, y);
            this.textColor = Color.White;
            this.hitbox = new Rectangle((int)(x - this.textFont.MeasureString(this.text).X* 0.5f), (int)(y - this.textFont.MeasureString(this.text).Y * 0.5f),
                                        (int)this.textFont.MeasureString(this.text).X, (int)this.textFont.MeasureString(this.text).Y);
            this.OnClickEvent = onClickEvent;
            this.origin = this.textFont.MeasureString(this.text) * 0.5f;
        }
        //METHODS
        private void UpdatePosition(float x, float y)
        {
            this.textPosition.X = x;
            this.textPosition.Y = y;
        }
        private void MouseHover(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.hitbox))
                this.textColor = Color.DarkCyan;
            else
                this.textColor = Color.LightSteelBlue;
        }
        private void MouseClick(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.hitbox))
                if (mouseState.IsMousePressed())
                    this.OnClickEvent(this);
        }
        
        //UPDATE & DRAW
        public void Update(Input mouseState)
        {
            this.MouseHover(mouseState);
            this.MouseClick(mouseState);
        }

        public void Update(Input mouseState, float x, float y)
        {
            this.UpdatePosition(x, y);
            this.MouseHover(mouseState);
            this.MouseClick(mouseState);
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.DrawString(this.textFont, this.text, this.textPosition, this.textColor, 0, this.origin, 1f, SpriteEffects.None, 0.1f);
    }
}
