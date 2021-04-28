using UnityEngine;

namespace Gameplay.Rules
{
    public abstract class BaseRule
    {
        public abstract bool IsMoveAllowed(Move move);
    }
}