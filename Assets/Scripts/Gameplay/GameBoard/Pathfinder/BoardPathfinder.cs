using System.Collections.Generic;
using System.Linq;
using Gameplay.GameBoard.Pathfinder.Strategy;
using UnityEngine;

namespace Gameplay.GameBoard.Pathfinder
{
    public class BoardPathfinder
    {
        private BaseGameBoard board;
        private List<PathNode> nodes = new List<PathNode>();
        private List<Vector2Int> possiblePaths = new List<Vector2Int>();

        private BaseChangeNodesCostStrategy strategy;
        
        public BoardPathfinder(BaseGameBoard board, BaseChangeNodesCostStrategy costStrategy)
        {
            strategy = costStrategy;
            this.board = board;
        }

        public List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int targetPos, List<Vector2Int> newPossiblePaths)
        {
            CreateNodes();
            
            possiblePaths = newPossiblePaths;
            
            var startNode = GetNode(startPos);
            startNode.IsObstacle = false;
            
            var goalNode = GetNode(targetPos);

            var path = new List<Vector2Int>();
            var reachable = new List<PathNode>();
            var explored = new List<PathNode>();

            reachable.Add(startNode);
            
            while (reachable.Count > 0)
            {
                var node = ChooseNode(reachable);
                
                if (node == null)
                    break;
                
                if (node == goalNode)
                {
//                    UpdateCoastsVisual();
//                    VisualizePath(reachable, explored, node);
                    return BuildPath(goalNode);
                }

                reachable.Remove(node);
                explored.Add(node);

                var newReachable = GetNeighborNodes(node).Except(explored);

                
                foreach (PathNode neighbor in newReachable)
                {
                    if (reachable.Contains(neighbor))
                    {
                        continue;
                    }
                    neighbor.previous = node;
                    strategy.ChangeNodesCost(node, neighbor);
                    
                    reachable.Add(neighbor);
                }
                
//                UpdateCoastsVisual();
            }

            return path;
        }


        private PathNode ChooseNode(List<PathNode> reachable)
        {
            return reachable.Find(x => x.cost <= 1);
        }

        private List<PathNode> GetNeighborNodes(PathNode node)
        {
            List<PathNode> neighborNodes = new List<PathNode>();

            foreach (Vector2Int possiblePath in possiblePaths)
            {
                var newNode = GetNode(node.pos + possiblePath);
                
                if (newNode == null) continue;
                if (node.previous == newNode) continue;
                
                
                
                //Also Worked with this
//                
//                if (HasFigureOnBoardPos(newNode.pos))
//                {
//                    var afterFigureNode = GetNode(newNode.pos + possiblePath);
//                    
//                    if (afterFigureNode == null) continue;
//                    if (HasFigureOnBoardPos(afterFigureNode.pos)) continue;
//
//                    GetBoardIndex(afterFigureNode.pos).SetColor(Color.cyan);
//                    afterFigureNode.cost = 0;
//                    path.Add(afterFigureNode);
//                }
                
                neighborNodes.Add(newNode);
            }

            return neighborNodes;
        }
        
        private List<Vector2Int> BuildPath(PathNode node)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            
            while (node.previous != null)
            {
                path.Add(node.pos);
                node = node.previous;
            }

            return path;
        }

        private void CreateNodes()
        {
            nodes.Clear();

            for (int y = 0; y < board.GetSize().y; y++)
            {
                for (int x = 0; x < board.GetSize().x; x++)
                {
                    var node = new PathNode(new Vector2Int(x, y), null);
                    node.IsObstacle = HasFigureOnBoardPos(node.pos);
                    nodes.Add(node);
                }
            }
            
        }

        private PathNode GetNode(Vector2Int pos)
        {
            return nodes.Find(x => x.pos == pos);
        }

        private bool HasFigureOnBoardPos(Vector2Int pos)
        {
            return GetFigure(pos) != null;
        }

        private Figure GetFigure(Vector2Int pos)
        {
            return board.GetIndex(pos.x, pos.y).Figure;
        }
        
        public BoardIndex GetBoardIndex(Vector2Int pos)
        {
            return board.GetIndex(pos.x, pos.y);
        }
        
        private void UpdateCoastsVisual()
        {
            foreach (PathNode node in nodes)
                GetBoardIndex(node.pos).SetText(node.cost.ToString());
        }
        
        private void VisualizePath(List<PathNode> reachable, List<PathNode> explored, PathNode node)
        {
            foreach (var pathNode in explored)
                board.GetIndex(pathNode.pos.x, pathNode.pos.y).SetColor(Color.red);

            foreach (var pathNode in reachable)
                board.GetIndex(pathNode.pos.x, pathNode.pos.y).SetColor(Color.blue);

            board.GetIndex(node.pos.x, node.pos.y).SetColor(Color.green);
        }
        
        

        
        public List<Vector2Int> GetPath(Move move)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            var moveLength = move.GetMoveDirection();

            Vector2Int pos = move.OldPosition;
            
            while (pos != move.NewPosition)
            {
                pos -= moveLength;
                
                path.Add(pos);
            }

            return path;
        }
    }
}