using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class Hitbox
    {
        //FIELDS
        private Vector2 position;
        public float hitboxWidth { get; set; }
        public float hitboxHeight { get; set; }
        private Vector2 center;
        private Entity holder;

        //PROPERTIES
        public float GetPositionX()
        {
            return this.position.X;
        }
        public float GetPositionY()
        {
            return this.position.Y;
        }
        public void SetCenter(float x, float y)
        {
            this.center.X = x;
            this.center.Y = y;
        }
        public float GetCenterX()
        {
            return this.center.X;
        }
        public float GetCenterY()
        {
            return this.center.Y;
        }
        public Entity GetHolder()
        {
            return this.holder;
        }
        public void SetHolder(Entity entity)
        {
            this.holder = entity;
        }

        //CONSTRUCTOR
        public Hitbox(Entity entity, float hitboxWidth = 0, float hitboxHeight = 0, float relativeX = 0, float relativeY = 0)
        {
            float scale = entity.GetTexture().GetScale();
            this.position = (relativeX == 0 && relativeY == 0) ? new Vector2(-Settings.tileSize * scale / 2, -Settings.tileSize * scale / 2) 
                        : new Vector2(relativeX, relativeY);
            this.hitboxWidth = (hitboxWidth == 0) ? Settings.tileSize * scale : hitboxWidth;
            this.hitboxHeight = (hitboxHeight == 0) ? Settings.tileSize * scale : hitboxHeight;
            this.center = new Vector2(0, 0);
            this.holder = entity;
        }

        //METHODS
        public bool Intersect(Hitbox entityHitbox, Vector2 subjectPosition, Vector2 otherPosition)
        {
            return !(subjectPosition.X + this.position.X > otherPosition.X + entityHitbox.position.X + entityHitbox.hitboxWidth
                  || subjectPosition.Y + this.position.Y > otherPosition.Y + entityHitbox.position.Y + entityHitbox.hitboxHeight
                  || subjectPosition.X + this.position.X + this.hitboxWidth < otherPosition.X + entityHitbox.position.X
                  || subjectPosition.Y + this.position.Y + this.hitboxHeight < otherPosition.Y + entityHitbox.position.Y);
        }

        //UPDATE & DRAW
        public void DrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.sprites["TextInput"], new Rectangle((int)(this.holder.GetOffsetPositionX() + this.position.X), (int)(this.holder.GetOffsetPositionY() + this.position.Y), (int)hitboxWidth, (int)hitboxHeight), null, Color.Red, 0, new Vector2(0,0),SpriteEffects.None, 0.6f);
        }
    }
}