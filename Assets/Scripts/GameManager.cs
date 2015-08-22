using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private Player playerPrefab;

    public List<IControl> controllers;
    
    public GameManager()
    {
        controllers = new List<IControl>();
    }

	void Start ()
    {
        var playerPrefab = Resources.Load<Player>("Prefabs/Player");
        var player = Instantiate<Player>(playerPrefab);

        /// FOR TESTING PURPOSES, SET A CONTROL HERE;
        var control = XBoxJoystickControl.GetControl();
        if(control != null)
        {
            control.SetControllable(player);
            controllers.Add(control);
        }
        else
        {
            throw new MissingReferenceException("No control found.");
        }

        var enemyPrefab = Resources.Load<Enemy>("Prefabs/Enemy");
        var enemy = Instantiate<Enemy>(enemyPrefab);
        enemy.SetGameObjectToFollow(player.gameObject);

        var enemy2 = Instantiate<Enemy>(enemyPrefab);
        List<Vector2> path = new List<Vector2>();
        path.Add(new Vector2(3229f, -818f));
        path.Add(new Vector2(3278f, -1656f));
        path.Add(new Vector2(2182f, -2050f));
        enemy.SetPathToFollow(path);
    }

    void Update()
    {
        foreach(IControl c in controllers)
        {
            c.Update();
        }
    }
}
