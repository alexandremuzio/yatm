using UnityEngine;
using System.Collections;
using System;

public class MonstahPlayer : Player
{
    Animator anim;

    [SerializeField]
    private static float defaultMaxHealth = 300;
    
    public IWeapon ExtraWeapon { get; private set; }

    [SerializeField]
    private static float timeAliveConstant = 0.3f;

    public static MonstahPlayer Create(Vector3 initialPos, Player player)
    {
        var playerPrefab = Resources.Load<MonstahPlayer>("Prefabs/MonstahPlayer");

        var mPlayer = Instantiate<MonstahPlayer>(playerPrefab);
        mPlayer.transform.position = initialPos;

        mPlayer.GetComponentInChildren<Health>().SetMaxHealth(defaultMaxHealth * (player.TimeAlive() * timeAliveConstant));
        mPlayer.GetComponentInChildren<Health>().InitialHealth = (defaultMaxHealth * (player.TimeAlive() * timeAliveConstant) * player.GetComponentInChildren<Health>().GetLifeRatio());


        mPlayer._isMonster = true;
        return mPlayer;
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
