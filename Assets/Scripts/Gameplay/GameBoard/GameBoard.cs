using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base;
using Core.Data;
using Gameplay.Rules;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public class GameBoard : BaseGameBoard
    {
        [SerializeField] private int wight = 7;
        [SerializeField] private int higth = 7;

        [SerializeField] private Vector2 indexSize;
        [SerializeField] private bool moveBoardToCenter = true;

        [SerializeField] private BoardIndex boardIndexPrefab;
        [SerializeField] private GameObject indicesHolder;
        [SerializeField] private GameObject figuresPool = default;
        
        [SerializeField] private Color whiteIndexColor;
        [SerializeField] private Color blackIndexColor;
        
        private List<Figure> figures = new List<Figure>(); 
        private List<IndexOnBoard> indicesOnBoard = new List<IndexOnBoard>();

        public override Figure SeedFigure(Figure figure, Vector2Int pos)
        {
            Figure newFigure = Instantiate(figure, figuresPool.transform);

            var index = GetIndex(pos.y, pos.x);

            figures.Add(newFigure);
            
            index.SetFigure(newFigure);

            return newFigure;
        }
        
        public override void BuildBoard()
        {
            indicesOnBoard = new List<IndexOnBoard>();
            
            for (int y = 0; y < higth; y++)
                for (int x = 0; x < wight; x++)
                    indicesOnBoard.Add(new IndexOnBoard(CreateIndex(x, y), new Vector2Int(x, y)));
        }

        private BoardIndex CreateIndex(int indexX, int indexY)
        {
            BoardIndex index = Instantiate(boardIndexPrefab, indicesHolder.transform);

            index.SetSize(indexSize);
            index.SetColor(GetColorByIndex(indexX, indexY));
            index.transform.localPosition = CalculateIndexPosition(indexX, indexY);
            
            if (moveBoardToCenter)
                ShiftIndexPosition(index.transform);
            
            index.onClick += boardIndex => OnIndexClickInternal(boardIndex, new Vector2Int(indexX, indexY));
            
            return index;
        }

        private void OnIndexClickInternal(BoardIndex boardIndex, Vector2Int pos)
        {
            OnIndexClick?.Invoke(boardIndex, pos);
        }
        
        private Color GetColorByIndex(int indexX, int indexY)
        {
            return (indexY + (indexX + 1)) % 2 == 1 ? whiteIndexColor : blackIndexColor;
        }
        
        private Vector2 CalculateIndexPosition(int indexX, int indexY)
        {
            var posX = (indexSize.x * indexX);
            var posY = (indexSize.y * indexY);
            
            return new Vector3(posX, posY);
        }

        private void ShiftIndexPosition(Transform indexTransform)
        {
            var pos = indexTransform.localPosition;

            var xSubstract = (wight % 2 == 0) ? indexSize.x / 2 : 0;
            var ySubstract = (higth % 2 == 0) ? indexSize.y / 2 : 0;
            
            pos.x -= (indexSize.x * (wight / 2) - xSubstract);
            pos.y -= (indexSize.y * (higth / 2) - ySubstract);
            
            indexTransform.localPosition = pos;
        }
        
        public override void ChangeFigPosition(Figure moveFigure, Vector2Int oldPos, Vector2Int moveNewPosition)
        {
            var index = GetIndex(moveNewPosition.x, moveNewPosition.y);
            var oldIndex = GetIndex(oldPos.x, oldPos.y);

            oldIndex.RemoveFigure();
            
            index.SetFigure(moveFigure);
        }
        
        public override BoardIndex GetIndex(int x, int y)
        {
            return indicesOnBoard.Find(elem => elem.pos == new Vector2Int(x, y)).index;
        }

        public override bool HasIndex(int x, int y)
        {
            var index = indicesOnBoard.Find(elem => elem.pos == new Vector2Int(x, y));
            
            return index != null;
        }

        public override Vector2Int GetSize()
        {
            return new Vector2Int(wight, higth);
        }

        public override Vector2Int GetFigurePos(Figure figure)
        {
            var pos = indicesOnBoard.Find(elem => elem.index.Figure == figure).pos;

            return pos;
        }
    }
}