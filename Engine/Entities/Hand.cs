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
        public Weapon GetWeapon()
        {
            return this.actualWeapon;
        }
        public void SetHolder(Entity holder)
        {
            this.holder = holder;
        }
        public Hand(Sprite skin)
        {
            this.skin = skin;
            this.actualWeapon = null;
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
                this.actualWeapon?.Update(x-7, y+12);
                this.actualWeapon?.SetSkinDepth(0.5f);
            }
            else
            {
                this.skin.SetDepth(0.8f);
                this.actualWeapon?.SetSkinRotation(MathHelper.PiOver2 + MathHelper.PiOver4);
                this.actualWeapon?.Update(x+9, y+12);
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