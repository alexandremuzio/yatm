using UnityEngine;
using System.Collections;
using System;

public class NPC : MonoBehaviour
{
    private float speed = 150f;
    private float rotateSpeed = 1200f;

    private IStrategy movementStrategy;

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
