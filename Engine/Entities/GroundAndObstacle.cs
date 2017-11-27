﻿using System;
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
        public GroundAndObstacle(Sprite objectTexture, bool realHitbox, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0, bool staticObject = true, bool noHitbox = false)
                : base(objectTexture, realHitbox,  hitboxWidth, hitboxHeight, relativeX, relativeY, noHitbox)
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
            this.sprite.DrawFromSpriteSheet(spriteBatch);
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
