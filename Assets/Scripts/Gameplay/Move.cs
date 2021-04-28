
using UnityEngine;

namespace Gameplay
{
    public class Move
    {
        public Player Player { get; }
        public Figure Figure { get; }
        public Vector2Int OldPosition { get; }
        public Vector2Int NewPosition { get; }
        
        public Move(Player player, Figure figure, Vector2Int oldPosition, Vector2Int newPosition)
        {
            Player = player;
            Figure = figure;
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }

        public override string ToString()
        {
            return $"{Figure.Type}: [{OldPosition.y}x{OldPosition.x}]=>[{NewPosition.y}x{NewPosition.x}]";
        }

        public Vector2Int GetMoveDirection()
        {
            Vector2Int result = GetMoveLength();
            
            int GetDirection(int x)
            {
                if (x == 0)
                    return 0;
                return (x > 0) ? 1 : -1;
            }
            
            return new Vector2Int(GetDirection(result.x), GetDirection(result.y));
        }

        public Vector2Int GetMoveLength()
        {
            return OldPosition - NewPosition;
        }
        
    }
}