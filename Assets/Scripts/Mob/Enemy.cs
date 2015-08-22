using Assets.Scripts.Mob.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb2d;

    public Player player;
    private float speed = 200f;
    private float rotateSpeed = 1200f;

    private IStrategy movementStrategy;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

    public float GetSpeed()
    {
        return speed;
    }

    public float GetRotateSpeed()
    {
        return rotateSpeed;
    }

    public void SetPathToFollow(List<Vector2> path)
    {
        movementStrategy = new EnemyPathStrategy(this, path);
    }
}
