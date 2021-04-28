using System.Collections.Generic;
using Gameplay.GameBoard;
using Gameplay.GameBoard.Pathfinder.Strategy;
using UnityEngine;

namespace Gameplay.Rules
{
    public class CornersHorizontalAndVertical : CornersBaseRule
    {
        public override bool EatAfterJumpOver() => false;

        protected override BaseChangeNodesCostStrategy GetPathfinderStrategy()
        {
            return new CornersCostStrategyDefault();
        }

        public override List<Vector2Int> GetPossibleMoves()
        {
            return new List<Vector2Int>()
            {
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
            };
        }

        public bool IsMoveAllowedTmp(Move move)
        {
            var moveLength = move.GetMoveLength();
            
            if (Mathf.Abs(moveLength.x) > 0 & Mathf.Abs(moveLength.y) > 0)
                return false;
            
            // TODO rework to "find path"
            var path = BoardPathfinder.GetPath(move);

            return IsPathCorrect(path);
        }
    }
}