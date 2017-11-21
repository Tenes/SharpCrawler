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
    public abstract class Entity
    {
        //FIELDS
        protected Sprite sprite;
        protected Vector2 position;
        protected Vector2 offsetPosition;
        protected Vector2 velocity;
        protected float friction;

        protected Hitbox hitbox;
        public enum State{MovingRight, MovingLeft}
        protected State currentState;

        //SETTER-GETTER
        public float GetPositionX()
        {
            return this.position.X;
        }
        public float GetPositionY()
        {
            return this.position.Y;
        }
        public void SetOffsetPosition(int x, int y)
        {
            this.offsetPosition.X = x;
            this.offsetPosition.Y = y;
        }
        public float GetOffsetPositionX()
        {
            return this.offsetPosition.X;
        }
        public float GetOffsetPositionY()
        {
            return this.offsetPosition.Y;
        }
        public Vector2 GetOffsetPosition()
        {
            return this.offsetPosition;
        }
        public void SetVelocityX(float value)
        {
            this.velocity.X = value;
        }
        public void SetVelocityY(float value)
        {
            this.velocity.Y = value;
        }
        public Hitbox GetHitbox()
        {
            return this.hitbox;
        }
        public Sprite GetTexture()
        {
            return this.sprite;
        }

        //CONSTRUCTOR
        protected Entity(Sprite sprite, bool realHitbox, float hitboxWidth, float hitboxHeight, int relativeX, int relativeY)
        {
            this.sprite = sprite;
            this.position = new Vector2(0, 0);
            this.offsetPosition = new Vector2(this.sprite.GetPositionX(), this.sprite.GetPositionY());
            
            this.velocity = new Vector2(0, 0);
            this.friction = 0.9f;
            this.currentState = State.MovingLeft;
            this.hitbox = (realHitbox) ? new Hitbox(this, hitboxWidth, hitboxHeight) 
                : new Hitbox(this, hitboxWidth, hitboxHeight, relativeX, relativeY);
        }

        //METHODS
        public void Intersect(Entity entity)
        {
            if(this.hitbox.Intersect(entity.GetHitbox(), this.offsetPosition, entity.offsetPosition))
                CancelUpdatePosition(this.hitbox.GetIntersectionDepth(entity.GetHitbox()));
            
        }
        public void UpdatePositionVelocite()
        {
            this.offsetPosition.X += this.velocity.X;
            this.offsetPosition.Y += this.velocity.Y;
        }

        public void CancelUpdatePosition(Vector2 intersectionDepth)
        {
            if (Math.Abs(intersectionDepth.X) < Math.Abs(intersectionDepth.Y))
                this.offsetPosition.X += intersectionDepth.X;
            else
                this.offsetPosition.Y += intersectionDepth.Y;
        }
        public void UpdatePositionCamera(CameraClass camera)
        {
            this.position.X = (this.offsetPosition.X - camera.GetPositionX());
            this.position.Y = (this.offsetPosition.Y - camera.GetPositionY());
        }
        public void ApplyFriction()
        {
            Vector2.Multiply(ref this.velocity, this.friction, out this.velocity); 
        }

        public virtual void UpdateSate()
        {
            switch (this.currentState)
            {
                case State.MovingLeft:
                    this.sprite.SetEffect(SpriteEffects.None);
                    break;
                case State.MovingRight:
                    this.sprite.SetEffect(SpriteEffects.FlipHorizontally);
                    break;
            }   
        }

        //UPDATE & DRAW
        public virtual void Update(GameTime gametime, CameraClass camera, Input input, float delta)
        {
            this.ApplyFriction();
            this.UpdatePositionVelocite();
            this.UpdateSate();
            this.UpdatePositionCamera(camera);
            this.sprite.Update(this.offsetPosition.X, this.offsetPosition.Y);
        }
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}