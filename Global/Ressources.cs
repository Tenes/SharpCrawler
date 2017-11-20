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
        private const byte tileSize = 16;

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
            return new Rectangle(2*tileSize, 13*tileSize, tileSize, tileSize);
        } 
    }
}
