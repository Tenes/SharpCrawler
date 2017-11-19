using System;

namespace SharpCrawler
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SharpCrawl())
                game.Run();
        }
    }
}
