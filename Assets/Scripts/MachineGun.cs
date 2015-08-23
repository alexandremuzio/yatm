using UnityEngine;
using UnityEngine.UI;


class MachineGun : MonoBehaviour, IWeapon
{
    public float timer;
    public float bulletCooldownTime = 0.3f;

    [SerializeField]
    private int maxAmmo = 20;

    [SerializeField]
    private int _ammo;

    public static MachineGun Create(Player player)
    {
        GameObject gunObject = player.transform.FindChild("GunPosition").gameObject;
        MachineGun gun  = gunObject.AddComponent<MachineGun>();
        gun.timer = 0.0f;

        return gun;
    }

    void Start()
    {
        _ammo = maxAmmo;
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
        if (_ammo <= 0) return;

        if (timer < bulletCooldownTime) return;

        RaycastBullet.Create(transform.position, new Vector2((float)Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180),
                                                    (float)Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180)));

        _ammo -= 1;
        timer = 0.0f;
    }

    public void AddAmmo(int ammo)
    {
 	    _ammo += ammo;
        if(_ammo > maxAmmo)
        {
            _ammo = maxAmmo;
        }
    }
}

