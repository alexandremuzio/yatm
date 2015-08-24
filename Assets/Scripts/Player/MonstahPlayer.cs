using UnityEngine;
using System.Collections;
using System;

public class MonstahPlayer : Player
{
    public IWeapon ExtraWeapon { get; private set; }

    new void Start()
    {
        base.Start();
        //ExtraWeapon = 
        //attacker = new SelfAttacker(Time.time);
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

        Health health = gameObject.GetComponentInChildren<Health>();
          
    }
}
