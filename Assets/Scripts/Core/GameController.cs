using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base;
using Core.Data;
using Core.DataLoader;
using Core.GameResult;
using Gameplay;
using Gameplay.GameBoard;
using Gameplay.Rules;
using UnityEngine;

namespace Core
{
    public class GameController : BaseGameController
    {
        [SerializeField] private Canvas gameCanvas;
        
        public float StartGameTime { get; private set; }
        public bool GameOver { get; private set; } = true;
        
        private CornersBaseRule rule;
        private BaseGameBoard gameBoard;

        private BaseGameBoard boardTemplate;
        private List<Figure> figureTemplates = new List<Figure>();
        
        private Dictionary<Player, List<Figure>> playerFigures = new Dictionary<Player, List<Figure>>();
        
        private Dictionary<Player, List<Vector2Int>> finishPositions = new Dictionary<Player, List<Vector2Int>>();
        
        private List<Player> players;
        private List<Move> moves = new List<Move>();
        
        private int currentTurnPlayerIndex;
        
        public override void SetData(DataLoaderResult config)
        {
            var loadResult = (CornersGameDataLoadResult) config;

            boardTemplate = loadResult.BaseGameBoard;

            figureTemplates.Add(loadResult.WhiteFigure);
            figureTemplates.Add(loadResult.BlackFigure);
        }

        public override void SetRule(BaseRule newRule)
        {
            rule = (CornersBaseRule) newRule;
        }
        
        public override void StartGame()
        {
            StartGameTime = Time.time;
            
            if (rule == null)
            {
                Debug.Log("[GameController] Game rule not set");
                return;
            }
            
            InstanceBoard();
            
            rule.SetBoard(gameBoard);
            
            InitPlayers();
            
            SeedFiguresOnBoard();

            InitFinishPositions();
            
            PlayersInput.Instance().Init(playerFigures, gameBoard);
            
            PlayersInput.Instance().OnTryDoMove += MakeMove;
            
            GameOver = false;
            
            onGameStart?.Invoke();
        }


        
        
        private void InitFinishPositions()
        {
            finishPositions.Clear();
            
            for (int i = 0; i < players.Count; i++)
                finishPositions.Add(players[i], rule.GetFinishPositions(i));
        }


        private void InitPlayers()
        {
            players = CoreData.GameSettings.Players.ToList();
            currentTurnPlayerIndex = 0;
        }

        private void SeedFiguresOnBoard()
        {
            playerFigures = new Dictionary<Player, List<Figure>>();
            
            for (int i = 0; i < CoreData.GameSettings.Players.Count; i++)
            {
                if (figureTemplates.Count == 0)
                    Debug.LogError("Figures for game not loaded!");

                
                var figuresPositions = rule.GetFigureStartPositions(i);
                var figures = new List<Figure>();
                
                for (int j = 0; j < figuresPositions.Length; j++)
                    figures.Add(gameBoard.SeedFigure(figureTemplates[i], figuresPositions[j].position));
                
                
                playerFigures.Add(CoreData.GameSettings.Players[i], figures.ToList());

                figures.Clear();
            }
        }
        
        private void InstanceBoard()
        {
            gameBoard = Instantiate(boardTemplate, gameCanvas.transform);
            
            gameBoard.BuildBoard();
        }

        public override void InterruptGame()
        {
            onGameOver = null;
            GameOver = true;
            onGameEnd?.Invoke();
        }

        public override void EndGame()
        {
            GameOver = true;
            onGameOver?.Invoke(new CornersGameResult(players[currentTurnPlayerIndex]));
            onGameEnd?.Invoke();
        }

        public void MakeMove(Move move)
        {
            if (! IsPlayerTurn(move.Player))
                return;
            
            if (! rule.IsMoveAllowed(move))
                return;

            
            moves.Add(move);
            
            players[currentTurnPlayerIndex].PlayerDoneMove(move);
            
            gameBoard.ChangeFigPosition(move.Figure, move.OldPosition, move.NewPosition);

            if (IsGameOver())
                EndGame();
            
            NextTurn();
        }

        private bool IsGameOver()
        {
            var figures = playerFigures[players[currentTurnPlayerIndex]];
            
            var currentPositions = new List<Vector2Int>();
            
            foreach (var figure in figures)
                currentPositions.Add(gameBoard.GetFigurePos(figure));

            var pos = currentPositions.Except(finishPositions[players[currentTurnPlayerIndex]]);
            
            return ! pos.Any();
        }


        private void NextTurn()
        {
            currentTurnPlayerIndex++;
            
            if (currentTurnPlayerIndex >= players.Count)
                currentTurnPlayerIndex = 0;
        }
        
        public bool IsPlayerTurn(Player player)
        {
            return player == players[currentTurnPlayerIndex];
        }

        public override float GetGameTime()
        {
            return Time.time - StartGameTime;
        }
    }
}