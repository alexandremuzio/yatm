using Assets.Scripts.Mob.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Mob;

public class Enemy : MonoBehaviour
{
    private float speed = 200f;
    private float rotateSpeed = 1200f;

    private IStrategy movementStrategy;
    private IStrategy nextStrategy;

    private ItemBag itemBag;


    private Attacker attacker;

    public event EventHandler DiedEvent;


    void Start()
    {
        attacker = new Attacker(Time.time);
        itemBag = new ItemBag();
    }

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

    void OnCollisionStay2D(Collision2D coll)
    {
        attacker.Attack(coll);
    }

    void OnDestroy()
    {
        itemBag.Open(transform);
    }

}
