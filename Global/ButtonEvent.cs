namespace SharpCrawler
{
    public static class ButtonEvent
    {
        //METHODS
        public static void Quit(ClickableText thisButton, SharpCrawl game) => game.Exit();
        public static void FromGameToMainMenu(Button thisButton, MainScene mainScene)
        {
            mainScene.GetGame().SetGameState(false);
            mainScene.Restart();
        }
        public static void FromStatsToMainMenu(ClickableText thisButton, SharpCrawl game)
        {
            game.SetGameState(false);
            game.GetMainScene().Restart();
        }
        public static void Retry(Button thisButton, MainScene game) => game.Restart();
        public static void Launch(ClickableText thisText, SharpCrawl game) => game.SetGameState(true);
    }
}
