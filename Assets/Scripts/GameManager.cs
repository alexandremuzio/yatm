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

	void Start () {

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
	}

    void Update()
    {
        foreach(IControl c in controllers)
        {
            c.Update();
        }
    }
}
