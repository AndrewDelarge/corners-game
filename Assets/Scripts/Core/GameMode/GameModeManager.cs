using System;
using System.Collections.Generic;
using CodeLib;
using UnityEngine;

namespace Core.GameMode
{
    public class GameModeManager : SingletonDD<GameModeManager>
    {
        public enum GameModeID
        {
            None,
            MainMenu,
            InGame
        }
        
        public enum GameModeState
        {
            NONE,
            IN_MAIN_MENU,
            TO_MAIN_MENU_FROM_LOADER,
            TO_GAME_FROM_MAIN_MENU,
            IN_GAME,
            START_GAME,
            TO_MAIN_MENU_FROM_GAME_WITH_GAMEOVER,
            TO_MAIN_MENU_FROM_GAME_WITH_INTERRUPTION,
            RESTART_GAME,
            PAUSE_GAME,
            UN_PAUSE_GAME
        }
        
        
        public Action<GameModeState> OnGameStateChangeAction = (GameModeState obj) => { };

        
        private static GameMode None = new GameMode();
        private static GameMode MainMenu = new GameModeMainMenu();
        private static GameMode InGame = new GameModeInGame();


        private Dictionary<GameModeID, GameMode> _availableGameModes = new Dictionary<GameModeID, GameMode>
        {
            { GameModeID.None, None },
            { GameModeID.MainMenu, MainMenu },
            { GameModeID.InGame, InGame }
        };

        private GameModeState _currentGameState = GameModeState.NONE;
        
        public GameModeState State => _currentGameState;

        private GameModeID _activeGameMode = GameModeID.None;
        
        public virtual GameMode ActiveGameMode 
        {
            get { return _availableGameModes[_activeGameMode]; }
        }

        private void ActivateGameMode(GameModeID gameMode) 
        {
            _availableGameModes[_activeGameMode].Deactivate();
            _activeGameMode = gameMode;
            _availableGameModes[_activeGameMode].Activate();
        }

        private void GameStateChanged()
        {
            switch (_currentGameState)
            {
                case GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_GAMEOVER:
                case GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_INTERRUPTION:
                case GameModeState.TO_MAIN_MENU_FROM_LOADER:
                    ActivateGameMode(GameModeID.MainMenu);
                    break;
                case GameModeState.RESTART_GAME:
                case GameModeState.TO_GAME_FROM_MAIN_MENU:
                    ActivateGameMode(GameModeID.InGame);
                    break;
            }

            OnGameStateChangeAction(State);
        }
        
        
        public void SetGameState(GameModeState newState)
        {
            if (_currentGameState == newState)
                return;
            
            Debug.Log($"## Changing game state from: {_currentGameState} to: {newState} in progress ##");

            switch (newState)
            {
                case GameModeState.NONE:
                    break;
                case GameModeState.TO_MAIN_MENU_FROM_LOADER:
                    break;
                case GameModeState.TO_GAME_FROM_MAIN_MENU:
                    break;
                case GameModeState.IN_MAIN_MENU:
                    break;
                case GameModeState.IN_GAME:
                    if (! CanInGame)
                        return;
                    break;
                case GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_GAMEOVER:
                case GameModeState.TO_MAIN_MENU_FROM_GAME_WITH_INTERRUPTION:
                    if (! CanInMenuFromGame)
                        return;
                    break;
                case GameModeState.RESTART_GAME:
                    if (! CanRestart)
                        return;
                    break;
                case GameModeState.PAUSE_GAME:
                    if (! CanPause)
                        return;
                    break;
                case GameModeState.UN_PAUSE_GAME:
                    if (! CanUnPause)
                        return;
                    break;
                case GameModeState.START_GAME:
                    if (! CanStartGame)
                        return;
                    break;
            }

            _currentGameState = newState;
            
            GameStateChanged();
        }


        private bool CanStartGame => _currentGameState == GameModeState.TO_GAME_FROM_MAIN_MENU || _currentGameState == GameModeState.RESTART_GAME;
        private bool CanInGame => _currentGameState == GameModeState.START_GAME || _currentGameState == GameModeState.UN_PAUSE_GAME;
        private bool CanInMenuFromGame => _currentGameState == GameModeState.IN_GAME;
        
        private bool CanRestart => _currentGameState == GameModeState.IN_GAME;
        
        private bool CanPause => _currentGameState == GameModeState.IN_GAME;
        private bool CanUnPause => _currentGameState == GameModeState.PAUSE_GAME;
        
        
    }
}