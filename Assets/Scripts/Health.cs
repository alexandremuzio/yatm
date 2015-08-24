using UnityEngine;


public class Health : MonoBehaviour {
 
    [SerializeField]
    private float _maxHealth = 100;

    public float InitialHealth = 0;
    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }

    private float currentHealth;

    public void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }
    public float GetLifeRatio()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }
        return currentHealth / _maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    private SpriteRenderer healthImage;
    private SpriteRenderer healthBar;

    [SerializeField]
    private float offset = -300;

    [SerializeField]
    private bool isVisible = false;

	void Start () 
    {
        if (InitialHealth == 0)
            currentHealth = _maxHealth;
        else
            currentHealth = InitialHealth;

        healthBar = transform.Find("lifeBar").GetComponent<SpriteRenderer>();
        healthImage = transform.Find("lifeImage").GetComponent<SpriteRenderer>();

        healthBar.enabled = isVisible;
        healthImage.enabled = isVisible;
	}
	

	void Update () 
    {
        if (transform.rotation != Quaternion.identity)
            transform.rotation = Quaternion.identity;
        transform.position = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y + offset);

        if(!isVisible)
        {

        }

        healthImage.transform.localScale = new Vector2(GetLifeRatio(), healthImage.transform.localScale.y);
	}

    [ContextMenu("Test Damage!")]
    private void Damage()
    {
        Debug.Log("Damaged: " + 20);
        Damage(20);
    }
    public void Damage(float dmg)
    {
        currentHealth -= dmg;
    }
}
