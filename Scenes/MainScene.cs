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
        public EntityPlayer GetPlayer()
        {
            return this.player;
        }
        public MainScene()
        {
            this.player = EntityFactory.PlayerBuilder(Ressources.FlameGuy(), 100, 100, "John", 3f);
            MapFactory.FirstMap(this.player);
        }
        public void Update(GameTime gameTime, CameraClass camera, Input generalInput)
        {
            this.player.Update(gameTime, camera, generalInput, (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.player.ActualMapUpdate();
        }
        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            this.player.DrawMap(spriteBatch);
            this.player.Draw(spriteBatch);
        }
    }
}