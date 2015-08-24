using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Scythe : MonoBehaviour, IWeapon
{
    public static Scythe Create(Player player)
    {
        GameObject gunObject = player.transform.FindChild("Armpit/ScythePosition").gameObject;
        Scythe scythe = gunObject.AddComponent<Scythe>();       

        return scythe;
    }

    [SerializeField]
    bool active = false;
    
    public void Shoot()
    {
        active = !active;
    }

    public void AddAmmo(int ammo)
    {       
        
    }

    [SerializeField]
    private float damage = 25;
    //BUG:
    //This is actually half the damage on the player because the player contains 2 colliders


    void OnTriggerEnter2D(Collider2D other)
    {        
        if (!active) return;

        Debug.Log(other);

        var otherParent = other.transform.parent;

        Health health = otherParent.gameObject.GetComponentInChildren<Health>();

        if (health != null)
        {
            health.Damage(damage);            
        }
    }
}
