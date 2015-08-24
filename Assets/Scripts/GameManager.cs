using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using XInputDotNetPure;

using Random = UnityEngine.Random;

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
    private float timer;

    public float firstPhaseLength = 10f; //in seconds;
    public float secondPhaseLength = 20f;
    public float endedPhaseLength = 30f;

    public List<IControl> controllers;
    public GameState state;
    public GameState lastState;

    private PlayerManager playerManager;
    private BasementManager basementManager;
    private IControl monsterControl;

    public event EventHandler<GameStateChangedEventArgs> GameStateChangedEvent;

    public AudioSource screamAudio;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerManager.AllPlayersDiedEvent += OnAllPlayersDiedEvent;
        playerManager.MonsterDiedEvent += OnMonsterDiedEvent;
        basementManager = GetComponent<BasementManager>();

        var audios = gameObject.GetComponents<AudioSource>();

        screamAudio = audios[0];

        basementManager.AllCitizensDiedEvent += OnAllCitiziensDiedEvent;
    }

    //event handlers
    private void OnMonsterDiedEvent(object sender, EventArgs e)
    {
        Debug.Log("The monster has died");
        StartCoroutine("ShowPlayersWin");
        ChangeState(GameState.SecondPhase);
    }

    private void OnAllPlayersDiedEvent(object sender, EventArgs e)
    {
        Debug.Log("The players have died");
        StartCoroutine("ShowMonstahWins");
        ChangeState(GameState.SecondPhase);
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

        //Monster logic
        int numberOfControllers = Input.GetJoystickNames().Length;
        int monsterIndex = Random.RandomRange(0, numberOfControllers);
        Debug.Log("The monsta is:" + monsterIndex);

        for(int i = 0; i < 4 && !noControlAvailable; i++)
        {
            var control = XBoxJoystickControl.GetControl();
            if (control != null)
            {
                var player = playerManager.CreatePlayer(i == monsterIndex);
                if (player != null)
                {
                    if (i == monsterIndex)
                    {
                        monsterControl = control;
                        StartCoroutine(Vibrator.BadVibrations((PlayerIndex)i));
                    }

                    else
                    {
                        StartCoroutine(Vibrator.GoodVibrations((PlayerIndex)i));
                    }
                        

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
        StartCoroutine("ShowStart");
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

    public void ChangeState(GameState newState)
    {
        this.state = newState;
        if(GameStateChangedEvent != null)
        {
            GameStateChangedEvent(this, new GameStateChangedEventArgs(newState));
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
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
                if (timer > firstPhaseLength)
                {
                    MonstahTime();
                }
                break;

            case GameState.SecondPhase:
                if (timer > secondPhaseLength)
                {
                    ChangeState(GameState.Ended);
                }
                break;

            case GameState.Ended:
                if (timer > endedPhaseLength)
                {
                    
                    //ending conditions here
                    //StartCoroutine(ShowEndTransition());
                    Application.LoadLevel(0);
                }
                break;
        }
    }

    public void MonstahTime()
    {
        StartCoroutine("ShowMonstahTime");
        TransformPlayerIntoMonster();
        screamAudio.Play();
        ChangeState(GameState.SecondPhase);
    }

    public void TransformPlayerIntoMonster()
    {
        var players = playerManager.GetPlayerList();
        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            if (!player.IsMonster()) continue;

            var initialPos = player.transform.position;

            monsterControl.SetControllable(MonstahPlayer.Create(initialPos, player));

            players.Remove(player);
            Destroy(player.gameObject);
            return;
        }
    }


    //ui related
    IEnumerator ShowStart()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/HUD/fearText");
        GameObject go = Instantiate(prefab);
        yield return new WaitForSeconds(4.0f);

        StartCoroutine("Fade", go);
    }

    public IEnumerator ShowMonstahTime()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/HUD/MonstahTimeText");
        GameObject go = Instantiate(prefab);
        yield return new WaitForSeconds(2.0f);

        StartCoroutine("Fade", go);
    }

    IEnumerator ShowPlayersWin()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/HUD/PlayersWin");
        GameObject go = Instantiate(prefab);
        yield return new WaitForSeconds(5.0f);

        StartCoroutine("Fade", go);
    }

    IEnumerator ShowMonstahWins()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/HUD/MonstahWins");
        GameObject go = Instantiate(prefab);
        yield return new WaitForSeconds(5.0f);

        StartCoroutine("Fade", go);
    }

    //IEnumerator ShowEndTransition()
    //{
    //    GameObject go = GameObject.Find("transition");
    //    yield return new WaitForSeconds(5.0f);

    //    StartCoroutine("Fade", go);
    //}

    IEnumerator Fade(GameObject go)
    {
        for(int i = 0; i < 30; i++)
        {
            var components = go.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in components)
            {
                renderer.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), i / 30.0f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(go);
    }
}
