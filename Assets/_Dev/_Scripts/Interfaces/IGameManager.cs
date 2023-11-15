using System;
using Game.Managers;

namespace Game.Interfaces
{
    public interface IGameManager
    {
        event Action<GameState> OnBeforeStateChanged;
        event Action<GameState> OnAfterStateChanged;
        event Action OnLevelCompleted;
        GameState State { get; }
        void ChangeState(GameState newState);
        int GetLevel();
    }
}
