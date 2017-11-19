using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class Sprite
    {
        //FIELDS
        private Texture2D Texture;
        private Vector2 Position;
        private Color Color;
        private float Rotation;
        private Vector2 Origin;
        private Rectangle Hitbox;
        //private float Scale;
        public SpriteEffects Effects { get; set; }
        private Rectangle FrameSize;
        private int SpriteWidth;
        private int SpriteHeight;
        public float AnimationTime { get; set; }

        //SETTER-GETTER
        public Sprite SetColor(Color color)
        {
            this.Color = color;
            return this;
        }
        public Color GetColor()
        {
            return this.Color;
        }
        public Vector2 GetPositionVector()
        {
            return this.Position;
        }
        public float GetPositionX()
        {
            return this.Position.X;
        }
        public float GetPositionY()
        {
            return this.Position.Y;
        }
        public int GetSpriteWidth()
        {
            return this.SpriteWidth;
        }
        public int GetSpriteHeight()
        {
            return this.SpriteHeight;
        }

        //CONSTRUCTOR
        public Sprite(string imageKey, int x, int y)
        {
            this.Texture = Ressources.sprites[imageKey];
            this.SpriteWidth = this.Texture.Width;
            this.SpriteHeight = this.Texture.Height;
            this.Position = new Vector2(x, y);
            
            this.Color = Color.White;
            this.Rotation = 0; //NO USE FOR NOW
            this.Origin = new Vector2(this.SpriteWidth/2, this.SpriteHeight/2);
            this.Effects = SpriteEffects.None; //NO USE FOR NOW
            this.AnimationTime = 200;
            this.Hitbox = new Rectangle((int)(x - this.SpriteWidth / 2), (int)(y - this.SpriteHeight / 2),
                                        this.SpriteWidth, this.SpriteHeight);
        }
        public Sprite(string imageKey, int x, int y, int sizeX, int sizeY)
        {
            this.Texture = Ressources.sprites[imageKey];
            this.SpriteWidth = this.Texture.Width;
            this.SpriteHeight = this.Texture.Height;
            this.Position = new Vector2(x, y);
            this.FrameSize = new Rectangle(0, 0, sizeX, sizeY);
            this.Color = Color.White;
            this.Rotation = 0; //NO USE FOR NOW
            this.Origin = new Vector2(this.FrameSize.X + this.FrameSize.Width / 2, this.FrameSize.Y + this.FrameSize.Height / 2);
            this.Effects = SpriteEffects.None; //NO USE FOR NOW
            this.AnimationTime = 200;
        }
        //PROPERTIES
        public int Width { get { return this.SpriteWidth; } }
        public int Height { get { return this.SpriteHeight; } }

        //METHODS
        public void SetPosition(float x, float y)
        {
            this.Position.X = x;
            this.Position.Y = y;
        }
        public void UpdatePosition(float x, float y)
        {
            this.Position.X += x;
            this.Position.Y += y;
        }
        public void UpdateAnimationFrame()
        {
            if (this.AnimationTime > 0)
                this.AnimationTime -= 20;
            else if (this.AnimationTime <= 0)
            {
                this.FrameSize.X += 40 % 120;
                this.FrameSize.Y += 40 % 120;
                this.AnimationTime = 100;
            }
        }

        public void Animation(string imageKey)
        {
            this.Texture = Ressources.sprites[imageKey];
        }

        //UPDATE & DRAW
        public void Update(float x, float y)
        {
            this.UpdatePosition(x, y);
            if(this.FrameSize != null)
            {
                this.UpdateAnimationFrame();
                this.Origin = new Vector2(this.FrameSize.X + this.FrameSize.Width / 2, this.FrameSize.Y + this.FrameSize.Height / 2);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, null, this.Color, this.Rotation, this.Origin, 1f, this.Effects, 0f);
        }
        public void DrawFromSpriteSheet(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, this.FrameSize, this.Color, this.Rotation, this.Origin, 1f, this.Effects, 0f);
        }
    }
}
