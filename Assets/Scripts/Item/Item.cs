using UnityEngine;
using System.Collections;
using System;

public class Item : MonoBehaviour {

    public event Action<Player> itemAction;

    private Sprite itemSprite;

    void Awake()
    {
        itemSprite = Resources.Load<Sprite>("Sprites/Item");
    }

    [SerializeField]
    private float startForceIntensity = 10000;

    [SerializeField]
    private float startForceDrag = 1;

    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = itemSprite;

        gameObject.AddComponent<BoxCollider2D>();
        var bcoll = gameObject.GetComponent<BoxCollider2D>();
        bcoll.isTrigger = true;

        gameObject.AddComponent<Rigidbody2D>();
        var rbody = gameObject.GetComponent<Rigidbody2D>();

        rbody.drag = startForceDrag;

        System.Random rnd = new System.Random();
        var force = new Vector2((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f).normalized * startForceIntensity;
        rbody.AddForce(force);    

    }

    public static Item Create(string name, Transform t, Action<Player> a)
    {
        GameObject itemObject = new GameObject(name + "Item");
        var item = itemObject.AddComponent<Item>();
        itemObject.transform.position = t.position;
        item.itemAction = a;
        return item;
    }    

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag + "Touched item");
        if (other.gameObject.tag != "Player") return;
        Debug.Log("And it was a player!");

        var p = other.transform.parent.gameObject.GetComponent<Player>();

        itemAction(p);

        Destroy(this.gameObject);
    }
}
