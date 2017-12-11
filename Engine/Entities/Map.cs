using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SharpCrawler
{
    public enum ObstacleType{Floor1, Floor2, Wall1, Wall2, Wall3, AlternativeWall, Void, Portal}
    public class Map
    {
        private static byte currentLeftLuck,
                            currentRightLuck,
                            currentTopLuck,
                            currentBottomLuck,
                            currentStep;
        private static Random mapRng = new Random();
        private ObstacleType[,] centerMap;
        private Entity[] Environment;
        private List<Ground> Ground;
        private List<Obstacle> Obstacles;
        private List<Portal> Portals;
        private Map LeftMap,
                    RightMap,
                    TopMap,
                    BottomMap;
        private Weapon pickableWeapon;
        public void NullifyWeapon()
        {
            this.pickableWeapon = null;
        }
        public Map(byte leftLuck, byte rightLuck, byte topLuck, byte bottomLuck, byte step)
        {
            currentLeftLuck = leftLuck;
            currentRightLuck = rightLuck;
            currentTopLuck = topLuck;
            currentBottomLuck = bottomLuck;
            currentStep = step;
            this.pickableWeapon = new Weapon(new Sprite("TileSet", 300, 300, 0.5f, Ressources.Sword1(), Settings.scale - 0.5f), 2, 5);
            this.initMap();
            this.GeneratePortals();
        }
        public Map()
        {
            this.initMap();
        }
        public void initMap()
        {
            ObstacleType w = ObstacleType.Wall1;
            ObstacleType v = ObstacleType.Void;
            ObstacleType wa = ObstacleType.AlternativeWall;
            ObstacleType f = ObstacleType.Floor1;
            this.centerMap = new ObstacleType[16,16]
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
        }
        private void GenerateLefttMap(EntityPlayer entity, byte portalStart)
        {
            currentLeftLuck -= (currentLeftLuck > 0) ? currentStep: (byte)0;
            Map tempMap = new Map();
            this.LeftMap = tempMap;
            tempMap.RightMap = this;
            tempMap.GeneratePortals(portalStart);
            MapFactory.allMaps.Append(tempMap);
            entity.SetActualMap(tempMap);
        }
        private void GenerateRightMap(EntityPlayer entity, byte portalStart)
        {
            currentRightLuck -= (currentRightLuck > 0) ? currentStep: (byte)0;
            Map tempMap = new Map();
            this.RightMap = tempMap;
            tempMap.LeftMap = this;
            tempMap.GeneratePortals(portalStart);
            MapFactory.allMaps.Append(tempMap);
            entity.SetActualMap(tempMap);
        }
        private void GenerateTopMap(EntityPlayer entity, byte portalStart)
        {
            currentTopLuck -= (currentTopLuck > 0) ? currentStep: (byte)0;
            Map tempMap = new Map();
            this.TopMap = tempMap;
            tempMap.BottomMap = this;
            tempMap.GeneratePortals(portalStart);
            MapFactory.allMaps.Append(tempMap);
            entity.SetActualMap(tempMap);
        }
        private void GenerateBottomtMap(EntityPlayer entity, byte portalStart)
        {
            currentBottomLuck -= (currentBottomLuck > 0) ? currentStep: (byte)0;
            Map tempMap = new Map();
            this.BottomMap = tempMap;
            tempMap.TopMap = this;
            tempMap.GeneratePortals(portalStart);
            MapFactory.allMaps.Append(tempMap);
            entity.SetActualMap(tempMap);
        }
        private void GeneratePortals(byte portalStart = 0)
        {
            if(LeftMap == null)
                GenerateLeftPortals();
            else
                PlaceLeftPortals(portalStart);
            if(RightMap == null)
                GenerateRightPortals();
            else
                PlaceRightPortals(portalStart);
            if(BottomMap == null)
                GenerateBottomPortals();
            else
                PlaceBottomPortals(portalStart);
            if(TopMap == null)
                GenerateTopPortals();
            else
                PlaceTopPortals(portalStart);

            this.Environment = new Entity[this.centerMap.Length];
            this.Ground = new List<Ground>();
            this.Obstacles = new List<Obstacle>();
            this.Portals = new List<Portal>();
            int scaledPosition = (int)(Settings.tileSize * Settings.scale);
            for(int row = 0; row < this.centerMap.GetLength(0); row++)
            {
                for(int column = 0; column < this.centerMap.GetLength(1); column++)
                {
                    Entity tempEnv;
                    switch(this.centerMap[row, column])
                    {
                        case ObstacleType.Wall1:
                            tempEnv = EntityFactory.WallBuilder(Ressources.Wall1(), column*scaledPosition, row*scaledPosition, Settings.scale); 
                            this.Obstacles.Add(tempEnv as Obstacle);
                            break;
                        case ObstacleType.AlternativeWall:
                            tempEnv = EntityFactory.VoidOrAlternativeBuilder(Ressources.AlternativeWall(), column*scaledPosition, row*scaledPosition, Settings.scale);
                            this.Obstacles.Add(tempEnv as Obstacle);
                            break;
                        case ObstacleType.Void:
                            tempEnv = EntityFactory.VoidOrAlternativeBuilder(Ressources.Void(), column*scaledPosition, row*scaledPosition, Settings.scale);
                            this.Obstacles.Add(tempEnv as Obstacle);
                            break;
                        case ObstacleType.Portal:
                            tempEnv = EntityFactory.PortalBuilder(column*scaledPosition, row*scaledPosition, Settings.scale);
                            this.Portals.Add(tempEnv as Portal);
                            break;
                        case ObstacleType.Floor1:
                        default:
                            tempEnv = EntityFactory.FloorBuilder(column*scaledPosition, row*scaledPosition, Settings.scale);                        
                            this.Ground.Add(tempEnv as Ground);                        
                            break;
                    }
                    this.Environment[this.centerMap.GetLength(0)*row + column] = tempEnv; 
                }
            }
        }
        private void GenerateLeftPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(0, 13);
            if(mapRng.Next(101) < currentLeftLuck)
            {
                this.centerMap[portalsPosition, 0] = ObstacleType.Wall1;
                this.centerMap[portalsPosition + 1, 0] = ObstacleType.Portal;
                this.centerMap[portalsPosition + 2, 0] = ObstacleType.Portal;
                this.centerMap[portalsPosition + 3, 0] = ObstacleType.AlternativeWall;
            }
        }
        private void PlaceLeftPortals(byte portalStart)
        {
            this.centerMap[portalStart, 0] = ObstacleType.Wall1;
            this.centerMap[portalStart + 1, 0] = ObstacleType.Portal;
            this.centerMap[portalStart + 2, 0] = ObstacleType.Portal;
            this.centerMap[portalStart + 3, 0] = ObstacleType.AlternativeWall;
        }
        private void GenerateRightPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(0, 13);
            if(mapRng.Next(101) < currentRightLuck)
            {
                this.centerMap[portalsPosition, 15] = ObstacleType.Wall1;
                this.centerMap[portalsPosition + 1, 15] = ObstacleType.Portal;
                this.centerMap[portalsPosition + 2, 15] = ObstacleType.Portal;
                this.centerMap[portalsPosition + 3, 15] = ObstacleType.AlternativeWall;
            }
        }
        private void PlaceRightPortals(byte portalStart)
        {
            this.centerMap[portalStart, 15] = ObstacleType.Wall1;
            this.centerMap[portalStart + 1, 15] = ObstacleType.Portal;
            this.centerMap[portalStart + 2, 15] = ObstacleType.Portal;
            this.centerMap[portalStart + 3, 15] = ObstacleType.AlternativeWall;
        }
        private void GenerateBottomPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(1, 12);
            if(mapRng.Next(101) < currentBottomLuck)
            {
                this.centerMap[15, portalsPosition] = ObstacleType.AlternativeWall;
                this.centerMap[15, portalsPosition + 1] = ObstacleType.Portal;
                this.centerMap[15, portalsPosition + 2] = ObstacleType.Portal;
                this.centerMap[15, portalsPosition + 3] = ObstacleType.AlternativeWall;
            }
        }
        private void PlaceBottomPortals(byte portalStart)
        {
            this.centerMap[15, portalStart] = ObstacleType.AlternativeWall;
            this.centerMap[15, portalStart + 1] = ObstacleType.Portal;
            this.centerMap[15, portalStart + 2] = ObstacleType.Portal;
            this.centerMap[15, portalStart + 3] = ObstacleType.AlternativeWall;
        }
        private void GenerateTopPortals()
        {
            byte portalsPosition = (byte)mapRng.Next(1, 12);
            if(mapRng.Next(101) < currentTopLuck)
            {
                this.centerMap[0, portalsPosition] = ObstacleType.Wall1;
                this.centerMap[0, portalsPosition + 1] = ObstacleType.Portal;
                this.centerMap[0, portalsPosition + 2] = ObstacleType.Portal;
                this.centerMap[0, portalsPosition + 3] = ObstacleType.Wall1;
            }
        }
        private void PlaceTopPortals(byte portalStart)
        {
            this.centerMap[0, portalStart] = ObstacleType.Wall1;
            this.centerMap[0, portalStart + 1] = ObstacleType.Portal;
            this.centerMap[0, portalStart + 2] = ObstacleType.Portal;
            this.centerMap[0, portalStart + 3] = ObstacleType.Wall1;
        }
        public void WarpLeft(EntityPlayer entity, byte portalStart)
        {
            if(entity.GetActualMap().LeftMap == null)
                GenerateLefttMap(entity, portalStart);
            else if(entity.GetActualMap().LeftMap != null)
                entity.SetActualMap(entity.GetActualMap().LeftMap);
            entity.SetOffsetPosition((int)(14 * Settings.tileSize * Settings.scale), (int)((portalStart + 1.5) *Settings.tileSize * Settings.scale));
        }
        public void WarpRigt(EntityPlayer entity, byte portalStart)
        {
            if(entity.GetActualMap().RightMap == null)
                GenerateRightMap(entity, portalStart);
            else if(entity.GetActualMap().RightMap != null)
                entity.SetActualMap(entity.GetActualMap().RightMap);
            entity.SetOffsetPosition((int)(1 * Settings.tileSize * Settings.scale), (int)((portalStart + 1.5) *Settings.tileSize * Settings.scale));
        }
        public void WarpBottom(EntityPlayer entity, byte portalStart)
        {
            if(entity.GetActualMap().BottomMap == null)
                GenerateBottomtMap(entity, portalStart);
            else if(entity.GetActualMap().BottomMap != null)
                entity.SetActualMap(entity.GetActualMap().BottomMap);
            entity.SetOffsetPosition((int)((portalStart+1.5) * Settings.tileSize * Settings.scale), (int)(1*Settings.tileSize * Settings.scale));
        }
        public void WarpTop(EntityPlayer entity, byte portalStart)
        {
            if(entity.GetActualMap().TopMap == null)
                GenerateTopMap(entity, portalStart);
            else if(entity.GetActualMap().TopMap != null)
                entity.SetActualMap(entity.GetActualMap().TopMap);
            entity.SetOffsetPosition((int)((portalStart+1.5)  * Settings.tileSize * Settings.scale), (int)(14 *Settings.tileSize * Settings.scale));
        }
        public void CheckWarp(EntityPlayer entity)
        {
            for(int i = 0; i < this.Portals.Count; i++)
            {
                if(entity.Intersect(this.Portals[i]))
                {
                    byte index = (byte)Array.IndexOf(this.Environment, this.Portals[i]);
                    byte x = (byte)(index%this.centerMap.GetLength(1));
                    byte y = (byte)(index/this.centerMap.GetLength(0));
                    if(x == 0)
                        WarpLeft(entity, (this.centerMap[y-1, x] == ObstacleType.Wall1)? (byte)(y-1) : (byte)(y-2));
                    else if(x == 15)
                        WarpRigt(entity, (this.centerMap[y-1, x] == ObstacleType.Wall1)? (byte)(y-1) : (byte)(y-2));
                    else if(y == 15)
                        WarpBottom(entity, (this.centerMap[y, x-1] == ObstacleType.AlternativeWall)? (byte)(x-1) : (byte)(x-2));
                    else if(y == 0)
                        WarpTop(entity, (this.centerMap[y, x-1] == ObstacleType.Wall1)? (byte)(x-1) : (byte)(x-2));
                }
            }
        }

        public void CollisionCheck(Entity entity)
        {
            for(int i = 0; i < this.Obstacles.Count; i++)
            {
                if(entity.Intersect(this.Obstacles[i]))
                    entity.CancelUpdatePosition(this.Obstacles[i]);
            }
        }
        public void Update(EntityPlayer entity)
        {
            this.CollisionCheck(entity);
            this.CheckWarp(entity);
            this.pickableWeapon?.UpdateOnGround(entity);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < this.Environment.Length; i++)
                this.Environment[i].Draw(spriteBatch);
            this.pickableWeapon?.Draw(spriteBatch);
        }

    }
}