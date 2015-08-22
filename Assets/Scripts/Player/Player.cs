using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Transform rotationCenter;
    Collider2D terrainCollider;

    float turnRate = 90;
    float playerSpeed = 1;

	// Use this for initialization
	void Start () {
        rotationCenter = transform.FindChild("RotationCenter");
        //terrainCollider = transform.FindChild("TerrainCollider").GetComponent(Collider2D;
    }


    void FixedUpdate()
    {
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
        }
    }
}
