using UnityEngine;
using System;

[Serializable]
public class PlayerControl : IControl
{
    private int _index;
    [SerializeField]
    private float dead_zone = 0.3f;

    public PlayerControl (int index)
    {
        _index = index;
    }

    public Vector2 GetDirection()
    {
        float x = Input.GetAxisRaw("Joystick" + _index + "XAxis");
        float y = Input.GetAxisRaw("Joystick" + _index + "YAxis");

        //Debug.Log("Joystick" + _index + "XAxis");
        //Debug.Log("Joystick" + _index + "YAxis");

        Debug.Log("index " + _index + ": " + x + " " + y);

        if (x*x + y*y < dead_zone)
        {
            return Vector2.zero;
        }

        return new Vector2(x, y).normalized;
    }

    public bool GetShoot()
    {
        return Input.GetButton("Joystick" + _index + "Fire0");
    }

    public Vector2 GetAim()
    {
        float x = Input.GetAxisRaw("Joystick" + _index + "AimXAxis");
        float y = Input.GetAxisRaw("Joystick" + _index + "AimYAxis");

        if (x * x + y * y < dead_zone)
        {
            return Vector2.zero;
        }

        return new Vector2(x, y).normalized;
    }
}
