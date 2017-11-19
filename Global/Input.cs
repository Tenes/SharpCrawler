using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class Input
    {
        //FIELDS
        private MouseState OldClick;
        private MouseState CurrentClick;
        private KeyboardState OldState;
        private KeyboardState CurrentState;
        
        public KeyboardState GetOldState()
        {
            return this.OldState;
        }
        public KeyboardState GetCurrentState()
        {
            return this.CurrentState;
        }
        //CONSTRUCTOR
        public Input(KeyboardState oldState, KeyboardState currentState)
        {
            this.OldState = oldState;
            this.CurrentState = currentState;
        }
        public Input(MouseState oldClick, MouseState currentClick)
        {
            this.OldClick = oldClick;
            this.CurrentClick = currentClick;
        }

        //METHODS
        #region MouseControls
        public bool IsMouseOver(Rectangle hitbox)
        {
            return hitbox.Intersects(new Rectangle(CurrentClick.Position.X, CurrentClick.Position.Y, 1, 1));
        }
        public bool IsMouseDown()
        {
            return (this.CurrentClick.LeftButton == ButtonState.Pressed);
        }
        public bool IsMouseUp()
        {
            return (this.CurrentClick.LeftButton == ButtonState.Released);
        }
        public bool IsMousePressed()
        {
            return this.CurrentClick.LeftButton == ButtonState.Released && this.OldClick.LeftButton == ButtonState.Pressed;
        }
        #endregion

        #region KeyboardControls
        public bool IsKeyDown(Keys key)
        {
            return this.CurrentState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return this.CurrentState.IsKeyUp(key);
        }
        public bool IsKeyPressed(Keys key)
        {
            return this.OldState.IsKeyDown(key) && this.CurrentState.IsKeyUp(key);
        }
        #endregion
    }
}