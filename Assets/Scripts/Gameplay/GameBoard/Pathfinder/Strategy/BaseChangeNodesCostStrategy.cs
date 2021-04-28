using System.Collections.Generic;

namespace Gameplay.GameBoard.Pathfinder.Strategy
{
    public abstract class BaseChangeNodesCostStrategy
    {
        public abstract void ChangeNodesCost(PathNode currentNode, PathNode neighbor);
    }
}