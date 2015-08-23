﻿using Assets.Scripts.Mob.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 200f;
    private float rotateSpeed = 1200f;

    private IStrategy movementStrategy;
    private IStrategy nextStrategy;

    public event EventHandler DiedEvent;

    void Update()
    {
        Health health = gameObject.GetComponentInChildren<Health>();
        if (!health.IsAlive())
        {
            //Create animation
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        if (movementStrategy != null)
        {
            movementStrategy.Run();
        }
    }

    public void SetGameObjectToFollow(GameObject go)
    {
        movementStrategy = new EnemyFollowStrategy(this, go);
    }

    public void SetPathToFollow(List<Vector2> path)
    {
        var strategy = new EnemyPathStrategy(this, path);
        strategy.OnPathFinished += (object sender, EventArgs args) => {
            if(nextStrategy != null)
            {
                movementStrategy = nextStrategy;
                nextStrategy = null;
            }
            else
            {
                Destroy(this.gameObject);
            }
        };
        movementStrategy = strategy;
    }

    public void SetNextStrategyPeopleAttack(Func<List<NPC>> getListOfNPC)
    {
        nextStrategy = new EnemyAttackPeopleStrategy(this, getListOfNPC);
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetRotateSpeed()
    {
        return rotateSpeed;
    }

}
