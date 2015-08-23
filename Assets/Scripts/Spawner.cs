using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using Assets.Scripts;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime = 4;
    private float lastSpawnTime;

    private MobManager mobManager;
    
    void Start()
    {
        mobManager = GameObject.Find("Managers").GetComponent<MobManager>();
        lastSpawnTime = Time.time + UnityEngine.Random.Range(0f, spawnTime);
    }

    void Update()
    {
        if(Time.time - lastSpawnTime > spawnTime)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }   
 
    void Spawn()
    {
        mobManager.Spawn(transform);
    }
}
