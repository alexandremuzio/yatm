using System;
using UnityEngine;
using UnityEngine.UI;


class FireballCaster : MonoBehaviour, IWeapon
{
    public float maxMana = 10;
    public float castCooldownTime = 1f;

    private float timer;

    [SerializeField]
    private float _mana;

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

    public void AddAmmo(int ammo)
    {
        throw new NotImplementedException();
    }

    public void Shoot()
    {
        if (_mana <= 0) return;

        if (timer < castCooldownTime) return;

        //Debug.Log("Calling Shoot!");
        RaycastBullet.Create(transform.position, new Vector2((float)Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180),
                                                    (float)Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180)));

        _mana -= 1;
        timer = 0.0f;
    }
}

