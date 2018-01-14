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
        private short invicibilityTime;
        private bool invicible;
        public void SetActualMap(Map map) => this.actualMap = map;
        public Map GetActualMap() => this.actualMap;
        public Hand GetHand() => this.entityHand;
        public byte GetHealth() => this.health;
        //CONSTRUCTOR
        public EntityPlayer(Sprite sprite,  bool realHitbox, string name, Hand hand, float knockback, float hitboxWidth = 0, float hitboxHeight = 0, int relativeX = 0, int relativeY = 0, bool noHitbox = false)
                : base(sprite, realHitbox, hitboxWidth, hitboxHeight, relativeX, relativeY, noHitbox)
        {
            this.name = name;
            this.entityHand = hand;
            this.entityHand.SetHolder(this);
            this.health = 3;
            this.invicibilityTime = 1560;
            this.knockback = knockback;
        }

        //METHODS
        public override void DeathAnimation()
        {
            this.sprite.Disappear();
            this.entityHand.Disappear();
        }
        public override void TakeDamages(byte damage, Vector2 knockback)
        {
            this.velocity = Vector2.Add(this.velocity, knockback);
            if(!this.invicible)
            {
                this.health -= damage;
                UIUtils.LinkHealthToUI(this.health);
                this.invicibilityTime = 0;
                this.invicible = true;
            }
        }
        public void DamageAnimation()
        {
            if(this.invicible)
            {
                this.sprite.BlinkAlpha();
                this.invicibilityTime += 26;
                if(this.invicibilityTime == 1560)
                {
                    this.invicible = !this.invicible;
                    this.sprite.SetColor(Color.White);
                }
            }
        }
        public void UpdateHand(Input input, GameTime gameTime)
        {
            if(input.IsMouseDown() && !this.attacking)
            {
                this.attacking = true;
                this.currentAttackDirection = (AttackDirection)this.currentState;
            }

            if(!this.attacking)
                this.entityHand.Update((this.currentState == State.MovingLeft) ?(int)this.offsetPosition.X + 8:(int)this.offsetPosition.X - 8,
                                    (int)this.offsetPosition.Y+ 16);
            else
                this.entityHand.Attack(gameTime, (this.currentAttackDirection == AttackDirection.AttackingLeft) ?(int)this.offsetPosition.X + 8:(int)this.offsetPosition.X - 8,
                                    (int)this.offsetPosition.Y+ 16);
        }
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
        public override void Update(GameTime gameTime, CameraClass camera, Input input)
        {
            if(this.IsAlive())
            {
                this.Mouvement(input, (float)gameTime.ElapsedGameTime.TotalSeconds);
                this.UpdateHand(input, gameTime);
                this.DamageAnimation();
                base.Update(gameTime, camera, input);
            }
            else
            {
                this.entityHand.DropWeapon(this.actualMap);
                this.DeathAnimation();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!this.sprite.Disappeared())
            {
                this.sprite.DrawFromSpriteSheet(spriteBatch);
                this.entityHand.Draw(spriteBatch);
            }
        }
    }
}
