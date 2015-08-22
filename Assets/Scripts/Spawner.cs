using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private Spawnable spawned;

    [SerializeField]
    private bool ShowSpawner = false;

    private Vector2 spawnPosition;

    void Start()
    {
        var cc = gameObject.AddComponent<CircleCollider2D>();
        cc.isTrigger = true;
        spawnPosition = cc.offset;
        Destroy(cc);

        spawned.Spawn(spawnPosition);
    }
    
	void Update () 
    {
        var mr = GetComponent<MeshRenderer>();
        mr.enabled = ShowSpawner;             
 
	}

    [ContextMenu("Stop!")]
    void StopVibrate()
    {
        GamePad.SetVibration(0, 0f, 0f);
    }

    [ContextMenu("Super Vibrate!")]
    IEnumerator SVibrate()
    {
        yield return StartCoroutine("VibrationRoutine");
    }

}
