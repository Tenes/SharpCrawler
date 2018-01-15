using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public enum State{MovingLeft, MovingRight}
        public enum AttackDirection{AttackingLeft, AttackingRight}
        protected State currentState;
        protected AttackDirection currentAttackDirection;
        protected bool attacking;
        protected byte health;
        protected float knockback;
        protected int speed;

        //SETTER-GETTER
        public float GetPositionX() => this.position.X;
        public float GetPositionY() => this.position.Y;
        public void SetOffsetPosition(int x, int y)
        {
            this.offsetPosition.X = x;
            this.offsetPosition.Y = y;
        }
        public float GetOffsetPositionX() => this.offsetPosition.X;
        public float GetOffsetPositionY() => this.offsetPosition.Y;
        public Vector2 GetOffsetPosition() => this.offsetPosition;
        public void SetVelocityX(float value) => this.velocity.X = value;
        public void SetVelocityY(float value) => this.velocity.Y = value;
        public Hitbox GetHitbox() => this.hitbox;
        public Sprite GetTexture() => this.sprite;
        public State GetState() => this.currentState;
        public AttackDirection GetAttackDirection() => this.currentAttackDirection;
        public void SetAttacking(bool attacking) => this.attacking = attacking;
        public virtual void TakeDamages(byte damage, Vector2 knockback) => this.health -= damage;
        public float GetKnockBack() => this.knockback;
        public void SetKnockBack(float newKnockback) => this.knockback = newKnockback;
        public virtual void DeathAnimation() => this.sprite.Disappear();
        //CONSTRUCTOR
        protected Entity(Sprite sprite, bool realHitbox, float hitboxWidth, float hitboxHeight, int relativeX, int relativeY, bool noHitbox)
        {
            this.sprite = sprite;
            this.position = new Vector2(0, 0);
            this.offsetPosition = new Vector2(this.sprite.GetPositionX(), this.sprite.GetPositionY());
            
            this.velocity = new Vector2(0, 0);
            this.friction = 0.9f;
            this.currentState = State.MovingLeft;
            if(noHitbox)
                this.hitbox = null;
            else
                this.hitbox = (realHitbox) ? new Hitbox(this, hitboxWidth, hitboxHeight) 
                    : new Hitbox(this, hitboxWidth, hitboxHeight, relativeX, relativeY);
            this.currentAttackDirection = AttackDirection.AttackingRight;
        }

        //METHODS
        public bool IsAlive()
        {
            if(this.health > 0)
                return true;
            return false;
        }
        public bool HasHitbox()
        {
            if(this.hitbox != null)
                return true;
            else
                return false;
        }
        public bool Intersect(Entity entity)
        {
            if(this.IsAlive())
                return this.hitbox.Intersect(entity.GetHitbox(), this.offsetPosition, entity.offsetPosition);
            return false;
        }
        public void UpdatePositionVelocite()
        {
            this.offsetPosition.X += this.velocity.X;
            this.offsetPosition.Y += this.velocity.Y;
        }

        public void CancelUpdatePosition(Entity entity)
        {
            Vector2 intersectionDepth = this.hitbox.GetIntersectionDepth(entity.GetHitbox());
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
        public void ApplyFriction() => Vector2.Multiply(ref this.velocity, this.friction, out this.velocity);

        public void CollisionCheck(List<Obstacle> entities)
        {
            for(int i = 0; i < entities.Count; i++)
                if(this.Intersect(entities[i]))
                    this.CancelUpdatePosition(entities[i]);
        }
        public void CollisionCheck(List<Portal> entities)
        {
            for(int i = 0; i < entities.Count; i++)
                if(this.Intersect(entities[i]))
                    this.CancelUpdatePosition(entities[i]);
        }
        public void CollisionCheck(Entity otherEntity)
        {
            if(otherEntity == this)
                return;
            if(this.Intersect(otherEntity))
                this.CancelUpdatePosition(otherEntity);
        }
        public void Collide(Entity otherEntity)
        {
            if(this.Intersect(otherEntity))
            {
                this.TakeDamages(1, Vector2.Multiply(this.GetOffsetPosition() - otherEntity.GetOffsetPosition(), otherEntity.GetKnockBack()));
                otherEntity.TakeDamages(1, Vector2.Multiply(otherEntity.GetOffsetPosition() - this.GetOffsetPosition(), this.GetKnockBack()));
            }
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
        public virtual void Update(GameTime gametime, CameraClass camera, Input input)
        {
            this.ApplyFriction();
            this.UpdatePositionVelocite();
            this.UpdateSate();
            this.UpdatePositionCamera(camera);
            this.sprite.Update(this.offsetPosition.X, this.offsetPosition.Y);
        }
        public virtual void Update(GameTime gametime)
        {
            this.ApplyFriction();
            this.UpdatePositionVelocite();
            this.UpdateSate();
            this.sprite.Update(this.offsetPosition.X, this.offsetPosition.Y);
        }
        public virtual void Draw(SpriteBatch spriteBatch) => this.sprite.DrawFromSpriteSheet(spriteBatch);
    }
}