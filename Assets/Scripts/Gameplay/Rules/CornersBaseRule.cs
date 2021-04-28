using System;
using System.Collections.Generic;
using Gameplay.GameBoard;
using Gameplay.GameBoard.Pathfinder;
using Gameplay.GameBoard.Pathfinder.Strategy;
using UnityEngine;

namespace Gameplay.Rules
{
    public abstract class CornersBaseRule : ChessTypeRule
    {
        public override int GetPlayersCount() => 2;
        
        protected BoardPathfinder BoardPathfinder;

        protected abstract BaseChangeNodesCostStrategy GetPathfinderStrategy();
        
        public override void SetBoard(BaseGameBoard newBoard)
        {
            BoardPathfinder = new BoardPathfinder(newBoard, GetPathfinderStrategy());
        }

        public abstract List<Vector2Int> GetPossibleMoves();
        
        public override FigurePosition[] GetFigureStartPositions(int player)
        {
            if (player >= GetPlayersCount())
                throw new Exception("Wrong player number");
            
            Vector2Int vectorOffset = new Vector2Int(player * 5, player * 5);
            
            return new FigurePosition[9]
            {
                new FigurePosition(FigureType.Checker, new Vector2Int(0, 0) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(0, 1) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(0, 2) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(1, 0) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(1, 1) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(1, 2) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(2, 0) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(2, 1) + vectorOffset), 
                new FigurePosition(FigureType.Checker, new Vector2Int(2, 2) + vectorOffset), 
            };
        }
        
        public List<Vector2Int> GetFinishPositions(int player)
        {
            var finishPos = new List<Vector2Int>();

            var figPos = GetFigureStartPositions(player == 0 ? 1 : 0);

            for (int i = 0; i < figPos.Length; i++)
            {
                finishPos.Add(figPos[i].position);
            }

            return finishPos;
        }
        
        public override bool IsMoveAllowed(Move move)
        {
            var path = BoardPathfinder.FindPath(move.OldPosition, move.NewPosition, GetPossibleMoves());

//            foreach (Vector2Int vector2Int in path)
//                BoardPathfinder.GetBoardIndex(vector2Int).SetColor(Color.green);

            return path.Count > 0;
        }
        
        
        protected bool IsPathCorrect(List<Vector2Int> path)
        {
            var movesCount = 0;
            var lastIndexWithFig = false;
            
            if (path.Count > 1 & path.Count % 2 != 0)
                return false;
            
            for (int i = 0; i < path.Count; i++)
            {
                movesCount++;
                
                var index = BoardPathfinder.GetBoardIndex(path[i]);

                if (lastIndexWithFig & index.Figure == null)
                {
                    lastIndexWithFig = false;
                    movesCount = 0;
                    continue;
                }
                
                if (movesCount == 1 & index.Figure == null)
                    continue;

                if (movesCount > 1 & lastIndexWithFig == false)
                    return false;

                if (movesCount == 1 & index.Figure != null)
                {
                    lastIndexWithFig = true;
                    continue;
                }

                return false;
            }
            
            return true;
        }

    }
}