using System;
using Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Screen.MainMenu.Elements
{
    public class UIPlayerEdit : MonoBehaviour
    {
        [SerializeField] private Text title;
        [SerializeField] private InputField playerNameInput;

        public UnityAction<string> OnPlayerNameChanged;
        
        private Player player;
        private void Start()
        {
            playerNameInput.onEndEdit.AddListener(OnPlayerNameChanged);
        }

        public void SetTitle(string newTitle)
        {
            title.text = newTitle;
        }
    }
}