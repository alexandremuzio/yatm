using UnityEngine;
using System;

public class PlayerControl : IControl
{
    private int _index;

    public PlayerControl (int index)
    {
        _index = index;
    }

    public Vector2 GetDirection()
    {
        float x = Input.GetAxisRaw("Joystick" + _index + "XAxis");
        float y = Input.GetAxisRaw("Joystick" + _index + "YAxis");

        return new Vector2(x, y).normalized;
    }

    public bool GetShoot()
    {
        return Input.GetButton("Joystick" + _index + "Fire0");
    }

    public Vector2 GetAim()
    {
        throw new NotImplementedException();
    }
}
