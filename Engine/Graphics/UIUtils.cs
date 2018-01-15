namespace SharpCrawler
{
    public static class UIUtils
    {
        public static MainScene mainScene;
        public static void LinkWeaponToUI(Weapon weapon) => mainScene.GetWeaponUI().SetDisplayed(weapon);
        public static void LinkHealthToUI(byte health) => mainScene.GetLivesUI().AffectCharacterHp(health);
        public static void ResetUI(LivesDisplay livesUI, WeaponDisplay weaponUI)
        {
            livesUI.Reset();
            weaponUI.Reset();
        }
        public static void TriggerWin(){
            mainScene.WinScreen();
        }
    }
}