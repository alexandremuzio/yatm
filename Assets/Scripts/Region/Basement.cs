using UnityEngine;
using System.Collections;
using System;

public class Basement : MonoBehaviour {

    public const float RADIUS = 350f;
    public float GetRadius()
    {
        return RADIUS;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RADIUS);
    }
}
