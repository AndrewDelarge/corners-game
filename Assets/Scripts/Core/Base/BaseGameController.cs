using System;
using CodeLib;
using Core.GameResult;
using Gameplay.Rules;

namespace Core.Base
{
    public abstract class BaseGameController : Singleton<BaseGameController>
    {
        public abstract void StartGame();
        public abstract void EndGame();
        public abstract void InterruptGame();
        public abstract void SetData(DataLoaderResult config);
        
        public Action onGameStart { get; set; }
        
        public Action onGameEnd { get; set; }
        public Action<BaseGameResult> onGameOver { get; set; }
        public abstract void SetRule(BaseRule rule);
        public abstract float GetGameTime();
    }
}