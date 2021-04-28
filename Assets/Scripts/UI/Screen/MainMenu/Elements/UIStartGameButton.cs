using System;
using Gameplay.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screen.MainMenu.Elements
{
    public class UIStartGameButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Button infoButton;
        [SerializeField] private Text text;
        
        private GameRuleData gameRuleData;
        public GameRuleData GameRuleData => gameRuleData;

        public Action<UIStartGameButton> OnClick;
        private void Start()
        {
            button.onClick.AddListener(() => OnClick?.Invoke(this));
        }

        public void SetGameMode(GameRuleData newGameRuleData)
        {
            gameRuleData = newGameRuleData;
            
            text.text = gameRuleData.Title;
        }
        
    }
}