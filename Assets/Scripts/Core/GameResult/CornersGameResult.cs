using Gameplay;

namespace Core.GameResult
{
    public class CornersGameResult : BaseGameResult
    {
        public Player Winner { get; }
        
        public CornersGameResult(Player winner)
        {
            Winner = winner;
        }
    }
}