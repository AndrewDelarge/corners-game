using System;
using System.Collections.Generic;
using Core;
using Core.Data;
using UnityEngine;

namespace UI.Screen.Game.Elements
{
    public class UIPlayerMovesController : MonoBehaviour
    {
        [SerializeField] private UIPlayerMoves playerMovesPrefab;
        [SerializeField] private Transform playerMovesHolder;

        private List<UIPlayerMoves> playerMoves = new List<UIPlayerMoves>();

        public void Init()
        {
            CreatePlayerMovesElements();
        }

        private void CreatePlayerMovesElements()
        {
            playerMovesHolder.ClearChilds();
            
            foreach (var player in CoreData.GameSettings.Players)
            {
                var element = CreatePlayerMove(moves => moves.SetPlayer(player));
                
                playerMoves.Add(element);
            }
        }

        private UIPlayerMoves CreatePlayerMove(Action<UIPlayerMoves> callback)
        {
            var element = Instantiate(playerMovesPrefab, playerMovesHolder);
            
            callback?.Invoke(element);
            return element;
        }
    }
}