using System;
using System.Collections.Generic;
using Core;
using Core.Data;
using UnityEngine;

namespace UI.Screen.MainMenu.Elements
{
    public class UIPlayerEditController : MonoBehaviour
    {
        [SerializeField] private UIPlayerEdit playerEditPrefab;
        [SerializeField] private Transform playerEditHolder;
        
        private List<UIPlayerEdit> playerEditElements = new List<UIPlayerEdit>();

        public void Init()
        {
            InitPlayerEditElements();
        }

        private void InitPlayerEditElements()
        {
            playerEditHolder.ClearChilds();

            foreach (var player in CoreData.GameSettings.Players)
            {
                var uiElement = CreatePlayerEditElement(playerEdit =>
                {
                    playerEdit.SetTitle(player.Name);
                    playerEdit.OnPlayerNameChanged += player.SetName;
                });
                
                playerEditElements.Add(uiElement);
            }
        }

        // TODO ELEMENT INSTANTINATOR
        private UIPlayerEdit CreatePlayerEditElement(Action<UIPlayerEdit> callback = null)
        {
            var element = Instantiate(playerEditPrefab, playerEditHolder);
            
            callback?.Invoke(element);
            return element;
        }
        
    }
}