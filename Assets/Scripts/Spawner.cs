using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private Spawnable spawned;

    private Vector2 spawnPosition;

    void Start()
    {
        spawned.Spawn(transform.position);
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
