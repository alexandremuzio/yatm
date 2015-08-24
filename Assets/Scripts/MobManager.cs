using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MobManager : MonoBehaviour {

    float timer = 0.0f;
    List<Transform> spawnPoints;
    List<Vector2> path;

    List<Enemy> enemyList;

    Enemy enemyPrefab;
    GameObject pathPrefab;
    GameObject spawnersPrefab;
    List<GameObject> enemyBodiesPrefabList;

    BasementManager basementManager;
    PlayerManager playerManager;
    GameManager gameManager;

    bool spawnEnabled = false;

    // Use this for initialization
    void Awake ()
    {
        enemyList = new List<Enemy>();
        basementManager = GetComponent<BasementManager>();
        playerManager = GetComponent<PlayerManager>();
        gameManager = GetComponent<GameManager>();

        enemyBodiesPrefabList = new List<GameObject>();

        for(int i = 0; i < 10; i++)
        {
            enemyBodiesPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/EnemyBody_" + i));
        }

        enemyPrefab = Resources.Load<Enemy>("Prefabs/Enemy");
        pathPrefab = Resources.Load<GameObject>("Prefabs/Path0");
        spawnersPrefab = Resources.Load<GameObject>("Prefabs/Spawners");

        spawnPoints = new List<Transform>();
        path = new List<Vector2>();

        var allChildren = pathPrefab.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        foreach (GameObject t in allChildren)
        {
            path.Add(new Vector2(t.transform.position.x, t.transform.position.y));
        }

        allChildren = spawnersPrefab.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();

        foreach (GameObject go in allChildren)
        {
            spawnPoints.Add(go.transform);
        }

        gameManager.GameStateChangedEvent += OnGameStateChangedEvent;
    }

    private void OnGameStateChangedEvent(object sender, GameStateChangedEventArgs e)
    {
        switch (e.newGameState)
        {
            case GameState.FirstPhase:
                spawnEnabled = true;
                break;
            case GameState.Ended:
                spawnEnabled = false;
                KillAllEnemies();
                break;
            default:
                spawnEnabled = false;
                break;
        }
    }

    private void KillAllEnemies()
    {
        var count = enemyList.Count();
        for(int i = count - 1; i >= 0; i--)
        {
            enemyList[i].Die();
        }
    }

    public void Spawn(Transform t)
    {
        if (!spawnEnabled)
            return;

        Enemy e = Instantiate<Enemy>(enemyPrefab);
        e.transform.position = t.position;
        var bodyIndex = UnityEngine.Random.Range(0, enemyBodiesPrefabList.Count);
        var body = Instantiate<GameObject>(enemyBodiesPrefabList[bodyIndex]);
        body.transform.parent = e.transform;
        body.transform.localPosition = body.transform.Find("RotationCenter").transform.position * (-1);

        e.SetFollowStrategy(basementManager.GetNPCList, playerManager.GetPlayerList);
        e.DiedEvent += OnDiedEvent;
        enemyList.Add(e);
    }

    private void OnDiedEvent(object sender, EventArgs e)
    {
        enemyList.Remove(sender as Enemy);
    }
}
