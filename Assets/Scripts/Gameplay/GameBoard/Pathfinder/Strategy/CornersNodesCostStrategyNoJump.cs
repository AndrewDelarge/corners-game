namespace Gameplay.GameBoard.Pathfinder.Strategy
{
    class CornersNodesCostStrategyNoJump : BaseChangeNodesCostStrategy
    {
        public override void ChangeNodesCost(PathNode currentNode, PathNode neighbor)
        {
            if (neighbor.IsObstacle)
                neighbor.cost = 5;
            
            neighbor.cost += currentNode.cost + 1;
        }
    }
}