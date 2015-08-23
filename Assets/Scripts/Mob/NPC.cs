using UnityEngine;
using System.Collections;
using System;

public class NPC : MonoBehaviour
{
    private float speed = 150f;
    private float rotateSpeed = 1200f;

    private IStrategy movementStrategy;

    public event EventHandler DiedEvent;

    void Update()
    {
        Health health = gameObject.GetComponentInChildren<Health>();
        if (!health.IsAlive())
        {
            //Create animation

            if (DiedEvent != null)
            {
                DiedEvent(this, null);
            }
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (movementStrategy != null)
        {
            movementStrategy.Run();
        }
    }

    public void SetBasementToWanderAround(Basement basement)
    {
        movementStrategy = new NPCWanderStrategy(this, basement.transform, basement.GetRadius());
    }

    public float GetSpeed()
    {
        return speed;
    }

    internal float GetRotateSpeed()
    {
        return rotateSpeed;
    }
}
