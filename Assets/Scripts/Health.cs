using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
 
    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;
    public float GetLifeRatio()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        return currentHealth / maxHealth;
    }

    private SpriteRenderer healthImage;
    private SpriteRenderer healthBar;

    [SerializeField]
    private float offset = -300;



	// Use this for initialization
	void Start () 
    {
        currentHealth = maxHealth;
        healthBar = transform.Find("lifeBar").GetComponent<SpriteRenderer>();
        healthImage = transform.Find("lifeImage").GetComponent<SpriteRenderer>();      
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (transform.rotation != Quaternion.identity)
            transform.rotation = Quaternion.identity;
        transform.position = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y + offset);

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
