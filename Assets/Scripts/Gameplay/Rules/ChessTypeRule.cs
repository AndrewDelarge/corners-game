using System.Collections.Generic;
using Gameplay.GameBoard;
using UnityEngine;

namespace Gameplay.Rules
{

    public struct FigurePosition
    {
        public FigureType FigureType { get; }
        public Vector2Int position { get; }

        public FigurePosition(FigureType figureType, Vector2Int position)
        {
            FigureType = figureType;
            this.position = position;
        }
    }
    public abstract class ChessTypeRule : BaseRule
    {
        public abstract bool EatAfterJumpOver();

        public abstract FigurePosition[] GetFigureStartPositions(int player);

        public abstract int GetPlayersCount();
        
        public abstract void SetBoard(BaseGameBoard board);

    }
}