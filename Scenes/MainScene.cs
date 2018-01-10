using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class MainScene
    {
        private EntityPlayer player;
        private LivesDisplay livesUI;
        private WeaponDisplay weaponUI;
        public EntityPlayer GetPlayer()
        {
            return this.player;
        }
        public WeaponDisplay GetWeaponUI()
        {
            return this.weaponUI;
        }
        public LivesDisplay GetLivesUI()
        {
            return this.livesUI;
        }
        public MainScene()
        {
            this.player = EntityFactory.PlayerBuilder(Ressources.FlameGuy(), 100, 100, "John", 3f);
            MapFactory.FirstMap(this.player);
            this.livesUI = new LivesDisplay((int)this.player.GetPositionX() + 200, (int)this.player.GetPositionY() + 100);
            this.weaponUI = new WeaponDisplay(new Sprite("UIWeapon", (int)this.player.GetPositionX() + 200, (int)this.player.GetPositionY() + 200, 0.2f));
        }
        public void Update(GameTime gameTime, CameraClass camera, Input generalInput)
        {
            this.player.Update(gameTime, camera, generalInput, (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.player.ActualMapUpdate();
            this.livesUI.Update(camera.GetPositionX() + Settings.windowWidth - 50, camera.GetPositionY()  + Settings.windowHeight - 160);
            this.weaponUI.Update(camera.GetPositionX() + Settings.windowWidth - 50, camera.GetPositionY()  + Settings.windowHeight - 25);
        }
        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            this.player.DrawMap(spriteBatch);
            this.player.Draw(spriteBatch);
            this.livesUI.Draw(spriteBatch);
            this.weaponUI.Draw(spriteBatch);
        }
    }
}