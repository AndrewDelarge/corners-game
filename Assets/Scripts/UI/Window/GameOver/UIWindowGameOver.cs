using Core.GameMode;
using Gameplay;
using UI.Base;
using UI.Base.WindowConfigs;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.GameOver
{
    public class UIWindowGameOver : UIWindow
    {
        [SerializeField] private Text playerName;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button restartButton;
        
        private Player winner;
        public override void SetConfig(WindowConfig config)
        {
            base.SetConfig(config);
            var gameOverWindowConfig = (UIGameOverWindowConfig) config;
            winner = gameOverWindowConfig.winner;
        }

        public override void Open()
        {
            exitButton.onClick.AddListener(ExitToMenu);
            restartButton.onClick.AddListener(Restart);

            playerName.text = $"{winner.Name}!";
            
            base.Open();
        }

        private void ExitToMenu()
        {
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_GAMEOVER);
        }
        
        private void Restart()
        {
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.RESTART_GAME);
        }

        public override void Close()
        {
            exitButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();

            winner = null;
            base.Close();
        }
    }
}