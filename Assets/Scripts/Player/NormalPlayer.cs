using UnityEngine;
using System;

public class NormalPlayer : Player
{
    private SelfAttacker selfAttacker;

    public static NormalPlayer Create(Vector3 initialPos, bool isMonster)
    {
        var playerPrefab = Resources.Load<NormalPlayer>("Prefabs/Player");

        var player = Instantiate<NormalPlayer>(playerPrefab);
        player.transform.position = initialPos;

        player._isMonster = isMonster;

        return player;
    }

    new void Start()
    {
        base.Start();

        selfAttacker = new SelfAttacker(Time.time);
        Weapon = MachineGun.Create(this);
        //Weapon = FireballCaster.Create(this);
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
