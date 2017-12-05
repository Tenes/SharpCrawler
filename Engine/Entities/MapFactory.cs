using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpCrawler
{
    public static class MapFactory
    {
        public static List<Map> allMaps = new List<Map>();
        public static byte step;
        
        private static byte rightGenerationLuck = 100,
                            leftGenerationLuck = 100, 
                            topGenerationLuck = 100,
                            bottomGenerationLuck = 100;
        public static void SetStep(byte newStep)
        {
            step = newStep;
        }
        public static void FirstMap(EntityPlayer player)
        {
            rightGenerationLuck = 100;
            leftGenerationLuck = 100; 
            topGenerationLuck = 100;
            bottomGenerationLuck = 100;
            allMaps.Add(new Map(leftGenerationLuck, rightGenerationLuck, topGenerationLuck, bottomGenerationLuck, 100));
            player.SetActualMap(allMaps.First());
        }
        public static void ActualMapUpdate(this EntityPlayer player)
        {
            player.GetActualMap().Update(player);
        }
        public static void DrawMap(this EntityPlayer player, SpriteBatch spriteBatch)
        {
            player.GetActualMap().Draw(spriteBatch);
        }
    }
}