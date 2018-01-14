using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class WeaponDisplay
    {
        private Sprite weaponHolder;
        private Sprite weaponDisplayed;
        public WeaponDisplay(Sprite holderSkin)
        {
            this.weaponHolder = holderSkin;
        }
        public void Reset() => this.weaponDisplayed = null;
        public void SetDisplayed(Weapon weapon)
        {
            this.weaponDisplayed = new Sprite(weapon.GetSkin());
            this.weaponDisplayed.SetScale(2f);
            this.weaponDisplayed.SetRotation(MathHelper.PiOver4);
            this.weaponDisplayed.SetDepth(0.1f);
        }
        public void Update(float x, float y)
        {
              this.weaponHolder.Update(x, y);
              this.weaponDisplayed?.Update(x, y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.weaponHolder.Draw(spriteBatch);
            this.weaponDisplayed?.DrawFromSpriteSheet(spriteBatch);
        }
    }
}