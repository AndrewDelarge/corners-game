using System;
using System.Collections.Generic;
using System.Linq;
using CodeLib;
using Gameplay;
using Gameplay.GameBoard;
using UnityEngine;

namespace Core
{
    public class SelectedFigure
    {
        public Figure Figure { get; }
        public Vector2Int CurrentPos { get; }

        public SelectedFigure(Figure figure, Vector2Int currentPos)
        {
            Figure = figure;
            CurrentPos = currentPos;
        }
    }
    
    public class PlayersInput : Singleton<PlayersInput>
    {
        private SelectedFigure currentSelectedFigure;

        private Dictionary<Player, List<Figure>> playerFigures;

        public Action<Move> OnTryDoMove;
        
        public void Init(Dictionary<Player, List<Figure>> newPlayerFigures, BaseGameBoard board)
        {
            playerFigures = newPlayerFigures;

            board.OnIndexClick += OnBoardIndexClick;
        }

        private void OnBoardIndexClick(BoardIndex index, Vector2Int pos)
        {
            if (index.Figure != null)
            {
                SelectFigure(index.Figure, pos);
                return;
            }
            
            if (currentSelectedFigure == null)
                return;
            
            var move = new Move(GetPlayer(currentSelectedFigure.Figure), currentSelectedFigure.Figure, currentSelectedFigure.CurrentPos, pos);
            
            OnTryDoMove?.Invoke(move);
            UnSelect();
        }

        //TODO make it simple
        public Player GetPlayer(Figure figure)
        {
            return playerFigures.FirstOrDefault(pair => pair.Value.Find(x => x == figure) == figure).Key;
        }

        private void SelectFigure(Figure figure, Vector2Int pos)
        {
            currentSelectedFigure?.Figure.SetSelect(false);

            if (currentSelectedFigure?.Figure == figure)
            {
                currentSelectedFigure = null;
                return;
            }
            
            currentSelectedFigure = new SelectedFigure(figure, pos);
            currentSelectedFigure.Figure.SetSelect(true);
        }

        private void UnSelect()
        {
            currentSelectedFigure?.Figure.SetSelect(false);
            currentSelectedFigure = null;
        }
    }
}