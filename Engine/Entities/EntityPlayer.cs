﻿using System;
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
        private string name;
        private Map actualMap;
        private byte teleportationTime;
        private Hand entityHand;
        public void SetActualMap(Map map)
        {
            this.actualMap = map;
        }
        public Map GetActualMap()
        {
            return this.actualMap;
        }
        public Hand GetHand()
        {
            return this.entityHand;
        }
        //CONSTRUCTOR
        public EntityPlayer(Sprite sprite,  bool realHitbox, string name, Hand hand, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0, bool noHitbox = false)
                : base(sprite, realHitbox, hitboxWidth, hitboxHeight, relativeX, relativeY, noHitbox)
        {
            this.name = name;
            this.entityHand = hand;
            this.entityHand.SetHolder(this);
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
                this.entityHand.GetWeapon()?.SetState(State.MovingLeft);
            }
            if (input.IsKeyDown(Keys.D)) //Move right
            {
                this.velocity.X += (Settings.pixelRatio*delta)*20;
                this.currentState = State.MovingRight;
                this.entityHand.GetWeapon()?.SetState(State.MovingRight);
            }

        }

        //UPDATE & DRAW
        public override void Update(GameTime gameTime, CameraClass camera, Input input, float delta)
        {
            this.Mouvement(input, delta);
            this.entityHand.Update((this.currentState == State.MovingLeft) ?(int)this.offsetPosition.X + 8:(int)this.offsetPosition.X - 8,
                                    (int)this.offsetPosition.Y+ 16);
            base.Update(gameTime, camera, input, delta);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //this.hitbox.DrawDebug(spriteBatch);
            this.sprite.DrawFromSpriteSheet(spriteBatch);
            this.entityHand.Draw(spriteBatch);
        }
    }
}
