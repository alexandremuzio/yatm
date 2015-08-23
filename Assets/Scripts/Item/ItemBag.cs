using UnityEngine;
using System.Collections;
using System;

public class ItemBag : MonoBehaviour {

    //private List<Action<Player>> ItemActions = new List<Action<Player>>();

    [ContextMenu("Create Item")]
    public void CreateItem()
    {
        Item.Create("test", transform, (p) => (p.Weapon.AddAmmo(10)));
    }
}
