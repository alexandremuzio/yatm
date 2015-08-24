using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Vibrator : MonoBehaviour {

    IEnumerator GoodVibrations(PlayerIndex num)
    {
        GamePad.SetVibration(num, 0.1f, 0.1f);
        yield return new WaitForSeconds(5);
        GamePad.SetVibration(num, 0f, 0f);
    }

    IEnumerator BadVibrations(PlayerIndex num)
    {
        
        GamePad.SetVibration(num, 0f, 0f);
        yield return new WaitForSeconds(5);
        GamePad.SetVibration(num, 0f, 0f);
    }

    
    [ContextMenu("Vibrate All")]
    void Vibes()
    {
        print("Starting " + Time.time);
        int a = UnityEngine.Random.RandomRange(0, 3);
        if (a == 0) StartCoroutine(BadVibrations(PlayerIndex.One));
        else StartCoroutine(GoodVibrations(PlayerIndex.One));
        if (a == 1) StartCoroutine(BadVibrations(PlayerIndex.Two));
        else StartCoroutine(GoodVibrations(PlayerIndex.Two));
        if (a == 2) StartCoroutine(BadVibrations(PlayerIndex.Three));
        else StartCoroutine(GoodVibrations(PlayerIndex.Three));
    }


    void Start()
    {
        print("Starting " + Time.time);
        int a = UnityEngine.Random.RandomRange(0, 3);
        if(a == 0) StartCoroutine(BadVibrations(PlayerIndex.One));
        else StartCoroutine(GoodVibrations(PlayerIndex.One));
        if (a == 1) StartCoroutine(BadVibrations(PlayerIndex.Two));
        else StartCoroutine(GoodVibrations(PlayerIndex.Two));
        if (a == 2) StartCoroutine(BadVibrations(PlayerIndex.Three));
        else StartCoroutine(GoodVibrations(PlayerIndex.Three));
    }
}
