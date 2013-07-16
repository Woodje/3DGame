using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WallE
{
    public class Collision
    {
        List<BoundingBox> boundingBoxList;

        public Collision(List<BoundingBox> boundingBoxList)
        {
            this.boundingBoxList = boundingBoxList;
        }

        public bool UpdateCollision(CModel model)
        {
            bool collisionDetected = false;

            for (int i = 0; i < boundingBoxList.Count; i++)
            {
                if (model.BoundingSphere.Intersects(boundingBoxList[i]))
                {
                    collisionDetected = true;
                    break;
                }
                else
                    collisionDetected = false;
            }

            return collisionDetected;
        }
    }
}
