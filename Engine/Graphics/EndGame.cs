using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class EndGame
    {
        private SpriteFont font;
        private string titleText,
               mainText;
        private Vector2 titleTextPosition,
                mainTextPosition;
        private Vector2 titleTextOrigin,
                mainTextOrigin;
        private Button RetryButton,
               ReturnButton;

        private static List<string> randomPhrases = new List<string>()
        {
            "It happens to the best of us sometimes...",
            "I hope you'll get good soon enough...",
            "Did you do it on purpose?",
            "Keep going buddy, persverance is key!",
            "Second time's a charm right?"
        };
        
        public EndGame(float screenMiddleX, float screenMiddleY, MainScene scene)
        {
            this.font = Ressources.fonts["MainFont"];
            this.titleText = "You died";
            this.mainText = randomPhrases[SharpCrawl.rng.Next(randomPhrases.Count)];
            this.titleTextPosition = new Vector2(screenMiddleX, screenMiddleY- 50);
            this.mainTextPosition = new Vector2(screenMiddleX, screenMiddleY + 10);
            this.titleTextOrigin = this.font.MeasureString(this.titleText) * 0.5f;
            this.mainTextOrigin = this.font.MeasureString(this.mainText) * 0.5f;
            this.RetryButton = new Button("Retry", screenMiddleX - 100, screenMiddleY + 80, (button) => 
            {
               ButtonEvent.Retry(button, scene);
               return true;
            });
            this.ReturnButton = new Button("Save\nand\nExit", screenMiddleX + 100, screenMiddleY + 80, (button) => 
            {
               return true;
            });
        }
        public void Update(float x, float y, Input mouseState)
        {
            this.titleTextPosition.X = x;
            this.titleTextPosition.Y = y - 50;
            this.mainTextPosition.X = x;
            this.mainTextPosition.Y = y + 10;
            this.RetryButton.Update(mouseState, x - 100, y + 80);
            this.ReturnButton.Update(mouseState, x + 100, y + 80);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.font, this.titleText, this.titleTextPosition, Color.IndianRed, 0, this.titleTextOrigin, 4f, SpriteEffects.None, 0.1f);
            spriteBatch.DrawString(this.font, this.mainText, this.mainTextPosition, Color.Aquamarine, 0,  this.mainTextOrigin, 2f, SpriteEffects.None, 0.1f);
            this.RetryButton.Draw(spriteBatch);
            this.ReturnButton.Draw(spriteBatch);
        }
    }
}