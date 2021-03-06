﻿using System;
using UnityEngine;

public abstract class Player : MonoBehaviour, IControllable
{

    public event EventHandler DiedEvent;
    public IWeapon Weapon { get; protected set; }

    public float rotateSpeed = 1000f;
    public float playerSpeed = 5f;

    protected Rigidbody2D rb2d;
    protected Vector2 moveToDir;
    protected Vector2 lookAtDir;

    [SerializeField]
    protected bool _isMonster = false;

    private float startingTime;

    public float TimeAlive()
    {
        return Time.time - startingTime;
    }

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startingTime = Time.time;
    }

    protected void Update()
    {
        Health health = gameObject.GetComponentInChildren<Health>();
        if (!health.IsAlive())
        {
            //Create animation
            if (DiedEvent != null)
            {
                DiedEvent(this, null);
            }
            //TODO - show add animation here
            Destroy(gameObject);
        }
    }

    protected void FixedUpdate()
    {
        rb2d.MovePosition(new Vector2(transform.position.x + Time.deltaTime * playerSpeed * moveToDir.x,
                                      transform.position.y + Time.deltaTime * playerSpeed * moveToDir.y));
        

        var angle = 0.0f;
        var didAngleChange = false;

        if (lookAtDir.sqrMagnitude > 0)
        {
            angle = Mathf.Atan2(lookAtDir.y, lookAtDir.x) * Mathf.Rad2Deg;
            didAngleChange = true;
        }
        else if (moveToDir.sqrMagnitude > 0)
        {
            angle = Mathf.Atan2(moveToDir.y, moveToDir.x) * Mathf.Rad2Deg;
            didAngleChange = true;
        }

        if (didAngleChange)
        {
            var q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
        }

        moveToDir = Vector2.zero;
        lookAtDir = Vector2.zero;   
    }


    public void MoveOnDir(Vector2 dir)
    {
        moveToDir = dir;
    }

    public void LookAtDir(Vector2 dir)
    {
        lookAtDir = dir;
    }

    abstract public void ActionFire0(GameState state);

    abstract public void ActionFire1(GameState state);

    public bool IsMonster()
    {
        return _isMonster;
    }

    public void SayName(string name)
    {
        Debug.Log(name);
    }


    public void ActionFire2(GameState state)
    {
        
        GameManager gm = GameObject.Find("Managers").GetComponent<GameManager>();

        if (state != GameState.FirstPhase) return;
        if (!IsMonster())
        {
            Debug.Log("Player that pressed control is not the monster");
            return;
        }

        gm.MonstahTime();
    }
}
