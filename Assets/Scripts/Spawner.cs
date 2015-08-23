using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using Assets.Scripts;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime = 4;

    private Vector2 spawnPosition;

    private float timer = 0;

    private MobManager mobManager;
    
    void Start()
    {
        mobManager = GameObject.Find("Managers").GetComponent<MobManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            Spawn();
            timer -= spawnTime;
        }
    }   
 
    void Spawn()
    {
        mobManager.Spawn(transform);
    }
}
