using Core.Base;
using Core.Data;
using Core.DataLoader;
using UnityEngine;

namespace Core.GameMode
{
    public class GameModeInGame : GameMode
    {

        protected override void GameStateChanged(GameModeManager.GameModeState state)
        {
            switch (state)
            {
                case GameModeManager.GameModeState.TO_GAME_FROM_MAIN_MENU:
                    ToGameFromMainMenu();
                    break;
                case GameModeManager.GameModeState.IN_GAME:
                    InGame();
                    break;
                case GameModeManager.GameModeState.START_GAME:
                    StartGame();
                    break;
                case GameModeManager.GameModeState.RESTART_GAME:
                    RestartGame();
                    break;
                case GameModeManager.GameModeState.PAUSE_GAME:
                    PauseGame();
                    break;
                case GameModeManager.GameModeState.UN_PAUSE_GAME:
                    UnPauseGame();
                    break;

            }
        }

        private void InGame()
        {
            
        }

        private void UnPauseGame()
        {
            UIManager.Instance().CloseWindow(UIManager.UIWindows.Pause);
            
            GameManager.Instance().SetPauseGame(false);
            
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.IN_GAME);
        }

        private void PauseGame()
        {
            UIManager.Instance().OpenWindow(UIManager.UIWindows.Pause);
            GameManager.Instance().SetPauseGame(true);
        }
        
        private void RestartGame()
        {
            BaseGameController.Instance().InterruptGame();

            UIManager.Instance().CloseAllWindows();
            UIManager.Instance().SetScreen(UIManager.UIScreens.None);

            GameManager.Instance().ReLoadGame();
        }

        private void ToGameFromMainMenu()
        {
            GameManager.Instance().LoadGame();
        }

        private void StartGame()
        {
            var gameDataLoader = new CornersGameDataLoader();

            UIManager.Instance().SetScreen(UIManager.UIScreens.InGame);

            BaseGameController.Instance().SetRule(CoreData.GameSettings.CurrentRule);

            BaseGameController.Instance().SetData(gameDataLoader.Load());
            
            BaseGameController.Instance().StartGame();

            BaseGameController.Instance().onGameOver += result => CoreData.GameSettings.GameResultHandler.Handle(result);
            
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.IN_GAME);
        }

    }
}