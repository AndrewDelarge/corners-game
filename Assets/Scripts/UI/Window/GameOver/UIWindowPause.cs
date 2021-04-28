using Core;
using Core.GameMode;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.GameOver
{
    public class UIWindowPause : UIWindow
    {
        [SerializeField] private Button resumeButton;
        
        public override void Open()
        {
            resumeButton.onClick.AddListener(UnPause);
            base.Open();
        }

        private void UnPause()
        {
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.UN_PAUSE_GAME);
        }

        public override void Close()
        {
            resumeButton.onClick.RemoveAllListeners();

            base.Close();
        }
    }
}