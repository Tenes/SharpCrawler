using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class EntityPlayer : Entity
    {
        //FIELDS
        private string Name;
        //CONSTRUCTOR
        public EntityPlayer(Sprite sprite, string name) 
                : base (sprite)
        {
            this.Name = name;
        }

        public EntityPlayer(Sprite sprite, string name, float hitboxWidth, float hitboxHeight)
                : base(sprite, hitboxWidth, hitboxHeight)
        {
            this.Name = name;
        }

        //METHODS
        public void Mouvement(Input input, float delta)
        {
            if (input.IsKeyDown(Keys.Z)) //Move up
                this.velocity.Y -= (Settings.pixelRatio * delta) * 20;
            if (input.IsKeyDown(Keys.S)) //Move up
                this.velocity.Y += (Settings.pixelRatio * delta) * 20;
            if (input.IsKeyDown(Keys.Q)) //Move left
                this.velocity.X -= (Settings.pixelRatio*delta)*20;
            if (input.IsKeyDown(Keys.D)) //Move right
                this.velocity.X += (Settings.pixelRatio*delta)*20;

            if (Math.Abs(this.velocity.X) > 0.4)
                this.currentState = "Moving";
            else
                this.currentState = "Idle";

        }

        //UPDATE & DRAW
        public override void Update(GameTime gameTime, Input input, float delta)
        {
            this.Mouvement(input, delta);
            base.Update(gameTime, input, delta);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.DrawFromSpriteSheet(spriteBatch);
        }
    }
}
