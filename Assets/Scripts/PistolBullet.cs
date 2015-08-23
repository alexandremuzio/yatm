using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour {

    [SerializeField]
    float _angle;
    [SerializeField]
    float _speed;

    public static PistolBullet Create(Vector2 position, float angle)
    {
        var bulletPrefab = Resources.Load<PistolBullet>("Prefabs/bullet");
        var bullet = Instantiate(bulletPrefab);
        bullet._angle = angle;
        bullet.transform.position = position;
        return bullet;
    }

    void Start () {
	    
	}
	
	void Update () {
        transform.position += (Vector3) new Vector2(Mathf.Cos(Mathf.PI*_angle/180),
                                                    Mathf.Sin(Mathf.PI*_angle/180)) * _speed;
	}

    [ContextMenu("SpawnBullet")]
    void SpawnBullet()
    {
        Create(transform.position, _angle + 0.5f);
    }
}
