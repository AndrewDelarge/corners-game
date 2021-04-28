using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GameBoard
{
    public abstract class BaseGameBoard : MonoBehaviour
    {
        public Action<BoardIndex, Vector2Int> OnIndexClick { get; set; }
        public abstract Figure SeedFigure(Figure figure, Vector2Int startPos);
        public abstract void BuildBoard();
        public abstract void ChangeFigPosition(Figure moveFigure, Vector2Int oldPos, Vector2Int moveNewPosition);
        public abstract BoardIndex GetIndex(int x, int y);
        public abstract Vector2Int GetFigurePos(Figure figure);
        public abstract Vector2Int GetSize();
    }
}