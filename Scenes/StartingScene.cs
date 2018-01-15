using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class StartingScene
    {
        private Sprite backgroundSprite;
        private ClickableText ExploreButton,
                       StatsButton,
                       ExitButton;
        public StartingScene(SharpCrawl sharpCrawl)
        {
            this.backgroundSprite = new Sprite("Background", (int)(Settings.windowWidth * 0.5f), (int)(Settings.windowHeight - 50), 1f);
            this.ExploreButton = new ClickableText("Explore the dungeon", 150, Settings.windowHeight - 20, (text) => 
            {
                ButtonEvent.Launch(text, sharpCrawl);
                return true;
            });
            this.StatsButton = new ClickableText("Display stats", 350, Settings.windowHeight - 20, (text) => 
            {
               return true;
            });
            this.ExitButton = new ClickableText("Quit the game", 500, Settings.windowHeight - 20, (text) => 
            {
                ButtonEvent.Quit(text, sharpCrawl);
               return true;
            });
        }
        public void Update(Input generalInput)
        {
            this.ExploreButton.Update(generalInput);
            this.StatsButton.Update(generalInput);
            this.ExitButton.Update(generalInput);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.backgroundSprite.Draw(spriteBatch);
            this.ExploreButton.Draw(spriteBatch);
            this.StatsButton.Draw(spriteBatch);
            this.ExitButton.Draw(spriteBatch);
        }
    }
}