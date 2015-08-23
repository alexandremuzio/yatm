using System;
using UnityEngine;
using UnityEngine.UI;


class FireballCaster : MonoBehaviour, IWeapon
{
    void IWeapon.AddAmmo(int ammo)
    {
        throw new NotImplementedException();
    }

    void IWeapon.Shoot()
    {
        throw new NotImplementedException();
    }
}

