using UI.Base.WindowConfigs;

namespace Core.GameResult
{
    public abstract class GameResultHandler
    {
        public abstract void Handle(BaseGameResult result);
    }

    public class CornersResultHandler : GameResultHandler
    {
        public override void Handle(BaseGameResult result)
        {
            var gameResult = (CornersGameResult) result;

            UIManager.Instance().OpenWindow(UIManager.UIWindows.GameOver, new UIGameOverWindowConfig(gameResult.Winner));
        }
    }
}