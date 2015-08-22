using UnityEngine;
using System.Collections;

public class PlayerOld
     : MonoBehaviour {

    [SerializeField]
    private PlayerControl control;
    public float speed = 0.05f;
    public float angularSpeed = 0.01f;

	void Start () {
        //index 1 for test only
        control = new PlayerControl(1);
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 direction = control.GetDirection();
        Vector2 aim = control.GetAim();


        transform.position = new Vector3(transform.position.x + direction.x * speed,
                                               transform.position.y + direction.y * speed,
                                               0.0f);

        
	}
}
