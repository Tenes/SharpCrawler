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
        private float Scale;
        private Rectangle Destination;
        public SpriteEffects Effects { get; set; }
        private Rectangle FrameSize;
        private int SpriteWidth;
        private int SpriteHeight;
        public float AnimationTime { get; set; }
        private float layerDepth;

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
        public void SetEffect(SpriteEffects effects)
        {
            this.Effects = effects;
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
        public float GetScale()
        {
            return this.Scale;
        }
        public void SetDepth(float depth)
        {
            this.layerDepth = (depth <= 1 && depth >= 0) ? depth : 1;
        }

        //CONSTRUCTOR
        public Sprite(string imageKey, int x, int y, float depth, Rectangle source = new Rectangle(), float scale = 1f, int destinationWidth = 0, int destinationHeight = 0)
        {
            this.Texture = Ressources.sprites[imageKey];
            this.SpriteWidth = (scale == 1f) ? (int)destinationWidth : (int)(source.Width * scale);
            this.SpriteHeight = (scale == 1f) ? (int)destinationHeight : (int)(source.Height * scale);
            this.Position = new Vector2(x, y);
            this.layerDepth = depth;
            this.FrameSize = source;
            this.Color = Color.White;
            this.Rotation = 0; //NO USE FOR NOW
            this.Effects = SpriteEffects.None; //NO USE FOR NOW
            this.AnimationTime = 200;
            this.Scale = scale;
            this.Destination = new Rectangle(x, y, destinationWidth, destinationHeight);
            this.Origin = new Vector2(source.Width / 2, source.Height / 2);
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
        public void Update(float x, float y, bool animated = false)
        {
            this.SetPosition(x, y);
            if (animated)
            {
                this.UpdateAnimationFrame();
                this.Origin = new Vector2(this.FrameSize.X + this.FrameSize.Width / 2, this.FrameSize.Y + this.FrameSize.Height / 2);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, null, this.Color, this.Rotation, this.Origin, 1f, this.Effects, this.layerDepth);
        }
        public void DrawFromSpriteSheet(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, this.FrameSize, this.Color, this.Rotation, this.Origin, this.Scale, this.Effects, this.layerDepth);
        }
        public void DrawFromSpriteSheetToDestination(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Destination, this.FrameSize, this.Color, this.Rotation, this.Origin, this.Effects, this.layerDepth);
        }
    }
}
