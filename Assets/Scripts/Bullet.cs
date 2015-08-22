using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float _angle;
    [SerializeField]
    float _speed;

    public static Bullet Create(Vector2 position, float angle)
    {
        var bulletPrefab = Resources.Load<Bullet>("Prefabs/bullet");
        var bullet = Instantiate(bulletPrefab);
        bullet._angle = angle;
        bullet.transform.position = position;
        return bullet;
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (Vector3) new Vector2(Mathf.Cos(Mathf.PI*_angle/180),
                                                    Mathf.Sin(Mathf.PI*_angle/180)) * _speed;
	}

    [ContextMenu("SpawnBullet")]
    void SpawnBullet()
    {
        Bullet.Create(transform.position, _angle + 0.5f);
    }
}
