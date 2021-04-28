using System.Linq;
using CodeLib;
using Core.Data;
using Core.DataLoader;
using Core.GameMode;
using UnityEngine;

namespace Core
{
    public class GameManager : SingletonDD<GameManager>
    {
        [SerializeField] private SceneController sceneController;
        
        private void Awake()
        {
            Init();
            DontDestroyOnLoad(this);
        }

        private void Init()
        {
            LoadCoreData();
            UIManager.Instance().Init();
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.TO_MAIN_MENU_FROM_LOADER);
        }

        public void LoadCoreData()
        {
            var gameModeloader = new CornersGamemodeLoader();
            var result = (CornersGamemodeLoadResult) gameModeloader.Load();
            
            CoreData.GameSettings = new GameSettings(result.gameRuleDatas.ToList());
        }

        public void LoadMainMenu()
        {
            sceneController.LoadScene(CoreData.SceneIndexes.MENU, onMenuSceneLoaded);

            void onMenuSceneLoaded(AsyncOperation operation) 
            {
                GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.IN_MAIN_MENU);
            }
        }

        public void LoadGame()
        {
            sceneController.LoadScene(CoreData.SceneIndexes.GAME, onGameSceneLoaded);
            
            void onGameSceneLoaded(AsyncOperation operation) 
            {
                GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.START_GAME);
            }
        }
        
        public void ReLoadGame()
        {
            sceneController.ReLoadScene(CoreData.SceneIndexes.GAME, onUnload);
            
            void onUnload(AsyncOperation operation) 
            {
                GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.START_GAME);
            }
        }

        public void SetPauseGame(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
            // Some logic
            
        }
    }
}