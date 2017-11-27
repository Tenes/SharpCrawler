using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SharpCrawler
{
    public enum Obstacle{Floor1, Floor2, Wall1, Wall2, Wall3, AlternativeWall, Void, Portal}
    public class Map
    {
        private static byte currentLeftLuck,
                            currentRightLuck,
                            currentTopLuck,
                            currentBottomLuck,
                            currentStep;
        private static Random mapRng = new Random();
        private Obstacle[,] centerMap;
        private GroundAndObstacle[] Environment;
        private Map LeftMap,
                    RightMap,
                    TopMap,
                    BottomMap;
        public Map(byte leftLuck, byte rightLuck, byte topLuck, byte bottomLuck, byte step)
        {
            currentLeftLuck = leftLuck;
            currentRightLuck = rightLuck;
            currentTopLuck = topLuck;
            currentBottomLuck = bottomLuck;
            currentStep = step;
            this.initMap();
        }
        public Map()
        {

            this.initMap();
        }
        public void initMap()
        {
            Obstacle w = Obstacle.Wall1;
            Obstacle v = Obstacle.Void;
            Obstacle wa = Obstacle.AlternativeWall;
            Obstacle f = Obstacle.Floor1;
            this.centerMap = new Obstacle[16,16]
            {
                {v, w, w, w, w, w, w, w, w, w, w, w, w, w, w, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, f, f, f, f, f, f, f, f, f, f, f, f, f, f, v},
                {v, wa, wa, wa, wa, wa, wa, wa, wa, wa, wa, wa, wa, wa, wa, v}
            };
            GeneratePortals();
            this.Environment = new GroundAndObstacle[this.centerMap.Length];
            int scaledPosition = (int)(Settings.tileSize * Settings.scale);
            for(int row = 0; row < this.centerMap.GetLength(0); row++)
            {
                for(int column = 0; column < this.centerMap.GetLength(1); column++)
                {
                    GroundAndObstacle tempEnv;
                    switch(this.centerMap[row, column])
                    {
                        case Obstacle.Wall1:
                            tempEnv = EntityFactory.WallBuilder(Ressources.Wall1(), column*scaledPosition, row*scaledPosition, Settings.scale); 
                            break;
                        case Obstacle.AlternativeWall:
                            tempEnv = EntityFactory.VoidOrAlternativeBuilder(Ressources.AlternativeWall(), column*scaledPosition, row*scaledPosition, Settings.scale);
                            break;
                        case Obstacle.Void:
                            tempEnv = EntityFactory.VoidOrAlternativeBuilder(Ressources.Void(), column*scaledPosition, row*scaledPosition, Settings.scale);
                            break;
                        case Obstacle.Portal:
                            tempEnv = EntityFactory.PortalBuilder(column*scaledPosition, row*scaledPosition, Settings.scale);
                            break;
                        case Obstacle.Floor1:
                        default:
                            tempEnv = EntityFactory.FloorBuilder(column*scaledPosition, row*scaledPosition, Settings.scale);                        
                            break;
                    }
                    this.Environment[this.centerMap.GetLength(0)*row + column] = tempEnv; 
                }
            }
        }
        public void GenerateLefttMap()
        {
            Map tempMap = new Map();
            this.LeftMap = tempMap;
            tempMap.RightMap = this;
            MapFactory.allMaps.Append(tempMap);
        }
        public void GenerateRightMap()
        {
            Map tempMap = new Map();
            this.RightMap = tempMap;
            tempMap.LeftMap = this;
            MapFactory.allMaps.Append(tempMap);
        }
        public void GenerateTopMap()
        {
            Map tempMap = new Map();
            this.TopMap = tempMap;
            tempMap.BottomMap = this;
            MapFactory.allMaps.Append(tempMap);
        }
        public void GenerateBottomtMap()
        {
            Map tempMap = new Map();
            this.BottomMap = tempMap;
            tempMap.TopMap = this;
            MapFactory.allMaps.Append(tempMap);
        }
        private void GeneratePortals()
        {
            GenerateLeftPortals();
            GenerateRightPortals();
            GenerateBottomPortals();
            GenerateTopPortals();
        }
        private void GenerateLeftPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(1, 13);
            if(mapRng.Next(101) <= currentLeftLuck)
            {
                this.centerMap[portalsPosition, 0] = Obstacle.Wall1;
                this.centerMap[portalsPosition + 1, 0] = Obstacle.Portal;
                this.centerMap[portalsPosition + 2, 0] = Obstacle.Portal;
                this.centerMap[portalsPosition + 3, 0] = Obstacle.AlternativeWall;
            }
        }
        private void GenerateRightPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(1, 13);
            if(mapRng.Next(101) <= currentRightLuck)
            {
                this.centerMap[portalsPosition, 15] = Obstacle.Wall1;
                this.centerMap[portalsPosition + 1, 15] = Obstacle.Portal;
                this.centerMap[portalsPosition + 2, 15] = Obstacle.Portal;
                this.centerMap[portalsPosition + 3, 15] = Obstacle.AlternativeWall;
            }
        }
        private void GenerateBottomPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(1, 13);
            if(mapRng.Next(101) <= currentBottomLuck)
            {
                this.centerMap[15, portalsPosition] = Obstacle.AlternativeWall;
                this.centerMap[15, portalsPosition + 1] = Obstacle.Portal;
                this.centerMap[15, portalsPosition + 2] = Obstacle.Portal;
                this.centerMap[15, portalsPosition + 3] = Obstacle.AlternativeWall;
            }
        }
        private void GenerateTopPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(1, 13);
            if(mapRng.Next(101) <= currentLeftLuck)
            {
                this.centerMap[0, portalsPosition] = Obstacle.Wall1;
                this.centerMap[0, portalsPosition + 1] = Obstacle.Portal;
                this.centerMap[0, portalsPosition + 2] = Obstacle.Portal;
                this.centerMap[0, portalsPosition + 3] = Obstacle.Wall1;
            }
        }

        public void CollisionCheck(Entity entity)
        {
            for(int i = 0; i < this.Environment.Length; i++)
            {
                if(this.Environment[i].HasHitbox())
                    entity.Intersect(this.Environment[i]);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < this.Environment.Length; i++)
                this.Environment[i].Draw(spriteBatch);
        }

    }
}