using UnityEngine;


class MachineGun : MonoBehaviour, IWeapon
{
    [SerializeField]
    public float bulletCooldownTime = 0.3f;
    public float lastBulletTimer;

    [SerializeField]
    private int maxAmmo = 20;

    private int _ammo;

    [SerializeField]
    private float emptyRange = 25;

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
            RaycastBullet.Create(transform.position, new Vector2((float)Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180),
                                                    (float)Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180)), emptyRange);

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
            maxAmmo = _ammo;
            _ammo = maxAmmo;
        }
    }


    public int GetAmmoValue()
    {
        return _ammo;
    }


    public int GetMaxAmmoValue()
    {
        return maxAmmo;
    }
}

