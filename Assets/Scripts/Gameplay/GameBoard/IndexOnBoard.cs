using UnityEngine;

namespace Gameplay.GameBoard
{
    public class IndexOnBoard
    {
        public BoardIndex index { get; }
        public Vector2Int pos { get; }

        public IndexOnBoard(BoardIndex index, Vector2Int pos)
        {
            this.index = index;
            this.pos = pos;
        }
    }
}