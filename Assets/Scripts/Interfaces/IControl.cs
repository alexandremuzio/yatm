using UnityEngine;
using System;

public interface IControl
{
    event EventHandler PauseRequestEvent;
    void Update(GameState state);
    void SetControllable(IControllable controllable);
}
