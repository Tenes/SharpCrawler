using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public static class Ressources
    {
        public static Dictionary<string, Texture2D> sprites;
        public static Dictionary<string, SpriteFont> fonts;

        //METHODS
        public static void LoadSprites(ContentManager content)
        {
            sprites = new Dictionary<string, Texture2D>();

            List<string> spr = new List<string>()
            {
                "UILives",
                "UIWeapon",
                "TileSet",
                "PressedButton",
                "UnpressedButton"
            };

            foreach (string sprite in spr)
            {
                sprites.Add(sprite, content.Load<Texture2D>($"spr/{sprite}"));
            }
        }
        public static void LoadFonts(ContentManager content)
        {
            fonts = new Dictionary<string, SpriteFont>();

            List<string> fontName = new List<string>()
            {
                "MainFont"
            };

            foreach (string name in fontName)
            {
                fonts.Add(name, content.Load<SpriteFont>($"font/{name}"));
            }
        }
        public static Rectangle FlameGuy()
        {
            return new Rectangle(2*Settings.tileSize, 13*Settings.tileSize, Settings.tileSize, Settings.tileSize);
        }
        public static Rectangle IcyBoy()
        {
            return new Rectangle(3*Settings.tileSize, 12*Settings.tileSize, Settings.tileSize, Settings.tileSize);
        }
        public static Rectangle Wall1()
        {
            return new Rectangle(0*Settings.tileSize + 1, 0*Settings.tileSize, Settings.tileSize, 2*Settings.tileSize);
        } 
        public static Rectangle Wall2()
        {
            return new Rectangle(1*Settings.tileSize, 0*Settings.tileSize, Settings.tileSize, 2*Settings.tileSize);
        } 
        public static Rectangle Wall3()
        {
            return new Rectangle(2*Settings.tileSize, 0*Settings.tileSize, Settings.tileSize, 2*Settings.tileSize);
        } 
        public static Rectangle WallFluid1()
        {
            return new Rectangle(3*Settings.tileSize, 3*Settings.tileSize, Settings.tileSize, 2*Settings.tileSize);
        } 
        public static Rectangle Floor()
        {
            return new Rectangle(2*Settings.tileSize, 3*Settings.tileSize, Settings.tileSize, Settings.tileSize);
        } 
        public static Rectangle Portal()
        {
            return new Rectangle(4*Settings.tileSize, 7*Settings.tileSize, Settings.tileSize, Settings.tileSize);
        } 
        public static Rectangle AlternativeWall()
        {
            return new Rectangle(8*Settings.tileSize, 0*Settings.tileSize, Settings.tileSize, Settings.tileSize);
        } 
        public static Rectangle Void()
        {
            return new Rectangle(4*Settings.tileSize, 14*Settings.tileSize, Settings.tileSize, Settings.tileSize);
        }
        public static Rectangle Hand1()
        {
            return new Rectangle(7*Settings.tileSize, 8*Settings.tileSize + 5, Settings.tileSize/4, Settings.tileSize/4);
        }
        public static Rectangle Sword1()
        {
            return new Rectangle(9*Settings.tileSize, 0*Settings.tileSize + 10, Settings.tileSize, 2*Settings.tileSize - 10);
        }
        public static Rectangle AnimatedLive(int frame)
        {
            return new Rectangle(8*Settings.tileSize  + 16 * frame, 9*Settings.tileSize , Settings.tileSize, 2*Settings.tileSize - 9);
        }
        public static Rectangle ExtinguishedLive()
        {
            return new Rectangle(7*Settings.tileSize, 9*Settings.tileSize + 3, Settings.tileSize, Settings.tileSize + 4);
        }
    }
}
