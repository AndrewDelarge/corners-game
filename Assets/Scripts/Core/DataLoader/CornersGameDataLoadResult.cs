using Core.Base;
using Gameplay;
using Gameplay.GameBoard;

namespace Core.DataLoader
{
    public class CornersGameDataLoadResult : DataLoaderResult
    {
        public BaseGameBoard BaseGameBoard { get; }
        public Figure WhiteFigure { get; }
        public Figure BlackFigure { get; }

        public CornersGameDataLoadResult(BaseGameBoard baseGameBoard, Figure whiteFigure, Figure blackFigure)
        {
            BaseGameBoard = baseGameBoard;
            WhiteFigure = whiteFigure;
            BlackFigure = blackFigure;
        }
    }
}