using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class LivesDisplay
    {
        private Sprite[] livesDisplayed;
        private bool[] animated;
        private ushort[] counter;
        private List<ushort> timeFrame;

        public LivesDisplay(int x, int y)
        {
            this.livesDisplayed = new Sprite[3]
            {
                new Sprite("TileSet", x, y, 0.2f, Ressources.AnimatedLive(0), 3f),
                new Sprite("TileSet", x, y + 32, 0.15f, Ressources.AnimatedLive(0), 3f),
                new Sprite("TileSet", x, y + 64, 0.1f, Ressources.AnimatedLive(0), 3f)
            };
            this.animated = new bool[3]
            {
                true, true, true
            };
            this.counter = new ushort[3]
            {
                0, 18, 30
            };
            this.timeFrame = new List<ushort>()
            {
                6, 12, 18, 24, 30, 36, 42
            };
        }

        public void AffectCharacterHp(byte hp)
        {
            switch(hp)
            {
                case 3:
                    this.animated[0] = true;
                    this.animated[1] = true;
                    this.animated[2] = true;
                    break;
                case 2:
                    this.animated[0] = false;
                    this.animated[1] = true;
                    this.animated[2] = true;
                    break;
                case 1:
                    this.animated[0] = false;
                    this.animated[1] = false;
                    this.animated[2] = true;
                    break;
                case 0:
                default:
                    this.animated[0] = false;
                    this.animated[1] = false;
                    this.animated[2] = false;
                    break;
            }
        }
        
        public void Animate(Sprite live, int counter)
        {
            if(this.timeFrame.Contains(this.counter[counter]))
                live.SetSource(Ressources.AnimatedLive(this.timeFrame.IndexOf(this.counter[counter])));
            this.counter[counter]++;
            if(this.counter[counter] == this.timeFrame[this.timeFrame.Count - 1] + 1)
                this.counter[counter] = 1;
        }
        public void Update(float x, float y)
        {
            for(int i = 0; i < livesDisplayed.Length; i++)
            {
                if(this.animated[i])
                {
                    this.livesDisplayed[i]?.Update(x, y + this.livesDisplayed[i].Height/2 * i);
                    Animate(this.livesDisplayed[i], i);
                }
                else
                {
                    this.livesDisplayed[i]?.Update(x, y + 9 + this.livesDisplayed[i].Height/2 * i);
                    this.livesDisplayed[i]?.SetSource(Ressources.ExtinguishedLive());
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < livesDisplayed.Length; i++)
                this.livesDisplayed[i]?.DrawFromSpriteSheet(spriteBatch);
        }
    }
}