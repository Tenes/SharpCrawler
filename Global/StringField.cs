using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace SharpCrawler
{
    public class StringField
    {
        //FIELDS
        private string AboveText;
        private Vector2 AboveTextPosition;
        private SpriteFont CaseFont;
        private Sprite CaseSkin;
        private StringBuilder CaseText;
        private Vector2 TextPosition;
        private Color TextColor;
        private Rectangle Case;
        private int CharacterLimit;
        private bool IsSelected = false;
        private string TextBar = "|";
        private Vector2 TextBarPosition;
        private float Timer = 500;
        public void Flush()
        {
            this.CaseText.Clear();
        }
        public string GetText()
        {
            return this.CaseText.ToString();
        }
        public bool GetSelected()
        {
            return  this.IsSelected;
        }
        public void SwitchSelected()
        {
            this.IsSelected = !this.IsSelected;
        }
        //CONSTRUCTOR
        public StringField(GameWindow window, ContentManager Content, string text, float x, float y, int characterLimit = 16)
        {
            this.AboveText = text;
            this.CaseFont = Content.Load<SpriteFont>("font/MainFont");
            this.CaseSkin = new Sprite("TextInput", (int)x, (int)y, 0.1f);
            this.TextPosition = new Vector2(x - this.CaseSkin.Width / 2 + 10, y - 5);
            this.TextBarPosition = this.TextPosition;
            this.TextColor = Color.Black;
            this.CaseText = new StringBuilder();
            this.Case = new Rectangle((int)(x - this.CaseSkin.Width / 2), (int)(y - this.CaseSkin.Height / 2),
                                        this.CaseSkin.Width, this.CaseSkin.Height);
            this.AboveTextPosition = new Vector2(x  - this.CaseFont.MeasureString(text).X / 2, y - this.CaseSkin.Height / 2 - 20);
            this.CharacterLimit = characterLimit;
            window.TextInput += TextEntered;
        }


        //METHODS
        private bool MouseClickOn(Input mouseState)
        {
            if (mouseState.IsMouseOver(this.Case))
            {
                if (mouseState.IsMouseDown())
                    return true;
            }
            return false;
        }
        private bool MouseClickOff(Input mouseState)
        {
            if (!mouseState.IsMouseOver(this.Case))
            {
                if (mouseState.IsMouseDown())
                    return true;
            }
            return false;
        }
        private void Delete()
        {
            if (this.CaseText.Length > 0)
                this.CaseText.Remove(this.CaseText.Length - 1, 1);
            else
                return;
        }

        private void TextEntered(object sender, TextInputEventArgs e)
        {
            if(this.IsSelected)
            {
                if(e.Key == Keys.Back)
                    this.Delete();
                else if(this.CaseText.Length < this.CharacterLimit && NotSpecial(e.Character))
                    this.CaseText.Append(e.Character);
                
                this.TextBarPosition = new Vector2(this.TextPosition.X + this.CaseFont.MeasureString(this.CaseText).X + 0.5f, this.TextPosition.Y);
            }
        }
        private bool NotSpecial(char e)
        {
            if(!this.CaseFont.Characters.Contains(e))
                return false;
            return true;
        }
        //UPDATE & DRAW
        public void Update(GameTime gameTime, Input generalInput)
        {
            if (this.MouseClickOn(generalInput))
                this.IsSelected = true;
            if (this.MouseClickOff(generalInput))
                this.IsSelected = false;

            if (this.IsSelected)
            {
                this.Timer -= (float)gameTime.ElapsedGameTime.Milliseconds;
                if (this.Timer <= 0)
                {
                    if (this.TextBar == "")
                        this.TextBar = "|";
                    else if (this.TextBar == "|")
                        this.TextBar = "";
                    this.Timer = 500;
                }
            }
            else
                this.TextBar = "";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.CaseFont, this.AboveText, this.AboveTextPosition, Color.White);
            this.CaseSkin.Draw(spriteBatch);
            spriteBatch.DrawString(this.CaseFont, this.CaseText, this.TextPosition, this.TextColor);
            spriteBatch.DrawString(this.CaseFont, this.TextBar, this.TextBarPosition, this.TextColor);
        }
    }
}
