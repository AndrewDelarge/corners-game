using System;
using System.Collections.Generic;
using Gameplay.GameBoard;
using Gameplay.GameBoard.Pathfinder.Strategy;
using UnityEngine;

namespace Gameplay.Rules
{
    public class CornersDiagonal : CornersBaseRule
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
                new Vector2Int(1, 1),
                new Vector2Int(-1, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(1, -1),
            };
        }
    }
}