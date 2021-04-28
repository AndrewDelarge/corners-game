using UI.Base;
using UI.Screen.MainMenu.Elements;
using UnityEngine;

namespace UI.Screen.MainMenu
{
    public class UIScreenMainMenu : UIView
    {
        [SerializeField] private UIMenuController uiMenuController;
        [SerializeField] private UIPlayerEditController uiPlayerEditController;


        public override void Open()
        {
            uiMenuController.Init();
            uiPlayerEditController.Init();
            
            base.Open();
        }

        public override void Close()
        {
            base.Close();
        }
    }
}