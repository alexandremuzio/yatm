using System;
using UnityEngine;
using UnityEngine.UI;


class FireballCaster : MonoBehaviour, IWeapon
{
    public int maxMana = 100;
    public float castCooldownTime = 1f;

    private float timer;

    [SerializeField]
    private int _mana;

    public static FireballCaster Create(Player player)
    {
        GameObject casterObject = player.transform.FindChild("GunPosition").gameObject;
        FireballCaster caster = casterObject.AddComponent<FireballCaster>();
        caster.timer = 0.0f;

        return caster;
    }

    void Start()
    {
        _mana = maxMana;
    }

    public void AddAmmo(int mana)
    {
        _mana += mana;
        if (_mana > maxMana)
        {
            _mana = maxMana;
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > castCooldownTime)
        {
            timer = castCooldownTime;
        }
    }

    public void Shoot()
    {
        if (_mana <= 0) return;

        if (timer < castCooldownTime) return;

        //Debug.Log("Calling Shoot!");
        Fireball.Create(transform.position, transform.rotation.eulerAngles.z + 90);

        _mana -= 1;
        timer = 0.0f;
    }


    public int GetAmmoValue()
    {
        return maxMana;
    }


    public int GetMaxAmmoValue()
    {
        return _mana;
    }
}

