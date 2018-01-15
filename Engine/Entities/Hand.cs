using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class Hand
    {
        private Sprite skin;
        private Weapon actualWeapon;
        private Entity holder;
        private byte attackTimer;
        private static Dictionary<short, Vector3> handPositions = new Dictionary<short, Vector3>{
            {0, new Vector3(0, 0, MathHelper.Pi + MathHelper.PiOver4)},
            {1, new Vector3(3, -12, MathHelper.Pi + MathHelper.PiOver2 + MathHelper.PiOver4)},
            {2, new Vector3(8, -20, 0)},
            {3, new Vector3(15, -14, MathHelper.PiOver4)},
            {4, new Vector3(25, -6, MathHelper.PiOver2 + MathHelper.PiOver4)}
        };
        private static Dictionary<short, Vector2> weaponPositions = new Dictionary<short, Vector2>{
            {0, new Vector2(9, 12)},
            {1, new Vector2(6, -11)},
            {2, new Vector2(0, -18)},
            {3, new Vector2(-8, -9)},
            {4, new Vector2(-17, 8)}
        };
        private byte actualFrame;
        public Weapon GetWeapon() => this.actualWeapon;
        public void SetHolder(Entity holder) => this.holder = holder;
        public void UpdateFrame() => this.actualFrame = (byte)((this.actualFrame + 1) % 5);
        public void Equip(Weapon weapon) => this.actualWeapon = weapon;
        public void Disappear() => this.skin.Disappear();
        //CONSTRUCTOR
        public Hand(Sprite skin)
        {
            this.skin = skin;
            this.actualWeapon = null;
            this.attackTimer = 0;
        }
        public void DropWeapon(Map currentMap)
        {
            if(this.HasWeapon())
            {
                currentMap.SetWeapon(this.actualWeapon);
                this.actualWeapon = null;
            }
        }
        public bool HasWeapon()
        {
            if(this.actualWeapon != null)
                return true;
            return false;
        }
        public void Attack(GameTime gameTime, int handX, int handY, List<EntityEnemy> monsters)
        {
            int tempX;
            int tempY = handY + (int)handPositions[this.actualFrame].Y;
            this.attackTimer += 1;
            if(this.holder.GetAttackDirection() == Entity.AttackDirection.AttackingRight)
            {
                tempX = handX + (int)handPositions[this.actualFrame].X;
                this.skin.SetPosition(tempX, tempY);
                this.actualWeapon?.Update(tempX-(int)weaponPositions[this.actualFrame].X, tempY+(int)weaponPositions[this.actualFrame].Y);
                this.actualWeapon?.SetSkinRotation(handPositions[this.actualFrame].Z);
            }
            else
            { 
                tempX = handX - (int)handPositions[this.actualFrame].X;
                this.skin.SetPosition(tempX, tempY);
                this.actualWeapon?.Update(tempX+(int)weaponPositions[this.actualFrame].X, tempY+(int)weaponPositions[this.actualFrame].Y);
                this.actualWeapon?.SetSkinRotation(-handPositions[this.actualFrame].Z);
            }
            if(this.attackTimer%2 == 0)
                this.UpdateFrame();
            if(this.attackTimer >= 10)
            {
                this.attackTimer = 0;
                this.actualFrame = 0;
                this.holder.SetAttacking(false);
            }
            if(this.actualWeapon != null)
                if(this.attackTimer > 4 && monsters.Any())
                    for(int i = 0; i < monsters.Count; i++)
                        if((monsters[i].GetOffsetPosition() - this.skin.GetPositionVector()).Length() - 10 <= Settings.tileSize * this.actualWeapon.GetScale())
                            monsters[i].TakeDamages(this.actualWeapon.GetDamage(), Vector2.Multiply(monsters[i].GetOffsetPosition() - this.holder.GetOffsetPosition(), this.actualWeapon.GetKnockback()));
        }
        public void Update(int x, int y)
        {
            if(this.holder.IsAlive())
            {
                this.skin.SetPosition(x, y);
                if(this.holder.GetState() == Entity.State.MovingRight)
                {
                    this.skin.SetDepth(0.4f);
                    this.actualWeapon?.SetSkinRotation(MathHelper.Pi + MathHelper.PiOver4);
                    this.actualWeapon?.Update(x-this.actualWeapon.GetRelativeX(), y + this.actualWeapon.GetRelativeY());
                    this.actualWeapon?.SetSkinDepth(0.5f);
                }
                else
                {
                    this.skin.SetDepth(0.8f);
                    this.actualWeapon?.SetSkinRotation(MathHelper.PiOver2 + MathHelper.PiOver4);
                    this.actualWeapon?.Update(x+this.actualWeapon.GetRelativeX(), y + this.actualWeapon.GetRelativeY());
                    this.actualWeapon?.SetSkinDepth(0.7f);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(!this.skin.Disappeared())
            {
                this.skin.DrawFromSpriteSheet(spriteBatch);
                this.actualWeapon?.Draw(spriteBatch);
            }
        }
    }
}