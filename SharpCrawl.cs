using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class SharpCrawl : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Input generalInput;
        MainScene principalScene;
        

        public SharpCrawl()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.generalInput = new Input(Keyboard.GetState(), Keyboard.GetState(), Mouse.GetState(), Mouse.GetState());
            this.principalScene = new MainScene(); 
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

            this.principalScene.Update(gameTime, this.generalInput);

            this.generalInput.SetOldStates(Keyboard.GetState(), Mouse.GetState());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            this.principalScene.Draw(this.spriteBatch, this.Content);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
