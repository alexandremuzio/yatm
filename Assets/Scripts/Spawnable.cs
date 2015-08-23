using UnityEngine;

public class Spawnable : MonoBehaviour
{
    private static GameObject _parent;
    private static GameObject parent
    {
        get
        {
            if(_parent == null)
            {
                _parent = new GameObject("SpawnableParent");
            }
            return _parent;
        }
    }
    
    public Spawnable Spawn(Vector2 position)
    {
        var s = Instantiate<Spawnable>(this);
        s.transform.position = position;
       
        return s;
    }

    void Start()
    {
        transform.parent = parent.transform;
    }
}
