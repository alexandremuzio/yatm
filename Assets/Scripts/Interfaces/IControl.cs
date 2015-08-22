using UnityEngine;

public interface IControl
{
    void Update();
    void SetControllable(IControllable controllable);
}
