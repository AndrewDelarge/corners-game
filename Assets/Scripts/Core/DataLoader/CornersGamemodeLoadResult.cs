using Core.Base;
using Gameplay.Scriptable;

namespace Core.DataLoader
{
    public class CornersGamemodeLoadResult : DataLoaderResult
    {
        public GameRuleData[] gameRuleDatas { get; private set; }

        public CornersGamemodeLoadResult(GameRuleData[] gameRuleDatas)
        {
            this.gameRuleDatas = gameRuleDatas;
        }
    }
}