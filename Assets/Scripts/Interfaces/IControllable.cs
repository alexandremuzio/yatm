using UnityEngine;

public interface IControllable
{
    void MoveOnDir(Vector2 dir);
    void LookAtDir(Vector2 dir);
    void ActionA();
    void ActionB();
}