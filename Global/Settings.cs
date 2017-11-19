using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public class Settings
    {

        public static float pixelRatio = 1f;


        public static int windowWidth = (int)(800 * pixelRatio);
        public static int windowHeight = (windowWidth * 9) / 16;

        public static bool fullscreen = false;
        public static bool visibleMouse = true;
    }
}