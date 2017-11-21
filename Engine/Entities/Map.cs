using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public enum Obstacle{Floor1, Floor2, Wall1, Wall2, Wall3}
    public class Map
    {
        private static short rightGenerationLuck = 100,
                            leftGenerationLuck = 100, 
                            topGenerationLuck = 100,
                            bottomGenerationLuck = 100;
        private Obstacle[,] centerMap = new Obstacle[16,16];
        private Map LeftMap,
                    RightMap,
                    TopMap,
                    BottomMap;
        public void SetLeftMap(Map leftMap, bool chaine = false)
        {
            this.LeftMap = leftMap;
            leftMap.SetRightMap(this);
        }
        public void SetRightMap(Map rightMap, bool chaine = false)
        {
            this.RightMap = rightMap;
            if(chaine)
                rightMap.SetLeftMap(this);
        }
        public void SetTopMap(Map topMap, bool chaine = false)
        {
            this.TopMap = topMap;
            topMap.SetBottomMap(this);
        }
        public void SetBottomMap(Map bottomMap, bool chaine = false)
        {
            this.BottomMap = bottomMap;
            bottomMap.SetTopMap(this);
        }
        public Map()
        {

        }

    }
}