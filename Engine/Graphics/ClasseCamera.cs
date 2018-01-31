using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SharpCrawler
{
    public class CameraClass
    {
        //FIELDS
        private Vector2 position;
        private int speed;
        private int width;
        private int height;
        private Entity follow;

        //SETTER-GETTER
        public float GetPositionX() => this.position.X;
        public float GetPositionY() => this.position.Y;

        //CONSTRUCTOR
        public CameraClass(int width, int height, Entity elem)
        {
            this.position = new Vector2(elem.GetPositionX() - width/2, elem.GetPositionY() - height/2);
            this.speed = 8;
            this.width = width;
            this.height = height;
            this.follow = elem;
        }
        public void Refresh(Entity entity)
        {
            this.position = new Vector2(entity.GetPositionX() - width/2, entity.GetPositionY() - height/2);
            this.follow = entity;
        }

        //METHODS
        public Matrix GetMatrix()
        {
            this.position.X += (this.follow.GetOffsetPositionX() - this.position.X - (this.width/2))/speed;
            this.position.Y += (this.follow.GetOffsetPositionY() - this.position.Y - (this.height/2))/speed;
            return
                Matrix.CreateTranslation(new Vector3((float)-Math.Floor(this.position.X), (float)-Math.Floor(this.position.Y), 0.0f));
        }
    }
}