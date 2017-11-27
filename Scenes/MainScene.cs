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
            MapFactory.FirstMap(this.Player);
        }
        public void Update(GameTime gameTime, CameraClass camera, Input generalInput)
        {
            this.Player.Update(gameTime, camera, generalInput, (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.Player.MapCollisionCheck();
        }
        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            this.Player.DrawMap(spriteBatch);
            this.Player.Draw(spriteBatch);
        }
    }
}