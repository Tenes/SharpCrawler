using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpCrawler
{
    public static class UIUtils
    {
        public static MainScene mainScene;
        public static void LinkWeaponToUI(Weapon weapon)
        {
            mainScene.GetWeaponUI().SetDisplayed(weapon);
        }
        public static void LinkHealthToUI(byte health)
        {
            mainScene.GetLivesUI().AffectCharacterHp(health);
        }
    }
}