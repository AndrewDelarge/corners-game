using Core.GameMode;
using UI.Base;
using UI.Screen.Game.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screen.Game
{
    public class UIScreenGame : UIView
    {
        [SerializeField] private UIPlayerMovesController uiPlayerMovesController;
        [SerializeField] private UIUpperPanelController uiUpperPanelController;


        [SerializeField] private Button pauseButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        
        public override void Open()
        {
            uiPlayerMovesController.Init();
            uiUpperPanelController.Init();
            
            pauseButton.onClick.AddListener(Pause);
            restartButton.onClick.AddListener(Restart);
            exitButton.onClick.AddListener(Exit);
            
            base.Open();
        }

        private void Exit()
        {
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_INTERRUPTION);
        }

        private void Restart()
        {
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.RESTART_GAME);
        }

        private void Pause()
        {
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.PAUSE_GAME);
        }

        public override void Close()
        {
            pauseButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            
            base.Close();
        }
    }
}