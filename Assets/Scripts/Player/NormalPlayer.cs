using UnityEngine.UI;
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

    Text ammoText;

    void Awake()
    {
        var TextPrefab = Resources.Load<Text>("Prefabs/HUD/AmmoText");
        ammoText = Instantiate(TextPrefab);
        ammoText.rectTransform.SetParent(GameObject.Find("Canvas").transform);        
    }

    void OnDestroy()
    {
        Destroy(ammoText.gameObject);
    }

    new void Start()
    {
        base.Start();
        selfAttacker = new SelfAttacker(Time.time);
        Weapon = MachineGun.Create(this);

        //Weapon = FireballCaster.Create(this);
    }

    public Vector3 textOffset = new Vector3(0, 100, 0);

    new void Update()
    {
        ammoText.rectTransform.position = transform.position + textOffset;
        ammoText.text = Weapon.GetAmmoValue() + "/" + Weapon.GetMaxAmmoValue();
        base.Update();
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
