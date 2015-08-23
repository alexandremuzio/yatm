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
            control.PauseRequestEvent += OnPauseRequestEvent;
        }
        else
        {
            throw new MissingReferenceException("No control found.");
        }

        var enemyPrefab = Resources.Load<Enemy>("Prefabs/Enemy");
        var enemy = Instantiate<Enemy>(enemyPrefab);
        enemy.SetGameObjectToFollow(player.gameObject);

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
