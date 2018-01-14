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
        
        public void SetOldStates(KeyboardState oldState, MouseState oldClick)
        {
            this.OldState = oldState;
            this.OldClick = oldClick;
        }
        public void SetCurrentStates(KeyboardState currentState, MouseState currentClick)
        {
            this.CurrentState = currentState;
            this.CurrentClick = currentClick;
        }
        public KeyboardState GetOldKeyboardState()
        {
            return this.OldState;
        }
        public KeyboardState GetKeyboardCurrentState()
        {
            return this.CurrentState;
        }
        public MouseState GetOldMouseState()
        {
            return this.OldClick;
        }
        public MouseState GetMouseCurrentState()
        {
            return this.CurrentClick;
        }
        //CONSTRUCTOR
        public Input(KeyboardState oldState, KeyboardState currentState, MouseState oldClick, MouseState currentClick)
        {
            this.OldState = oldState;
            this.CurrentState = currentState;
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