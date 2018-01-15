using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class SharpCrawl : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Input generalInput;
        private CameraClass gameCamera;
        private StartingScene mainMenu;
        private MainScene principalScene;
        private bool inGame;
        public static Random rng = new Random();
        
        public MainScene GetMainScene() => this.principalScene;
        public void SetGameState(bool boolean) => this.inGame = boolean;
        

        public SharpCrawl()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = Settings.fullscreen;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.inGame = false;
            this.generalInput = new Input(Keyboard.GetState(), Keyboard.GetState(), Mouse.GetState(), Mouse.GetState());
            this.mainMenu = new StartingScene(this);
            this.principalScene = new MainScene(this);
            this.gameCamera = new CameraClass(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, this.principalScene.GetPlayer());
            UIUtils.mainScene = this.principalScene;
            this.principalScene.SetCamera(this.gameCamera);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadSprites(Content);
            Ressources.LoadFonts(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.generalInput.SetCurrentStates(Keyboard.GetState(), Mouse.GetState());

            if(this.inGame)
                this.principalScene.Update(gameTime, this.gameCamera, this.generalInput);
            else
                this.mainMenu.Update(generalInput);
            
            this.generalInput.SetOldStates(this.generalInput.GetKeyboardCurrentState(), this.generalInput.GetMouseCurrentState());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if(this.inGame)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: this.gameCamera.GetMatrix());
                this.principalScene.Draw(this.spriteBatch);
            }
            else
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront);
                this.mainMenu.Draw(this.spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
