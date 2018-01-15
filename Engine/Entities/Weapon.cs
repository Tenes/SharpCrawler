using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class Weapon
    {
        private Sprite skin;
        private byte relativeX;
        private byte relativeY;
        private Hand holder;
        private byte damage;
        private byte range;
        private float knockback;
        private bool minify;
        private Hitbox hitbox;
        private Entity.State direction;
        public byte GetDamage() => this.damage;
        public float GetKnockback() => this.knockback;
        public byte GetRange() => this.range;
        public float GetScale() => this.skin.GetScale();
        public byte GetRelativeX() => this.relativeX;
        public byte GetRelativeY() => this.relativeY;
        public Sprite GetSkin() => this.skin;
        public void SetHolder(Hand hand) => this.holder = hand;
        public void SetState(Entity.State direction) => this.direction = direction;
        public void SetSkinRotation(float piValue) => this.skin.SetRotation(piValue);
        public void SetSkinDepth(float depth) => this.skin.SetDepth(depth);
        public Weapon(Sprite skin, byte damage, byte range, float knockback)
        {
            this.relativeX = 9;
            this.relativeY = 12;
            this.skin = skin;
            this.damage = damage;
            this.range = range;
            this.hitbox = new Hitbox(10, 22);
            this.knockback = knockback;
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
                player.GetHand().Equip(this);
                player.GetActualMap().NullifyWeapon();
                UIUtils.LinkWeaponToUI(this);
            }
        }
        public void UpdateOnGround() => AnimateOnGround();
        public void UpdateOnGround(EntityPlayer entity)
        {
            AnimateOnGround();
            PickUp(entity);
        }
        public void Update(int x, int y) => this.skin.SetPosition(x, y);
        public void Draw(SpriteBatch spriteBatch) => this.skin.DrawFromSpriteSheet(spriteBatch);
    }
}