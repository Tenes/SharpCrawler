using System;
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
        private MainScene principalScene;
        public static Random rng = new Random();
        
        

        public SharpCrawl()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //graphics.IsFullScreen = Settings.fullscreen;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.generalInput = new Input(Keyboard.GetState(), Keyboard.GetState(), Mouse.GetState(), Mouse.GetState());
            this.principalScene = new MainScene();
            this.gameCamera = new CameraClass(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, this.principalScene.GetPlayer());
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadSprites(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.generalInput.SetCurrentStates(Keyboard.GetState(), Mouse.GetState());

            this.principalScene.Update(gameTime, this.gameCamera, this.generalInput);
            this.generalInput.SetOldStates(this.generalInput.GetKeyboardCurrentState(), this.generalInput.GetMouseCurrentState());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: this.gameCamera.GetMatrix());
            this.principalScene.Draw(this.spriteBatch, this.Content);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
