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
        protected Vector2 velocity;
        protected float friction;

        protected Hitbox hitbox;
        protected string currentState;

        //SETTER-GETTER
        public float GetPositionX()
        {
            return this.position.X;
        }
        public float GetPositionY()
        {
            return this.position.Y;
        }
        // public void SetOffsetPosition(int x, int y)
        // {
        //     this.offsetPosition.X = x;
        //     this.offsetPosition.Y = y;
        // }
        // public float GetOffsetPositionX()
        // {
        //     return this.offsetPosition.X;
        // }
        // public float GetOffsetPositionY()
        // {
        //     return this.offsetPosition.Y;
        // }
        // public Vector2 GetOffsetPosition()
        // {
        //     return this.offsetPosition;
        // }
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
        protected Entity(Sprite sprite)
        {
            this.sprite = sprite;
            this.position = new Vector2(this.sprite.GetPositionX(), this.sprite.GetPositionY());
            //this.offsetPosition = new Vector2(this.sprite.GetPositionX(), this.sprite.GetPositionY());
            this.velocity = new Vector2(0, 0);
            this.friction = 0.9f;
            this.currentState = null;
            this.hitbox = new Hitbox(this);
        }
        protected Entity(Sprite sprite, float hitboxWidth, float hitboxHeight)
        {
            this.sprite = sprite;
            this.position = new Vector2(this.sprite.GetPositionX(), this.sprite.GetPositionY());
            //this.offsetPosition = new Vector2(this.sprite.GetPositionX(), this.sprite.GetPositionY());
            this.velocity = new Vector2(0, 0);
            this.friction = 0.9f;
            this.currentState = null;
            this.hitbox = new Hitbox(hitboxWidth, hitboxHeight);
            this.hitbox.SetHolder(this);
        }

        //METHODS
        public bool Intersect(Entity entity)
        {
            return this.hitbox.Intersect(entity.GetHitbox(), this.position, entity.position);
        }
        public void UpdatePositionVelocite()
        {
            this.position.X += this.velocity.X;
            this.position.Y += this.velocity.Y;
        }

        public void CancelUpdatePosition(Vector2 intersectionDepth)
        {
            if (Math.Abs(intersectionDepth.X) < Math.Abs(intersectionDepth.Y))
                this.position.X += intersectionDepth.X;
            else
                this.position.Y += intersectionDepth.Y;
        }

        // public void UpdatePositionCamera(CameraClass camera)
        // {
        //     this.position.X = (this.offsetPosition.X - camera.GetPositionX());
        //     this.position.Y = (this.offsetPosition.Y - camera.GetPositionY());
        // }

        public void ApplyFriction()
        {
            Vector2.Multiply(ref this.velocity, this.friction, out this.velocity); 
        }

        public virtual void UpdateSate(string imageKey)
        {
            switch (this.currentState)
            {
                case "Moving":
                    break;
                case "Attacking":
                    break;
                case "Idle":
                    break;
            }   
        }

        //UPDATE & DRAW
        public virtual void Update(GameTime gametime, Input input, float delta)
        {
            this.ApplyFriction();
            this.UpdatePositionVelocite();
            //this.UpdatePositionCamera(camera);
            this.sprite.Update(this.position.X, this.position.Y);
        }
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}