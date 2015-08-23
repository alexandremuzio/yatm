using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public void Spawn(Vector2 position)
    {
        var c = Instantiate<Spawnable>(this);
        c.transform.position = position;
    }
}
