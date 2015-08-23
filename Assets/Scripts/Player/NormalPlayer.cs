using UnityEngine;
using System;

public class NormalPlayer : Player
{
    private SelfAttacker selfAttacker;

    new void Start()
    {
        base.Start();

        selfAttacker = new SelfAttacker(Time.time);
        Weapon = MachineGun.Create(this);
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

        if (selfAttacker.CanHurtSelf())
        {
            Health health = gameObject.GetComponentInChildren<Health>();
            health.Damage(selfAttacker.Damage);
        }
    }

}
