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
    public class Ressources
    {
        public static Dictionary<string, Texture2D> sprites;

        //METHODS
        public static void LoadSprites(ContentManager content)
        {
            sprites = new Dictionary<string, Texture2D>();

            List<string> spr = new List<string>()
            {

            };

            foreach (string sprite in spr)
            {
                sprites.Add(sprite, content.Load<Texture2D>($"spr/{sprite}"));
            }

        }

    }
}
