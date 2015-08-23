using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private float startTime;

    private List<IControl> controllers;

    private PlayerManager playerManager;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

	void Start ()
    {
        controllers = new List<IControl>();
        
        XBoxJoystickControl.Reset();

        var noControlAvailable = false;

        for(int i = 0; i < 4 && !noControlAvailable; i++)
        {
            var control = XBoxJoystickControl.GetControl();
            if (control != null)
            {
                var player = playerManager.CreatePlayer();
                if (player != null)
                {
                    control.SetControllable(player);
                    controllers.Add(control);
                }
            }
            else
            {
                noControlAvailable = true;
            }
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
