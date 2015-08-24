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

    private const float MAX_BARK_TIME = 45f;
    private const float MIN_BARK_TIME = 15f;
    private float barkDelay = 0f;
    private float lastBarkTimer = 0f;

    [SerializeField]
    private ItemBag itemBag;

    private Attacker attacker;

    public event EventHandler DiedEvent;

    AudioSource[] zombieAudios;

    private static GameObject _parent;
    private static GameObject parent
    {
        get
        {
            if (_parent == null)
            {
                _parent = new GameObject("EnemyParent");
            }
            return _parent;
        }
    }

    void Start()
    {
        zombieAudios = gameObject.GetComponents<AudioSource>();

        transform.parent = parent.transform;
        attacker = new Attacker(Time.time);
        itemBag = new ItemBag();
        lastBarkTimer = UnityEngine.Random.Range(MIN_BARK_TIME, MAX_BARK_TIME);
    }

    void Update()
    {
        if (Time.time - lastBarkTimer > barkDelay)
        {
            if(zombieAudios.Length > 0)
            {
                var i = UnityEngine.Random.Range(0, zombieAudios.Length);
                zombieAudios[i].pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                zombieAudios[i].Play();
            }
            barkDelay = UnityEngine.Random.Range(MIN_BARK_TIME, MAX_BARK_TIME);
            lastBarkTimer = Time.time;
        }

        Health health = gameObject.GetComponentInChildren<Health>();
        if (!health.IsAlive())
        {
            itemBag.Open(transform);
            Die();
        }
    }

    public void Die()
    {
        if (DiedEvent != null)
        {
            DiedEvent(this, null);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (movementStrategy != null)
        {
            movementStrategy.Run();
        }
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

    public void SetNextStrategyPeopleAttack(Func<List<NPC>> getListOfNPC, Func<List<Player>> getListOfPlayers)
    {
        nextStrategy = new EnemyAttackPeopleStrategy(this, getListOfNPC, getListOfPlayers);
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

    public void SetFollowStrategy(Func<List<NPC>> getNPCList, Func<List<Player>> getPlayerList)
    {
        movementStrategy = new EnemyFollowStrategy(this, getNPCList, getPlayerList);
    }
}
