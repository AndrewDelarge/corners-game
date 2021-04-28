using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screen.Game.Elements
{
    public class UIPlayerMoves : MonoBehaviour
    {
        [SerializeField] private Text playerName;
        [SerializeField] private Text textMovePrefab;
        [SerializeField] private Transform movesHolder;


        private List<Text> moves = new List<Text>();
        private void Awake()
        {
            movesHolder.ClearChilds();
        }

        public void SetPlayer(Player player)
        {
            playerName.text = player.Name;
            player.OnPlayerDoneMove += AddMove;
        }
        
        public void AddMove(Move move)
        {
            var textMove = Instantiate(textMovePrefab, movesHolder);
            textMove.text = move.ToString();
            
            moves.Add(textMove);
        }
    }
}