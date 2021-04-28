namespace Core.GameMode
{
    public class GameModeMainMenu : GameMode
    {

        protected override void GameStateChanged(GameModeManager.GameModeState state)
        {
            switch (state)
            {
                case GameModeManager.GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_INTERRUPTION:
                case GameModeManager.GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_GAMEOVER:
                    ToMainMenuGame();
                    break;
                case GameModeManager.GameModeState.TO_MAIN_MENU_FROM_LOADER:
                    ToMainMenuFromLoader();
                    break;
                case GameModeManager.GameModeState.IN_MAIN_MENU:
                    InMainMenu();
                    break;
                
                
            }
        }

        private void ToMainMenuFromLoader()
        {
            GameManager.Instance().LoadMainMenu();
        }
        
        private void ToMainMenuGame()
        {
            UIManager.Instance().CloseAllWindows();
            GameManager.Instance().LoadMainMenu();
        }

        private void InMainMenu()
        {
            UIManager.Instance().SetScreen(UIManager.UIScreens.MainMenu);
        }
    }
}