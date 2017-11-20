using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public class GroundAndObstacle : Entity
    {
        //FIELDS
        private bool staticObject;

        //CONSTRUCTOR
        public GroundAndObstacle(Sprite objectTexture, bool staticObject = true)
            : base(objectTexture)
        {
            this.sprite = objectTexture;
            this.staticObject = staticObject;
        }

        public GroundAndObstacle(Sprite objectTexture, float hitboxWidth, float hitboxHeight, bool staticObject = true)
                : base(objectTexture, hitboxWidth, hitboxHeight)
        {
            this.sprite = objectTexture;
            this.staticObject = staticObject;
        }

        //METHODS


        //UPDATE & DRAW
        public void Update(float delta)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }
    }
}
