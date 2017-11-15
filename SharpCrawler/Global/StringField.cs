using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SharpCrawler
{
    public class StringField
    {
        //FIELDS
        private string AboveText;
        private Vector2 AboveTextPosition;
        private SpriteFont CaseFont;
        private Sprite CaseSkin;
        private string CaseText;
        private string SecuredText;
        private Vector2 TextPosition;
        private Color TextColor;
        private Rectangle Case;
        private char TempChar;
        private int CharacterLimit;
        private bool IsSelected = false;
        private string TextBar = "|";
        private Vector2 TextBarPosition;
        private float Timer = 500;
        private bool IsSecured;
        private bool Special;
        public void Flush()
        {
            this.CaseText = string.Empty;
        }
        public string GetText()
        {
            return this.CaseText;
        }
        public string GetSecuredText()
        {
            return this.SecuredText;
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
        public StringField(ContentManager Content, string fontPath, string text, float x, float y, int characterLimit = 16, bool isSecured = false, bool special = false)
        {
            this.AboveText = text;
            this.CaseFont = Content.Load<SpriteFont>(fontPath);
            this.CaseSkin = new Sprite("TextInput", (int)x, (int)y);
            this.TextPosition = new Vector2(x - this.CaseSkin.GetSpriteWidth() / 2 + 10, y - 5);
            this.TextColor = Color.Black;
            this.CaseText = "";
            this.Case = new Rectangle((int)(x - this.CaseSkin.GetSpriteWidth() / 2), (int)(y - this.CaseSkin.GetSpriteHeight() / 2),
                                        this.CaseSkin.GetSpriteWidth(), this.CaseSkin.GetSpriteHeight());
            this.AboveTextPosition = new Vector2(x  - this.CaseFont.MeasureString(text).X / 2, y - this.CaseSkin.GetSpriteHeight() / 2 - 20);
            this.CharacterLimit = characterLimit;
            this.IsSecured = isSecured;
            this.Special = special;
        }


        //METHODS
        public bool TryConvertKeyboardInput(Input input, out char key)
        {
            Keys[] keys = input.GetCurrentState().GetPressedKeys();

            //#region Get Keys
            //if (input.GetCurrentState().IsKeyDown(Keys.A)) keys.Add(Keys.A);
            //if (input.GetCurrentState().IsKeyDown(Keys.B)) keys.Add(Keys.B);
            //if (input.GetCurrentState().IsKeyDown(Keys.C)) keys.Add(Keys.C);
            //if (input.GetCurrentState().IsKeyDown(Keys.D)) keys.Add(Keys.D);
            //if (input.GetCurrentState().IsKeyDown(Keys.E)) keys.Add(Keys.E);
            //if (input.GetCurrentState().IsKeyDown(Keys.F)) keys.Add(Keys.F);
            //if (input.GetCurrentState().IsKeyDown(Keys.G)) keys.Add(Keys.G);
            //if (input.GetCurrentState().IsKeyDown(Keys.H)) keys.Add(Keys.H);
            //if (input.GetCurrentState().IsKeyDown(Keys.I)) keys.Add(Keys.I);
            //if (input.GetCurrentState().IsKeyDown(Keys.J)) keys.Add(Keys.J);
            //if (input.GetCurrentState().IsKeyDown(Keys.K)) keys.Add(Keys.K);
            //if (input.GetCurrentState().IsKeyDown(Keys.L)) keys.Add(Keys.L);
            //if (input.GetCurrentState().IsKeyDown(Keys.M)) keys.Add(Keys.M);
            //if (input.GetCurrentState().IsKeyDown(Keys.N)) keys.Add(Keys.N);
            //if (input.GetCurrentState().IsKeyDown(Keys.O)) keys.Add(Keys.O);
            //if (input.GetCurrentState().IsKeyDown(Keys.P)) keys.Add(Keys.P);
            //if (input.GetCurrentState().IsKeyDown(Keys.Q)) keys.Add(Keys.Q);
            //if (input.GetCurrentState().IsKeyDown(Keys.R)) keys.Add(Keys.R);
            //if (input.GetCurrentState().IsKeyDown(Keys.S)) keys.Add(Keys.S);
            //if (input.GetCurrentState().IsKeyDown(Keys.T)) keys.Add(Keys.T);
            //if (input.GetCurrentState().IsKeyDown(Keys.U)) keys.Add(Keys.U);
            //if (input.GetCurrentState().IsKeyDown(Keys.V)) keys.Add(Keys.V);
            //if (input.GetCurrentState().IsKeyDown(Keys.W)) keys.Add(Keys.W);
            //if (input.GetCurrentState().IsKeyDown(Keys.X)) keys.Add(Keys.X);
            //if (input.GetCurrentState().IsKeyDown(Keys.Y)) keys.Add(Keys.Y);
            //if (input.GetCurrentState().IsKeyDown(Keys.Z)) keys.Add(Keys.Z);
            //if (input.GetCurrentState().IsKeyDown(Keys.LeftShift) ||
            //    input.GetCurrentState().IsKeyDown(Keys.RightShift)) keys.Add(Keys.LeftShift);


            //if (input.GetCurrentState().IsKeyDown(Keys.D0)) keys.Add(Keys.D0);
            //if (input.GetCurrentState().IsKeyDown(Keys.D1)) keys.Add(Keys.D1);
            //if (input.GetCurrentState().IsKeyDown(Keys.D2)) keys.Add(Keys.D2);
            //if (input.GetCurrentState().IsKeyDown(Keys.D3)) keys.Add(Keys.D3);
            //if (input.GetCurrentState().IsKeyDown(Keys.D4)) keys.Add(Keys.D4);
            //if (input.GetCurrentState().IsKeyDown(Keys.D5)) keys.Add(Keys.D5);
            //if (input.GetCurrentState().IsKeyDown(Keys.D6)) keys.Add(Keys.D6);
            //if (input.GetCurrentState().IsKeyDown(Keys.D7)) keys.Add(Keys.D7);
            //if (input.GetCurrentState().IsKeyDown(Keys.D8)) keys.Add(Keys.D8);
            //if (input.GetCurrentState().IsKeyDown(Keys.D9)) keys.Add(Keys.D9);

            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad0)) keys.Add(Keys.NumPad0);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad1)) keys.Add(Keys.NumPad1);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad2)) keys.Add(Keys.NumPad2);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad3)) keys.Add(Keys.NumPad3);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad4)) keys.Add(Keys.NumPad4);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad5)) keys.Add(Keys.NumPad5);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad6)) keys.Add(Keys.NumPad6);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad7)) keys.Add(Keys.NumPad7);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad8)) keys.Add(Keys.NumPad8);
            //if (input.GetCurrentState().IsKeyDown(Keys.NumPad9)) keys.Add(Keys.NumPad9);

            //if (input.GetCurrentState().IsKeyDown(Keys.OemSemicolon)) keys.Add(Keys.OemSemicolon);
            //if (input.GetCurrentState().IsKeyDown(Keys.OemTilde)) keys.Add(Keys.OemTilde);
            //if (input.GetCurrentState().IsKeyDown(Keys.Space)) keys.Add(Keys.Space);
            //if (input.GetCurrentState().IsKeyDown(Keys.Back)) keys.Add(Keys.Back);

            //#endregion

            bool shift = input.GetCurrentState().IsKeyDown(Keys.LeftShift);
            foreach(Keys usedKey in keys)
            {
                if (input.GetOldState().IsKeyUp(usedKey))
                {
                    switch (usedKey)
                    {
                        //Alphabet keys
                        case Keys.A: if (shift) { key = 'Q'; } else { key = 'q'; } return true;
                        case Keys.B: if (shift) { key = 'B'; } else { key = 'b'; } return true;
                        case Keys.C: if (shift) { key = 'C'; } else { key = 'c'; } return true;
                        case Keys.D: if (shift) { key = 'D'; } else { key = 'd'; } return true;
                        case Keys.E: if (shift) { key = 'E'; } else { key = 'e'; } return true;
                        case Keys.F: if (shift) { key = 'F'; } else { key = 'f'; } return true;
                        case Keys.G: if (shift) { key = 'G'; } else { key = 'g'; } return true;
                        case Keys.H: if (shift) { key = 'H'; } else { key = 'h'; } return true;
                        case Keys.I: if (shift) { key = 'I'; } else { key = 'i'; } return true;
                        case Keys.J: if (shift) { key = 'J'; } else { key = 'j'; } return true;
                        case Keys.K: if (shift) { key = 'K'; } else { key = 'k'; } return true;
                        case Keys.L: if (shift) { key = 'L'; } else { key = 'l'; } return true;
                        case Keys.OemSemicolon: if (shift) { key = 'M'; } else { key = 'm'; } return true;
                        case Keys.N: if (shift) { key = 'N'; } else { key = 'n'; } return true;
                        case Keys.O: if (shift) { key = 'O'; } else { key = 'o'; } return true;
                        case Keys.P: if (shift) { key = 'P'; } else { key = 'p'; } return true;
                        case Keys.Q: if (shift) { key = 'A'; } else { key = 'a'; } return true;
                        case Keys.R: if (shift) { key = 'R'; } else { key = 'r'; } return true;
                        case Keys.S: if (shift) { key = 'S'; } else { key = 's'; } return true;
                        case Keys.T: if (shift) { key = 'T'; } else { key = 't'; } return true;
                        case Keys.U: if (shift) { key = 'U'; } else { key = 'u'; } return true;
                        case Keys.V: if (shift) { key = 'V'; } else { key = 'v'; } return true;
                        case Keys.W: if (shift) { key = 'Z'; } else { key = 'z'; } return true;
                        case Keys.X: if (shift) { key = 'X'; } else { key = 'x'; } return true;
                        case Keys.Y: if (shift) { key = 'Y'; } else { key = 'y'; } return true;
                        case Keys.Z: if (shift) { key = 'W'; } else { key = 'w'; } return true;

                        //Decimal keys
                        case Keys.D0: if (shift) { key = '0'; } else { key = '@'; } return true;
                        case Keys.D1: if (shift) { key = '1'; } else { key = '&'; } return true;
                        case Keys.D2: if (shift) { key = '2'; } else { key = '~'; } return true;
                        case Keys.D3: key = '3'; return true;
                        case Keys.D4: key = '4'; return true;
                        case Keys.D5: key = '5'; return true;
                        case Keys.D6: if (shift) { key = '6'; } else { key = '-'; } return true;
                        case Keys.D7: if (shift) { key = '7'; } else { key = '`'; } return true;
                        case Keys.D8: if (shift) { key = '8'; } else { key = '_'; } return true;
                        case Keys.D9: if (shift) { key = '9'; } else { key = '^'; } return true;

                        //Decimal numpad keys
                        case Keys.NumPad0: key = '0'; return true;
                        case Keys.NumPad1: key = '1'; return true;
                        case Keys.NumPad2: key = '2'; return true;
                        case Keys.NumPad3: key = '3'; return true;
                        case Keys.NumPad4: key = '4'; return true;
                        case Keys.NumPad5: key = '5'; return true;
                        case Keys.NumPad6: key = '6'; return true;
                        case Keys.NumPad7: key = '7'; return true;
                        case Keys.NumPad8: key = '8'; return true;
                        case Keys.NumPad9: key = '9'; return true;


                        case Keys.Back: this.Delete(); break;
                        
                        //Special keys
                        case Keys.OemComma: if (this.Special) { if (shift) { key = '.'; } else { key = ';'; } return true; } else break;
                        case Keys.OemPeriod: if (this.Special) { if (shift) { key = '/'; } else { key = ':'; } return true; } else break;
                        case Keys.Space: key = ' '; return true;
                    }
                }
            }
            
            
            key = '|';
            return false;
        }

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
            {
                this.CaseText = this.CaseText.Substring(0, this.CaseText.Length - 1);
                this.SecuredText = this.SecuredText.Substring(0, this.SecuredText.Length - 1);
            }
            else
                return;
        }
        //UPDATE & DRAW
        public void Update(GameTime gameTime, Input keyboardInput, Input mouseInput)
        {
            this.TextBarPosition = new Vector2(this.TextPosition.X + this.CaseFont.MeasureString(this.CaseText).X + 0.5f, this.TextPosition.Y);
            if (this.MouseClickOn(mouseInput))
                this.IsSelected = true;
            if (this.MouseClickOff(mouseInput))
                this.IsSelected = false;

            if (this.IsSelected)
            {
                this.Timer -= (float)gameTime.ElapsedGameTime.Milliseconds;
                if (TryConvertKeyboardInput(keyboardInput, out TempChar))
                {
                    if (this.CaseText.Length < this.CharacterLimit)
                    {
                        if (this.IsSecured)
                        {
                            this.CaseText += "*";
                            this.SecuredText += TempChar;
                        }
                        else
                        {
                            this.CaseText += TempChar;
                            this.SecuredText += TempChar;
                        }
                    }
                }
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
