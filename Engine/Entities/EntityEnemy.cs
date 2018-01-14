using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class EntityEnemy : Entity
    {
        public byte GetHealth()
        {
            return this.health;
        }
        //CONSTRUCTOR
        public EntityEnemy(Sprite sprite,  byte health, float knockback, bool realHitbox, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0, bool noHitbox = false)
                : base(sprite, realHitbox, hitboxWidth, hitboxHeight, relativeX, relativeY, noHitbox)
        {
            this.health = health;
            this.knockback = knockback;
        }
        
        public override void TakeDamages(byte damage, Vector2 knockback)
        {
            this.velocity = Vector2.Add(this.velocity, knockback);
            this.health -= damage;
        }
        public bool Disappeared()
        {
            return this.sprite.Disappeared();
        }
        public void AILogic(EntityPlayer player, float delta)
        {
            Vector2 playerPosition = player.GetOffsetPosition();
            Vector2 tempVelocity = Vector2.Subtract(playerPosition, this.offsetPosition);
            tempVelocity.Normalize();
            Vector2.Multiply(ref tempVelocity, delta*20, out tempVelocity);
            this.velocity += tempVelocity;
        }
        //UPDATE & DRAW
        public void Update(GameTime gameTime, EntityPlayer player)
        {
            if(this.IsAlive())
            {
                this.AILogic(player, (float)gameTime.ElapsedGameTime.TotalSeconds);
                base.Update(gameTime);
            }
            else
                this.DeathAnimation();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!this.Disappeared())
                this.sprite.DrawFromSpriteSheet(spriteBatch);
        }
    }
}