namespace SharpCrawler
{
    public static class ButtonEvent
    {
        //METHODS
        public static void Quit(Button thisButton, SharpCrawl game) => game.Exit();
        public static void Retry(Button thisButton, MainScene game) => game.Restart();
    }
}
