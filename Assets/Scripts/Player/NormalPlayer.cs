using UnityEngine;
using System;

public class NormalPlayer : Player
{
    private SelfAttacker attacker;

    new void Start()
    {
        base.Start();
        attacker = new SelfAttacker(Time.time);
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ActionFire0(GameState state)
    {
        if (state == GameState.Paused) return;

        Weapon.Shoot();
    }

    public override void ActionFire1(GameState state)
    {
        if (state == GameState.Paused) return;

        if (attacker.CanHurtSelf())
        {
            Health health = gameObject.GetComponentInChildren<Health>();
            health.Damage(attacker.Damage);
        }
    }

}
