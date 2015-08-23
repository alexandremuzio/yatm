using System;
using UnityEngine;

class XBoxJoystickControl : IControl
{
    private static int indexCounter = 0;

    private IControllable controllable;
    private int index;
    private float deadzone;

    private XBoxJoystickControl(int index, float deadzone)
    {
        this.index = index;
        this.deadzone = deadzone;
    }

    public static XBoxJoystickControl GetControl()
    {
        var joysticks = Input.GetJoystickNames();
        if ((indexCounter + 1) > joysticks.Length)
            return null;

        return new XBoxJoystickControl(++indexCounter, 0.3f);
    }

    public static void Reset()
    {
        indexCounter = 0;
    }

    public void SetControllable(IControllable controllable)
    {
        this.controllable = controllable;
    }

    public void Update()
    {
        if(controllable == null)
        {
            throw new NullReferenceException();
        }

        // Left Stick for movement
        float x = Input.GetAxisRaw("Joystick" + index + "XAxis");
        float y = Input.GetAxisRaw("Joystick" + index + "YAxis");

        if (x * x + y * y > deadzone)
        {
            controllable.MoveOnDir(new Vector2(x, y).normalized);
        }

        // Right Stick for aiming
        x = Input.GetAxisRaw("Joystick" + index + "AimXAxis");
        y = Input.GetAxisRaw("Joystick" + index + "AimYAxis");

        if (x * x + y * y > deadzone)
        {
            controllable.LookAtDir(new Vector2(x, y).normalized);
        }

        if(Input.GetButton("Joystick" + index + "Fire0"))
        {
            controllable.ActionFire0();
        }
    }
}