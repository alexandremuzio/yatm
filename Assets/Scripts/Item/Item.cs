using UnityEngine;
using System.Collections;
using System;

public class Item : MonoBehaviour {

    private static GameObject _parent;
    private static GameObject parent
    {
        get
        {
            if (_parent == null)
            {
                _parent = new GameObject("ItemParent");
            }
            return _parent;
        }
    }
    
    
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
        transform.parent = parent.transform;
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = itemSprite;

        gameObject.AddComponent<BoxCollider2D>();
        var bcoll = gameObject.GetComponent<BoxCollider2D>();
        bcoll.isTrigger = true;

        gameObject.AddComponent<Rigidbody2D>();
        var rbody = gameObject.GetComponent<Rigidbody2D>();

        rbody.drag = startForceDrag;
        rbody.gravityScale = 0;

        var force = new Vector2(UnityEngine.Random.RandomRange(-1, 1), UnityEngine.Random.RandomRange(-1, 1)).normalized * UnityEngine.Random.RandomRange(0, startForceIntensity);
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
        if (other.gameObject.tag != "Player") return;

        var p = other.transform.parent.gameObject.GetComponent<Player>();

        itemAction(p);

        Destroy(this.gameObject);
    }
}
