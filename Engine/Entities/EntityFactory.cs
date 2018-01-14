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
        public static Obstacle WallBuilder(Rectangle sourceRectangle, int positionX, int positionY, float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 && height == 0)
                return new Obstacle(new Sprite("TileSet", positionX, positionY - scaledSize/2, 1, sourceRectangle, scale), false, scaledSize, scaledSize * 0.8f, relativePosition, (int)(relativePosition * 0.3f));
            else
                return new Obstacle(new Sprite("TileSet", positionX, positionY - scaledSize/2, 1, sourceRectangle, scale, width, height), false, width, height * 0.3f, (int)(-width/2), (int)(-height/2 * 0.15f));
        }
        public static EntityPlayer PlayerBuilder(Rectangle sourceRectangle, int positionX, int positionY, string name, float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 || height == 0)
                return new EntityPlayer(new Sprite("TileSet", positionX, positionY, 0.6f, sourceRectangle, scale), false, name, new Hand(new Sprite("TileSet", positionX + 10, positionY + 10, 0.4f, Ressources.Hand1(), scale)), 0.3f, scaledSize * 0.7f, scaledSize * 0.75f, (int)(relativePosition * 0.6f), (int)(relativePosition * 0.6f));
            else
                return new EntityPlayer(new Sprite("TileSet", positionX, positionY, 0.6f, sourceRectangle, scale,  width, height), false, name, new Hand(new Sprite("TileSet", positionX + 10, positionY + 10, 0.5f, Ressources.Hand1(), scale)), 0.3f, width, height, (int)(-width/2), (int)(-height/2));
        }
        public static EntityEnemy EnemyBuilder(Rectangle sourceRectangle, int positionX, int positionY, byte health, float knockback,float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 || height == 0)
                return new EntityEnemy(new Sprite("TileSet", positionX, positionY, 0.6f, sourceRectangle, scale), health, knockback,false, scaledSize * 0.7f, scaledSize * 0.75f, (int)(relativePosition * 0.6f), (int)(relativePosition * 0.6f));
            else
                return new EntityEnemy(new Sprite("TileSet", positionX, positionY, 0.6f, sourceRectangle, scale,  width, height), health, knockback, false, width, height, (int)(-width/2), (int)(-height/2));
        }
        public static Obstacle VoidOrAlternativeBuilder(Rectangle sourceRectangle, int positionX, int positionY, float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 && height == 0)
                return new Obstacle(new Sprite("TileSet", positionX, positionY, 1, sourceRectangle, scale), false, scaledSize, scaledSize, relativePosition, relativePosition);
            else
                return new Obstacle(new Sprite("TileSet", positionX, positionY, 1, sourceRectangle, scale, width, height), false, width, height, (int)(-width/2), (int)(-height/2));
        }
        public static Portal PortalBuilder(int positionX, int positionY, float scale = 1, int width = 0, int height = 0)
        {
            scaledSize = (int)(Settings.tileSize * scale);
            relativePosition = (int)(-Settings.tileSize/2 * scale);
            if(width == 0 && height == 0)
                return new Portal(new Sprite("TileSet", positionX, positionY, 1, Ressources.Portal(), scale), false, scaledSize, scaledSize, relativePosition, relativePosition);
            else
                return new Portal(new Sprite("TileSet", positionX, positionY, 1, Ressources.Portal(), scale, width, height), false, width, height, (int)(-width/2), (int)(-height/2));
        }
        public static Ground FloorBuilder(int positionX, int positionY, float scale = 1, int width = 0, int height = 0)
        {
            if(width == 0 && height == 0)
                return new Ground(new Sprite("TileSet", positionX, positionY, 1, Ressources.Floor(), scale), false);
            else
                return new Ground(new Sprite("TileSet", positionX, positionY, 1, Ressources.Floor(), scale, width, height), false);
        }
    }
}