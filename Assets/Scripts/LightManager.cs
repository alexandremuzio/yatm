using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    private float currentTime;

    public float dayDuration;
    public GameManager gameManager;

    public Light light;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        dayDuration = gameManager.firstPhaseLength;
    }
	void Start () {
	    var lightPrefab = Resources.Load<Light>("Prefabs/DirectionalLight");
        light = Instantiate<Light>(lightPrefab);
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        if (currentTime >= dayDuration) return;

        Debug.Log(currentTime + "/" + dayDuration);
        light.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 0),
                                                                 new Vector3(90, 0, 0),
                                                                 currentTime/dayDuration));
    }
}
