using UnityEngine;

class StateManager : MonoBehaviour
{
    public enum GameState
    {
        CoOp,
        MonsterTime,
        Paused,
        Ended
    }

    public GameState state;

    public StateManager()
    {
        state = GameState.CoOp;
    }
}
