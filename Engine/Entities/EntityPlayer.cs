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
        public EntityPlayer(Sprite sprite,  bool realHitbox, string name, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0)
                : base(sprite, realHitbox, hitboxWidth, hitboxHeight, relativeX, relativeY)
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
            {
                this.velocity.X -= (Settings.pixelRatio*delta)*20;
                this.currentState = State.MovingLeft;
            }
            if (input.IsKeyDown(Keys.D)) //Move right
            {
                this.velocity.X += (Settings.pixelRatio*delta)*20;
                this.currentState = State.MovingRight;
            }

        }

        //UPDATE & DRAW
        public override void Update(GameTime gameTime, CameraClass camera, Input input, float delta)
        {
            this.Mouvement(input, delta);
            base.Update(gameTime, camera, input, delta);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.hitbox.DrawDebug(spriteBatch);
            this.sprite.DrawFromSpriteSheet(spriteBatch);
        }
    }
}
