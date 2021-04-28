using System;
using System.Collections.Generic;
using System.Data;
using Core.Data;
using Core.GameResult;
using Gameplay.Rules;
using UnityEngine;

namespace Gameplay.Scriptable
{
    
    [CreateAssetMenu(fileName = "New gamemode", menuName = "Gamemode/New gamemode")]
    public class GameRuleData : ScriptableObject
    {
        [SerializeField] private string title;
        [TextArea]
        [SerializeField] private string discription;

        [SerializeField] private Rules ruleType;
        
        public string Title => title;

        public string Discription => discription;

        public Rules RuleType => ruleType;
        
        public enum Rules
        {
            CornersDiagonal,
            CornersHorizontalAndVertical,
            CornersNoJump,
        }

        public BaseRule GetRule()
        {
            if (! AvailableRules.ContainsKey(ruleType))
            {
                Debug.LogWarning($"Rule `{ruleType}` not assigned");
                throw new Exception($"Rule `{ruleType}` not assigned");
            }
            
            return AvailableRules[ruleType];
        }

        
        private static Dictionary<Rules, BaseRule> AvailableRules = new Dictionary<Rules, BaseRule>
        {
            { Rules.CornersDiagonal, new CornersDiagonal() },
            { Rules.CornersHorizontalAndVertical, new CornersHorizontalAndVertical() },
            { Rules.CornersNoJump, new CornersNoJump() },
        };
        
        public GameResultHandler GetGameResultHandler()
        {
            switch (ruleType)
            {
                case Rules.CornersDiagonal:
                case Rules.CornersNoJump:
                case Rules.CornersHorizontalAndVertical:
                    return new CornersResultHandler();
            }
            throw new Exception($"Result handler not implemented for rule: {ruleType}");
        }
        
    }
}