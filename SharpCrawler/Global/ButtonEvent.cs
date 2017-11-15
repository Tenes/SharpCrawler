using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public static class ButtonEvent
    {
        //METHODS
        public static void Quit(Button thisButton, SharpCrawl game)
        {
            game.Exit();
        }
    }
}
