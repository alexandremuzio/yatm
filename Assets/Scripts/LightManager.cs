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
        light = Instantiate(lightPrefab);
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;

        if (gameManager.state == GameState.SecondPhase)
        {
            light.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            return;
        }

        if (currentTime >= dayDuration) return;

        light.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(10, 0, 0),
                                                                 new Vector3(90, 0, 0),
                                                                 currentTime/dayDuration));
    }
}
