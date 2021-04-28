using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GameBoard.Pathfinder
{
    public class PathNode
    {
        public Vector2Int pos { get; }
        public PathNode previous;
        public int cost = 0;
        
        public bool IsObstacle = false;
        
        public Dictionary<Vector2Int, int> CornersCost = new Dictionary<Vector2Int, int>()
        {
            {new Vector2Int(1, 1), 1},
            {new Vector2Int(-1, -1), 1},
            {new Vector2Int(-1, 1), 1},
            {new Vector2Int(1, -1), 1},
            
            {new Vector2Int(0, 1), 1},
            {new Vector2Int(0, -1), 1},
            {new Vector2Int(-1, 0), 1},
            {new Vector2Int(1, 0), 1},
        };
        
        public PathNode(Vector2Int pos, PathNode previous = null)
        {
            this.pos = pos;
            this.previous = previous;
        }
    }
}