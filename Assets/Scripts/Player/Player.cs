using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerControl control;

    Transform rotationCenter;
    Collider2D terrainCollider;
    Vector2 lastDirection;

    public float playerSpeed = 5f;

	void Start () {
        control = new PlayerControl(1);
        rotationCenter = transform.FindChild("RotationCenter");
    }


    void FixedUpdate()
    {
        Vector2 direction = control.GetDirection();
        Vector2 aim = control.GetAim();

        transform.position = new Vector3(transform.position.x + Time.deltaTime * playerSpeed * direction.x,
                                         transform.position.y + Time.deltaTime * playerSpeed * direction.y,
                                         0.0f);

        float angle = 0.0f;
        if (aim.SqrMagnitude() > 0)
            angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

        else if (aim.SqrMagnitude() == 0 && direction.SqrMagnitude() > 0)
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        else
            angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if (direction != Vector2.zero)
            lastDirection = direction;

        //Debug.DrawRay(rotationCenter.position, aim);
    }
}
