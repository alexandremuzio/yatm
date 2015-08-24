using UnityEngine;


class MachineGun : MonoBehaviour, IWeapon
{
    [SerializeField]
    public float bulletCooldownTime = 0.3f;
    public float lastBulletTimer;

    [SerializeField]
    private int maxAmmo = 20;

    [SerializeField]
    private int _ammo;

    AudioSource gunAudio;
    AudioSource gunEmptyAudio;

    public static MachineGun Create(Player player)
    {
        GameObject gunObject = player.transform.FindChild("GunPosition").gameObject;
        MachineGun gun  = gunObject.AddComponent<MachineGun>();

        return gun;
    }

    void Start()
    {
        var audios = gameObject.GetComponents<AudioSource>();
        gunAudio = audios[0];
        gunEmptyAudio = audios[1];
        _ammo = maxAmmo;
        lastBulletTimer = 0f;
    }
    
    public void Shoot()
    {
        if (Time.time - lastBulletTimer < bulletCooldownTime) return;

        if (_ammo <= 0)
        {
            gunEmptyAudio.Play();
            lastBulletTimer = Time.time;
            return;
        }

        //Debug.Log("Calling Shoot!");
        RaycastBullet.Create(transform.position, new Vector2((float)Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180),
                                                    (float)Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180)));

        gunAudio.Play();
        _ammo -= 1;
        lastBulletTimer = Time.time;
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

