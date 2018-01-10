using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            {4, new Vector3(20, -6, MathHelper.PiOver2 + MathHelper.PiOver4)}
        };
        private static Dictionary<short, Vector2> armPositions = new Dictionary<short, Vector2>{
            {0, new Vector2(9, 12)},
            {1, new Vector2(6, -11)},
            {2, new Vector2(0, -18)},
            {3, new Vector2(-8, -9)},
            {4, new Vector2(-13, 8)}
        };
        private byte actualFrame;
        public Weapon GetWeapon()
        {
            return this.actualWeapon;
        }
        public void SetHolder(Entity holder)
        {
            this.holder = holder;
        }
        public void UpdateFrame()
        {
            this.actualFrame = (byte)((this.actualFrame + 1)%5);
        }
        public Hand(Sprite skin)
        {
            this.skin = skin;
            this.actualWeapon = null;
            this.attackTimer = 0;
        }
        public void Attack(GameTime gameTime, int handX, int handY)
        {
            int tempX;
            int tempY = handY + (int)handPositions[this.actualFrame].Y;
            this.attackTimer += 1;
            if(this.holder.GetAttackDirection() == Entity.AttackDirection.AttackingRight)
            {
                tempX = handX + (int)handPositions[this.actualFrame].X;
                this.skin.SetPosition(tempX, tempY);
                this.actualWeapon?.Update(tempX-(int)armPositions[this.actualFrame].X, tempY+(int)armPositions[this.actualFrame].Y);
                this.actualWeapon?.SetSkinRotation(handPositions[this.actualFrame].Z);
            }
            else
            { 
                tempX = handX - (int)handPositions[this.actualFrame].X;
                this.skin.SetPosition(tempX, tempY);
                this.actualWeapon?.Update(tempX+(int)armPositions[this.actualFrame].X, tempY+(int)armPositions[this.actualFrame].Y);
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
        }

        public void Unequip()
        {
            this.actualWeapon = null;
        }
        public void Equip(Weapon weapon)
        {
            this.actualWeapon = weapon;
        }
        public void Update(int x, int y)
        {
            this.skin.SetPosition(x, y);
            if(this.holder.GetState() == Entity.State.MovingRight)
            {
                this.skin.SetDepth(0.4f);
                this.actualWeapon?.SetSkinRotation(MathHelper.Pi + MathHelper.PiOver4);
                this.actualWeapon?.Update(x-this.actualWeapon.GetRelativeX(), y+this.actualWeapon.GetRelativeY());
                this.actualWeapon?.SetSkinDepth(0.5f);
            }
            else
            {
                this.skin.SetDepth(0.8f);
                this.actualWeapon?.SetSkinRotation(MathHelper.PiOver2 + MathHelper.PiOver4);
                this.actualWeapon?.Update(x+this.actualWeapon.GetRelativeX(), y+this.actualWeapon.GetRelativeY());
                this.actualWeapon?.SetSkinDepth(0.7f);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.skin.DrawFromSpriteSheet(spriteBatch);
            this.actualWeapon?.Draw(spriteBatch);
        }
    }
}