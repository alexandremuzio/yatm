using UnityEngine;

class RaycastBullet : MonoBehaviour
{
    private float timer;

    public float range = 1000f;
    public float effectsDisplayTime = 0.05f;

    public float bulletDamage = 25;
    public float damageLoss = 0.4f;
    Vector2 direction;

    LineRenderer gunLine;

    float maxRange;

    public static RaycastBullet Create(Vector2 initialPos, Vector2 initialDirection, float range = 1000)
    {
        var raycastPrefab = Resources.Load<RaycastBullet>("Prefabs/RaycastBullet");
        RaycastBullet bullet = Instantiate(raycastPrefab);

        bullet.transform.position = initialPos;
        bullet.direction = initialDirection;
        bullet.timer = 0;

        bullet.gunLine = bullet.GetComponent<LineRenderer>();
        bullet.gunLine.SetWidth(10f, 10f);
        bullet.gunLine.transform.position += new Vector3(0, 0, -2);

        bullet.range = range;

        return bullet;
    }

    void Start()
    {
        float distanceToTarget = 0;
        var hitDamage = bulletDamage;
        //Debug.DrawRay(transform.position, 1000*direction, Color.red);
        RaycastHit2D hit;
        if (range >= 1000)
            hit = Physics2D.Raycast(transform.position, direction);
        else
        {
            hit = Physics2D.Raycast(transform.position, direction, range);
            hitDamage *= damageLoss; 
        }



        gunLine.SetPosition(0, transform.position);
        
        if (hit.collider != null)
        {
            distanceToTarget = hit.distance;

            gunLine.SetPosition(1, new Vector3(hit.point.x,
                                   hit.point.y,
                                   -2f));

            if (hit.rigidbody == null) return;

            Health health = hit.rigidbody.gameObject.GetComponentInChildren<Health>();

            if (health != null)
            {
                health.Damage(hitDamage);
            }

        }
        else
        {
            gunLine.SetPosition(1, transform.position + (Vector3)direction * range);
        }
    }

    void Update()
    {
        if (timer >= effectsDisplayTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    void DisableEffects()
    {
        gunLine.enabled = false;
    }

    [ContextMenu("SpawnBullet")]
    void SpawnBullet()
    {
        Create(transform.position, Vector3.up);
    }
}
