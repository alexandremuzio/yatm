using UnityEngine;
using System;

class SelfAttacker
{
    private float timer;

    [SerializeField]
    private float damage = 15;
    public float Damage {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField]
    private float cooldown = 1;

    [SerializeField]
    private float lastAttackTime;

    public SelfAttacker (float currentGameTime)
    {
        lastAttackTime = currentGameTime;
    }


    public bool CanHurtSelf()
    {
        timer = Time.time;
        if (timer - lastAttackTime >= cooldown)
        {
            lastAttackTime = Time.time;
            return true;
        }
        return false;
    }
}
