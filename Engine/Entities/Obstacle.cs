﻿using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class Obstacle : Entity
    {
        //CONSTRUCTOR
        public Obstacle(Sprite objectTexture, bool realHitbox, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0)
                : base(objectTexture, realHitbox,  hitboxWidth, hitboxHeight, relativeX, relativeY, false)
        {
        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
            if(this.HasHitbox())
                this.hitbox.DrawDebug(spriteBatch);
            this.sprite.DrawFromSpriteSheet(spriteBatch);
        }
        
        public void DrawToDestinationDebug(SpriteBatch spriteBatch)
        {
            if(this.HasHitbox())
                this.hitbox.DrawDebug(spriteBatch);
            this.sprite.DrawFromSpriteSheetToDestination(spriteBatch);
        }
    }
}
