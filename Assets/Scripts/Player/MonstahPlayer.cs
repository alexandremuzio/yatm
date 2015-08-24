using UnityEngine;
using System.Collections;
using System;

public class MonstahPlayer : Player
{
    Animator anim;
    
    public IWeapon ExtraWeapon { get; private set; }


    public static MonstahPlayer Create(Vector3 initialPos)
    {
        var playerPrefab = Resources.Load<MonstahPlayer>("Prefabs/MonstahPlayer");

        var player = Instantiate<MonstahPlayer>(playerPrefab);
        player.transform.position = initialPos;

        player._isMonster = true;
        return player;
    }
    new void Start()
    {
        _isMonster = true;
        anim = GetComponent<Animator>();
        ExtraWeapon = Scythe.Create(this);
        Weapon = FireballCaster.Create(this);

        base.Start();
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
        
        anim.SetBool("Attack", true);                  
    }

    [ContextMenu("Scythe!")]
    public void ScytheA()
    {
        anim.SetBool("Attack", true); 
    }

    public void ScytheAttack()
    {
        ExtraWeapon.Shoot();
    }

    public void AnimationEnd()
    {
        anim.SetBool("Attack", false);
    }
}
