using System;

namespace Gameplay
{
    public class Player
    {
        public Action<Move> OnPlayerDoneMove;
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void PlayerDoneMove(Move move)
        {
            OnPlayerDoneMove?.Invoke(move);
        }
    }
}