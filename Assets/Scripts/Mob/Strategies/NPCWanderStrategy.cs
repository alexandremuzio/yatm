using UnityEngine;

public class NPCWanderStrategy : IStrategy
{
    private const float NEXT_NODE_THRESHOLD = 10.0f;
    private float wanderingTimeout = 0f;

    private bool arrivedAtTarget;

    private NPC npc;
    private Transform wanderingPoint;
    private float wanderingRadius;
    Rigidbody2D rb2d;

    private Vector3 target;

    public NPCWanderStrategy(NPC npc, Transform wanderingPoint, float radius)
    {
        this.npc = npc;
        this.wanderingPoint = wanderingPoint;
        this.wanderingRadius = radius;
        rb2d = npc.GetComponent<Rigidbody2D>();
        wanderingTimeout = Random.Range(0.0f, 9.0f);
        target = GetRandomPositionInsideWanderingArea();
        arrivedAtTarget = true;
    }

    private Vector3 GetRandomPositionInsideWanderingArea()
    {
        var pos = (Vector3)Random.insideUnitCircle * wanderingRadius;
        pos += wanderingPoint.position;
        return pos;
    }

    public void Run()
    {
        if(wanderingTimeout > 8f)
        {
            target = GetRandomPositionInsideWanderingArea();
            arrivedAtTarget = false;
            wanderingTimeout -= 8f;
        }

        if(!arrivedAtTarget)
        {
            var moveToDir = ((Vector3)target - npc.transform.position);

            if (moveToDir.sqrMagnitude < NEXT_NODE_THRESHOLD)
            {
                arrivedAtTarget = true;
            }
            else
            {
                moveToDir = moveToDir.normalized;

                rb2d.MovePosition(new Vector2(npc.transform.position.x + Time.deltaTime * npc.GetSpeed() * moveToDir.x,
                                              npc.transform.position.y + Time.deltaTime * npc.GetSpeed() * moveToDir.y));

                var angle = 0.0f;
                var didAngleChange = false;

                if (moveToDir.sqrMagnitude > 0)
                {
                    angle = Mathf.Atan2(moveToDir.y, moveToDir.x) * Mathf.Rad2Deg;
                    didAngleChange = true;
                }

                if (didAngleChange)
                {
                    var q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    npc.transform.rotation = Quaternion.RotateTowards(npc.transform.rotation, q, npc.GetRotateSpeed() * Time.deltaTime);
                }
            }
        }

        wanderingTimeout += Time.deltaTime;
    }
}
