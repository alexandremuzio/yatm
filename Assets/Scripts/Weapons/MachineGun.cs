using UnityEngine;

class MachineGun : MonoBehaviour, IWeapon
{
    public float timer;
    public float bulletCooldownTime = 0.3f;

    public static MachineGun Create(Player player)
    {
        GameObject gunObject = player.transform.FindChild("GunPosition").gameObject;
        MachineGun gun  = gunObject.AddComponent<MachineGun>();
        gun.timer = 0.0f;

        return gun;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > bulletCooldownTime)
        {
            timer = bulletCooldownTime;
        }
    }
    
    public void Shoot()
    {
        if (timer < bulletCooldownTime) return;

        //Debug.Log("Calling Shoot!");
        RaycastBullet.Create(transform.position, new Vector2((float)Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180),
                                                    (float)Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180)));

        timer = 0.0f;
    }

}

