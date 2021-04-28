using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.GameResult;
using Gameplay;
using Gameplay.Rules;
using Gameplay.Scriptable;
using UnityEngine;

namespace Core.Data
{
    public class GameSettings
    {
        private int DEFAULT_PLAYERS_COUNT = 2;
        private readonly List<GameRuleData> gameRuleData;
        private readonly List<Player> players = new List<Player>();

        public GameSettings(List<GameRuleData> gameRuleData)
        {
            this.gameRuleData = gameRuleData;

            CreateDefaultPlayers();
        }

        private void CreateDefaultPlayers()
        {
            for (int i = 0; i < DEFAULT_PLAYERS_COUNT; i++)
                CreatePlayer($"Player {i + 1}");
        }

        public ReadOnlyCollection<GameRuleData> GameRuleData => gameRuleData.AsReadOnly();
        public ReadOnlyCollection<Player> Players => players.AsReadOnly();

        public GameRuleData CurrentRuleData { get; private set; }
        public BaseRule CurrentRule => CurrentRuleData.GetRule();
        public GameResultHandler GameResultHandler { get; private set; }

        private void CreatePlayer(string name)
        {
            var player = new Player(name);
            
            players.Add(player);
        }

        public void SetRule(GameRuleData rule)
        {
            CurrentRuleData = rule;

            GameResultHandler = rule.GetGameResultHandler();
        }

    }
}