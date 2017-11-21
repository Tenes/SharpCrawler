using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class GroundAndObstacle : Entity
    {
        //FIELDS
        private bool staticObject;

        //CONSTRUCTOR
        public GroundAndObstacle(Sprite objectTexture, bool realHitbox, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0, bool staticObject = true)
                : base(objectTexture, realHitbox,  hitboxWidth, hitboxHeight, relativeX, relativeY)
        {
            this.sprite = objectTexture;
            this.staticObject = staticObject;
        }

        //METHODS


        //UPDATE & DRAW
        public void Update(float delta)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.hitbox.DrawDebug(spriteBatch);
            this.sprite.DrawFromSpriteSheet(spriteBatch);
        }
        
        public void DrawToDestination(SpriteBatch spriteBatch)
        {
            this.hitbox.DrawDebug(spriteBatch);
            this.sprite.DrawFromSpriteSheetToDestination(spriteBatch);
        }
    }
}
