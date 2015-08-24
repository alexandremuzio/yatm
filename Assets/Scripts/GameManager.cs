using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public enum GameState
{
    FirstPhase,
    SecondPhase,
    Paused,
    Ended
}

public class GameStateChangedEventArgs : EventArgs
{
    public GameState newGameState { get; private set; }

    public GameStateChangedEventArgs(GameState newGameState)
    {
        this.newGameState = newGameState;
    }
}

public class GameManager : MonoBehaviour {

    private Player playerPrefab;
    private float unpauseTime;
    private float startTime;

    public float firstPhaseLength = 10f; //in seconds;
    public float secondPhaseLength = 20f;
    public float endedPhaseLength = 30f;

    public List<IControl> controllers;
    public GameState state;
    public GameState lastState;

    private PlayerManager playerManager;
    private BasementManager basementManager;

    public event EventHandler<GameStateChangedEventArgs> GameStateChangedEvent;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        basementManager = GetComponent<BasementManager>();
        basementManager.AllCitizensDiedEvent += OnAllCitiziensDiedEvent;
    }

    private void OnAllCitiziensDiedEvent(object sender, EventArgs e)
    {
        Debug.Log("ALLL CITIZEEENS DIED MODAFUCKA");
        ChangeState(GameState.Ended);
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

        ChangeState(GameState.FirstPhase);
        startTime = Time.time;
    }

    private void OnPauseRequestEvent(object sender, EventArgs e)
    {
        if (Time.realtimeSinceStartup < unpauseTime) return;

        if (state == GameState.Paused)
        {
            Time.timeScale = 1.0f;
            state = lastState;
            ChangeState(GameState.Paused);
        }

        else
        {
            Time.timeScale = 0.0f;
            lastState = state;
            ChangeState(GameState.Paused);
        }
        unpauseTime = Time.realtimeSinceStartup + 0.5f;
        Debug.Log("Pause requested");

    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    private void ChangeState(GameState newState)
    {
        this.state = newState;
        if(GameStateChangedEvent != null)
        {
            GameStateChangedEvent(this, new GameStateChangedEventArgs(newState));
        }
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
                    StartCoroutine("ShowText");
                    ChangeState(GameState.SecondPhase);
                }
                break;

            case GameState.SecondPhase:
                if (Time.time > secondPhaseLength)
                {
                    
                    ChangeState(GameState.Ended);
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

    IEnumerator ShowText()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/HUD/MonstahTimeText");
        GameObject go = Instantiate(prefab);
        yield return new WaitForSeconds(2.0f);

        StartCoroutine("Fade", go);
    }

    IEnumerator Fade(GameObject go)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(go);
    }
}
