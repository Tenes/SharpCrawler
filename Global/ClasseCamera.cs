// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace SharpCrawler
// {
//     public class CameraClass
//     {
//         //FIELDS
//         private Vector2 position;
//         private int speed;
//         private int width;
//         private int height;
//         private Entity follow;

//         //SETTER-GETTER
//         public float GetPositionX()
//         {
//             return this.position.X;
//         }
//         public float GetPositionY()
//         {
//             return this.position.Y;
//         }

//         //CONSTRUCTOR
//         public CameraClass(int width, int height, Entity elem)
//         {
//             this.position = new Vector2(elem.GetPositionX() - width/2, elem.GetPositionY() - height/2);
//             this.speed = 5;
//             this.width = width;
//             this.height = height;
//             this.follow = elem;
//         }

//         //METHODS
//         public Matrix Following()
//         {
//             this.position.X += (this.follow.GetOffsetPositionX() - this.GetPositionX() - (this.width/2))/speed;
//             this.position.Y += (this.follow.GetOffsetPositionY() - this.GetPositionY() - (this.height/2))/speed;
//             return
//                 Matrix.CreateTranslation(new Vector3(-this.position, 0.0f));
//         }
//     }
// }