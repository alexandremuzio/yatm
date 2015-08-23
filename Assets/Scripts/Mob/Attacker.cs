using System;
using UnityEngine;

[Serializable]
public class Attacker
{
    [SerializeField]
    private float MaxDamage = 15;

    [SerializeField]
    private float MinDamage = 5;

    [SerializeField]
    private float cooldown = 5;
    private float timer;

    [SerializeField]
    private float lastAttackTime;
    System.Random rnd;
    public Attacker(float currentGameTime)
    {
        lastAttackTime = currentGameTime;
        rnd = new System.Random();
    }

    public void Attack(Collision2D coll)
    {
        timer = Time.time;
        if (timer - lastAttackTime < cooldown) return;

        if(coll.gameObject.tag == "Player")
        {
            Health health = coll.gameObject.GetComponentInChildren<Health>();

            if (health != null)
            {
                if (coll.gameObject.GetComponent<Player>().IsMonster()) return;
                float damage = (float)(rnd.NextDouble() * (MaxDamage - MinDamage)) + MinDamage;
                health.Damage(damage);
                lastAttackTime = Time.time;
            }
        }        
    }
}
