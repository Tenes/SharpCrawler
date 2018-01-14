using Microsoft.Xna.Framework;

namespace SharpCrawler
{
    public static class HitboxExtension
    {
        public static Vector2 GetIntersectionDepth(this Hitbox hitboxA, Hitbox hitboxB)
        {
            float halfWidthA = hitboxA.hitboxWidth / 2.0f;
            float halfHeightA = hitboxA.hitboxHeight / 2.0f;
            float halfWidthB = hitboxB.hitboxWidth / 2.0f;
            float halfHeightB = hitboxB.hitboxHeight / 2.0f;

            hitboxA.SetCenter(hitboxA.GetPositionX() + halfWidthA, hitboxA.GetPositionY()+ halfHeightA);
            hitboxB.SetCenter(hitboxB.GetPositionX() + halfWidthB, hitboxB.GetPositionY() + halfHeightB);

            float distanceX = ((hitboxA.GetHolder().GetOffsetPositionX() + hitboxA.GetCenterX()) - (hitboxB.GetHolder().GetOffsetPositionX() + hitboxB.GetCenterX()));
            float distanceY = ((hitboxA.GetHolder().GetOffsetPositionY() + hitboxA.GetCenterY()) - (hitboxB.GetHolder().GetOffsetPositionY() + hitboxB.GetCenterY()));
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector2(depthX, depthY);
        }
    }
}
