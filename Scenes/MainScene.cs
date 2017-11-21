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
        private EntityPlayer Player;
        public EntityPlayer GetPlayer()
        {
            return this.Player;
        }
        public MainScene()
        {
            this.Player = EntityFactory.PlayerBuilder(Ressources.CharacterN1(), 100, 100, "John", 3f);
        }
        public void Update(GameTime gameTime, CameraClass camera, Input generalInput)
        {
            this.Player.Update(gameTime, camera, generalInput, (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            spriteBatch.DrawString(Content.Load<SpriteFont>("font/MainFont"), "Hello", new Vector2(100,100), Color.Black);
            this.Player.Draw(spriteBatch);
        }
    }
}