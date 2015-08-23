using UnityEngine;

public interface IControllable
{
    void MoveOnDir(Vector2 dir);
    void LookAtDir(Vector2 dir);
    void ActionFire0(GameState state);
    void ActionFire1(GameState state);
}