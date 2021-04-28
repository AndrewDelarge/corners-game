using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.GameMode;
using Gameplay.Scriptable;
using UnityEngine;

namespace UI.Screen.MainMenu.Elements
{
    public class UIMenuController : MonoBehaviour
    {
        [SerializeField] private UIStartGameButton menuButtonPrefab;
        [SerializeField] private Transform menuButtonHolder;
        public List<UIStartGameButton> UiGameModeButtons { get; } = new List<UIStartGameButton>();

        public void Init()
        {
            InitButtons();
        }

        private void InitButtons()
        {
            menuButtonHolder.ClearChilds();

            foreach (GameRuleData data in CoreData.GameSettings.GameRuleData)
            {
                var uiElement = CreateMenuButton(button => button.SetGameMode(data));
                
                uiElement.OnClick += StartGame;
                
                UiGameModeButtons.Add(uiElement);
            }
        }

        private UIStartGameButton CreateMenuButton(Action<UIStartGameButton> callback)
        {
            // TODO ! UI ELEMENTS INSTANTINATOR
            var startGameButton = Instantiate(menuButtonPrefab, menuButtonHolder);
            
            callback?.Invoke(startGameButton);
            return startGameButton;
        }
        
        private void StartGame(UIStartGameButton startButton)
        {
            CoreData.GameSettings.SetRule(startButton.GameRuleData);
            GameModeManager.Instance().SetGameState(GameModeManager.GameModeState.TO_GAME_FROM_MAIN_MENU);
        }
        
    }
}