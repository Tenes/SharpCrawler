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
    public static class Ressources
    {
        public static Dictionary<string, Texture2D> sprites;

        //METHODS
        public static void LoadSprites(ContentManager content)
        {
            sprites = new Dictionary<string, Texture2D>();

            List<string> spr = new List<string>()
            {
                "TextInput",
                "TileSet",
                "Gear"
            };

            foreach (string sprite in spr)
            {
                sprites.Add(sprite, content.Load<Texture2D>($"spr/{sprite}"));
            }
        }
        public static Rectangle CharacterN1()
        {
            return new Rectangle(2*Settings.tileSize, 13*Settings.tileSize, Settings.tileSize, Settings.tileSize);
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
    }
}
