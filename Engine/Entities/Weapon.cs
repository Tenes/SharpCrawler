using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class Weapon
    {
        private Sprite skin;
        private Hand holder;
        private byte damage;
        private byte range;
        private bool minify;
        private Hitbox hitbox;
        private Entity.State direction;
        public void SetHolder(Hand hand)
        {
            this.holder = hand;
        }
        public void SetState(Entity.State direction)
        {
            this.direction = direction;
        }
        public Weapon(Sprite skin, byte damage, byte range)
        {
            this.skin = skin;
            this.damage = damage;
            this.range = range;
            this.hitbox = new Hitbox(10, 22);
        }
        public void SetSkinRotation(float piValue)
        {
            this.skin.SetRotation(piValue);
        }
        public void SetSkinDepth(float depth)
        {
            this.skin.SetDepth(depth);
        }
        public void AnimateOnGround()
        {
            float tempScale = this.skin.GetScale();
            float tempRotation = this.skin.GetRotation();
            if (tempScale >= Settings.scale-0.5)
                minify = true;
            else if(tempScale <= Settings.scale-1.5)
                minify = false;
            if(minify)
                this.skin.SetScale(tempScale - 0.05f);
            else
                this.skin.SetScale(tempScale + 0.05f);
            this.skin.SetRotation(tempRotation + 0.016f);
        }
        public void PickUp(EntityPlayer player)
        {
            if(this.hitbox.Intersect(player.GetHitbox(), this.skin.GetPositionVector(), player.GetOffsetPosition()))
            {
                this.skin.SetScale(Settings.scale - 1);
                this.SetSkinRotation(MathHelper.Pi + MathHelper.PiOver4);
                player.GetHand().Equip(this);
                player.GetActualMap().NullifyWeapon();
            }
        }
        public void UpdateOnGround(EntityPlayer entity)
        {
            AnimateOnGround();
            PickUp(entity);
        }
        public void Update(int x, int y)
        {
            this.skin.SetPosition(x, y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.skin.DrawFromSpriteSheet(spriteBatch);
        }
    }
}