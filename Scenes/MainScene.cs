using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class MainScene
    {
        private SharpCrawl game;
        private EntityPlayer player;
        private LivesDisplay livesUI;
        private WeaponDisplay weaponUI;
        private EndGame endGameUI;
        private bool won;
        private CameraClass gameCamera;
        public EntityPlayer GetPlayer() => this.player;
        public WeaponDisplay GetWeaponUI() => this.weaponUI;
        public LivesDisplay GetLivesUI() => this.livesUI;
        public void SetCamera(CameraClass camera) => this.gameCamera = camera;
        public CameraClass GetCamera() => this.gameCamera;
        public SharpCrawl GetGame() => this.game;
        public void Win() => this.won = true;
        public MainScene(SharpCrawl game)
        {
            this.game = game;
            this.player = EntityFactory.PlayerBuilder(Ressources.FlameGuy(), 100, 100, "John", 20, 3f);
            MapFactory.FirstMap(this.player);
            this.livesUI = new LivesDisplay((int)this.player.GetPositionX() + 200, (int)this.player.GetPositionY() + 100);
            this.weaponUI = new WeaponDisplay(new Sprite("UIWeapon", (int)this.player.GetPositionX() + 200, (int)this.player.GetPositionY() + 200, 0.2f));
            this.endGameUI = new EndGame(Settings.windowWidth/2, Settings.windowHeight/2, this);
        }
        public void WinScreen()
        {
            if(!this.won)
            {
                this.won = true;
                this.endGameUI = new EndGame(Settings.windowWidth/2, Settings.windowHeight/2, this, false);
                this.player.SetSpeed(0);
            }
        }

        public void Restart()
        {
            this.player = EntityFactory.PlayerBuilder(Ressources.FlameGuy(), 100, 100, "John", 20, 3f);
            MapFactory.Reset(this.player);
            this.gameCamera.Refresh(this.player);
            this.won = false;
            this.livesUI.Reset();
            this.weaponUI.Reset();
            this.endGameUI.Reset();
        }
        public void Update(GameTime gameTime, CameraClass camera, Input generalInput)
        {
            this.player.Update(gameTime, camera, generalInput, this.player.GetActualMap().GetMonsters());
            this.player.ActualMapUpdate(gameTime);
            this.livesUI.Update(camera.GetPositionX() + Settings.windowWidth - 50, camera.GetPositionY()  + Settings.windowHeight - 160);
            this.weaponUI.Update(camera.GetPositionX() + Settings.windowWidth - 50, camera.GetPositionY()  + Settings.windowHeight - 25);
            if(!this.player.IsAlive() || this.won)
                this.endGameUI.Update(camera.GetPositionX() + Settings.windowWidth*0.5f, camera.GetPositionY() + Settings.windowHeight*0.5f, generalInput);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.player.DrawMap(spriteBatch);
            this.player.Draw(spriteBatch);
            this.livesUI.Draw(spriteBatch);
            this.weaponUI.Draw(spriteBatch);
            if(!this.player.IsAlive() || this.won)
                this.endGameUI.Draw(spriteBatch);

        }
    }
}