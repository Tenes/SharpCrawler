using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public static class EntityFactory
    {
        private static int scaledSize;
        private static int relativePosition;
        public static GroundAndObstacle WallBuilder(Rectangle sourceRectangle, int positionX, int positionY, float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 && height == 0)
                return new GroundAndObstacle(new Sprite("TileSet", positionX, positionY, sourceRectangle, scale), false, scaledSize, scaledSize * 0.8f, (int)(relativePosition * scale), (int)(relativePosition * 0.3f));
            else
                return new GroundAndObstacle(new Sprite("TileSet", positionX, positionY, sourceRectangle, scale, width, height), false, width, height * 0.3f, (int)(-width/2), (int)(-height/2 * 0.15f));
        }
        public static EntityPlayer PlayerBuilder(Rectangle sourceRectangle, int positionX, int positionY, string name, float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 || height == 0)
                return new EntityPlayer(new Sprite("TileSet", positionX, positionY, sourceRectangle, scale), false, name, scaledSize * 0.7f, scaledSize * 0.75f, (int)(relativePosition * 0.6f), (int)(relativePosition * 0.6f));
            else
                return new EntityPlayer(new Sprite("TileSet", positionX, positionY, sourceRectangle, scale,  width, height), false, name, width, height, (int)(-width/2), (int)(-height/2));
        }

        //public static 
    }
}