using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private float startTime;

    public List<IControl> controllers;

    public GameManager()
    {
        controllers = new List<IControl>();
    }

	void Start ()
    {
        var playerPrefab = Resources.Load<Player>("Prefabs/Player");
        var player = Instantiate<Player>(playerPrefab);

        /// FOR TESTING PURPOSES, SET A CONTROL HERE;
        var control = XBoxJoystickControl.GetControl();
        if(control != null)
        {
            control.SetControllable(player);
            controllers.Add(control);
        }
        else
        {
            throw new MissingReferenceException("No control found.");
        }

        startTime = Time.time;
    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    void Update()
    {
        foreach(IControl c in controllers)
        {
            c.Update();
        }
    }
}
