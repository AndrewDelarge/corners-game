using UnityEngine;

namespace Gameplay.GameBoard.Pathfinder.Strategy
{
    class CornersCostStrategyDefault : BaseChangeNodesCostStrategy
    {
        public override void ChangeNodesCost(PathNode currentNode, PathNode neighbor)
        {
            var neighborDirection = neighbor.pos - currentNode.pos;
            var nodeDirection = Vector2Int.zero;
                    
            if (currentNode.previous != null)
                nodeDirection = currentNode.pos - currentNode.previous.pos;

            if (currentNode.IsObstacle & neighbor.IsObstacle)
                neighbor.cost = 5;
            else if (currentNode.IsObstacle & neighborDirection == nodeDirection)
            {
                currentNode.cost = 0;
                neighbor.cost = 1;
            }
            else if (currentNode.IsObstacle & neighborDirection != nodeDirection)
                neighbor.cost = 5;
            else
                neighbor.cost += currentNode.cost + 1;
        }
    }
}