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
                            bottomGenerationLuck = 100,
                            endLuck = 0;
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
            allMaps.Add(new Map(leftGenerationLuck, rightGenerationLuck, topGenerationLuck, bottomGenerationLuck, 100, endLuck));
            player.SetActualMap(allMaps.First());
        }
        public static void Reset(EntityPlayer player)
        {
            rightGenerationLuck = 100;
            leftGenerationLuck = 100; 
            topGenerationLuck = 100;
            bottomGenerationLuck = 100;
            endLuck = 0;
            allMaps.Clear();
            allMaps.Add(new Map(leftGenerationLuck, rightGenerationLuck, topGenerationLuck, bottomGenerationLuck, 100, endLuck));
            player.SetActualMap(allMaps.First());
        }
        public static void ActualMapUpdate(this EntityPlayer player, GameTime gameTime) => player.GetActualMap().Update(gameTime, player);
        public static void DrawMap(this EntityPlayer player, SpriteBatch spriteBatch) => player.GetActualMap().Draw(spriteBatch);
    }
}