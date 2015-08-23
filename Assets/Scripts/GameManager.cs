using UnityEngine;
using System.Collections.Generic;
using System;

public enum GameState
{
    FirstPhase,
    SecondPhase,
    Paused,
    Ended
}

public class GameManager : MonoBehaviour {

    private Player playerPrefab;
    private float unpauseTime;
    private float startTime;

    public float firstPhaseLength = 1f; //in seconds;
    public float secondPhaseLength = 10f;
    public float endedPhaseLength = 20f;

    public List<IControl> controllers;
    public GameState state;
    public GameState lastState;

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
                    control.PauseRequestEvent += OnPauseRequestEvent;
                }
            }
            else
            {
                noControlAvailable = true;
            }
        }

        state = GameState.FirstPhase;
        startTime = Time.time;
    }

    private void OnPauseRequestEvent(object sender, EventArgs e)
    {
        if (Time.realtimeSinceStartup < unpauseTime) return;

        if (state == GameState.Paused)
        {
            Time.timeScale = 1.0f;
            state = lastState;
            lastState = GameState.Paused;
        }

        else
        {
            Time.timeScale = 0.0f;
            lastState = state;
            state = GameState.Paused;
        }
        unpauseTime = Time.realtimeSinceStartup + 0.5f;
        Debug.Log("Pause requested");

    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    void Update()
    {
        //if (state == GameState.Paused) return;
        
        foreach (IControl c in controllers)
        {
            c.Update(state);
        }

        switch (state)
        {
            case GameState.FirstPhase:
                if (Time.time > firstPhaseLength)
                {
                    state = GameState.SecondPhase;
                }
                break;

            case GameState.SecondPhase:
                if (Time.time > secondPhaseLength)
                {
                    state = GameState.Ended;
                }
                break;

            case GameState.Ended:
                if (Time.time > endedPhaseLength)
                {
                    //ending conditions here
                }
                break;
        }
    }

}
