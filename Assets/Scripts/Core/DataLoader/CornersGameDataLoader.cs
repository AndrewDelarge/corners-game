using Core.Base;
using Gameplay;
using Gameplay.GameBoard;
using UnityEngine;

namespace Core.DataLoader
{
    public class CornersGameDataLoader : BaseDataLoader
    {
        private static string GAMEBOARD_PATH = "Gameboards/Default";
        private static string WHITE_FIGURE_PATH = "Figures/DefaultWhite";
        private static string BLACK_FIGURE_PATH = "Figures/DefaultBlack";
        
        public override DataLoaderResult Load()
        {
            var gameBoard = Resources.Load<BaseGameBoard>(GAMEBOARD_PATH);
            var whiteFigure = Resources.Load<Figure>(WHITE_FIGURE_PATH);
            var blackFigure = Resources.Load<Figure>(BLACK_FIGURE_PATH);
            
            
            return new CornersGameDataLoadResult(gameBoard, whiteFigure, blackFigure);
        }
    }
}