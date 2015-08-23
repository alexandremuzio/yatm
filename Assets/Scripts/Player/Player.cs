﻿using System;
using UnityEngine;

public class Player : MonoBehaviour, IControllable
{
    Rigidbody2D rb2d;

    public float rotateSpeed = 1000f;
    public float playerSpeed = 5f;

    private Vector2 moveToDir;
    private Vector2 lookAtDir;
    private SelfAttacker attacker;

    public IWeapon Weapon { get; private set; }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        attacker = new SelfAttacker(Time.time);
        Weapon = MachineGun.Create(this);
    }

    void FixedUpdate()
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
        this.moveToDir = dir;
    }

    public void LookAtDir(Vector2 dir)
    {
        this.lookAtDir = dir;
    }

    public void ActionFire0(GameState state)
    {
        if (state == GameState.Paused) return;

        Weapon.Shoot();
    }

    public void ActionFire1(GameState state)
    {
        if (state == GameState.Paused) return;
        
        if (attacker.CanHurtSelf())
        {
            Health health = gameObject.GetComponentInChildren<Health>();
            health.Damage(attacker.Damage);
        }
    }

    [SerializeField]
    bool _isMonster = false;

    internal bool IsMonster()
    {
        return _isMonster;
    }
}
