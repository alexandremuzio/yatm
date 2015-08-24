using UnityEngine;
using System;

class Fireball : MonoBehaviour
{
    public float maxMana = 200;
   
    public float aliveTime = 2f;

    private float timer;

    [SerializeField]
    float _angle;

    [SerializeField]
    float _speed;

    public static Fireball Create(Vector2 position, float angle)
    {
        var fireballPrefab = Resources.Load<Fireball>("Prefabs/fireball");
        var fireball = Instantiate(fireballPrefab);
        fireball._angle = angle;
        fireball.transform.position = position;

        fireball.timer = 0.0f;
        return fireball;
    }

    void Update()
    {
        transform.position += (Vector3)new Vector2(Mathf.Cos(Mathf.PI * _angle / 180),
                                                    Mathf.Sin(Mathf.PI * _angle / 180)) * _speed;

        if (timer >= aliveTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    [ContextMenu("SpawnFireball")]
    void SpawnFireball()
    {
        Create(transform.position, _angle + 0.5f);
    }

}
