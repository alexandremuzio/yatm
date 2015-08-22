using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerControl control;

    Transform rotationCenter;
    Collider2D terrainCollider;
    Vector2 lastAim;

    float turnRate = 90;
    float playerSpeed = 0.03f;

	// Use this for initialization
	void Start () {
        control = new PlayerControl(1);
        rotationCenter = transform.FindChild("RotationCenter");
        //terrainCollider = transform.FindChild("TerrainCollider").GetComponent(Collider2D;
    }

    void FixedUpdate()
    {
        Vector2 direction = control.GetDirection();
        Vector2 aim = control.GetAim();

        transform.Translate(direction * playerSpeed);

        Debug.DrawLine(rotationCenter.position, aim);

        /*Vector3 dir = rotationCenter.position - (Vector3)aim;
        float angle = Mathf.Atan2(dir.y, dir.x);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnRate);*/

        /*
        float angle = Mathf.Atan2(aim.y, aim.x);
        transform.Rotate(0.0f, 0.0f, angle * 180 / Mathf.PI * angularSpeed);

        if (Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey("down"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey("right")) //Right arrow key to turn right
        {
            transform.RotateAround(rotationCenter.position, Vector3.forward, -turnRate * Time.deltaTime);
        }

        if (Input.GetKey("left"))//Left arrow key to turn left
        {
            transform.RotateAround(rotationCenter.position, Vector3.forward, turnRate * Time.deltaTime);
        }*/
    }
}
