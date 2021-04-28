using Gameplay;

namespace UI.Base.WindowConfigs
{
    public class UIGameOverWindowConfig : WindowConfig
    {
        public Player winner { get; }

        public UIGameOverWindowConfig(Player winner)
        {
            this.winner = winner;
        }
    }
}