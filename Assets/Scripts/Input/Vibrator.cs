using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Vibrator1 : MonoBehaviour {

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(0, 0.33f, 0.33f);
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(0, 0f, 0f);
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(0, 0.67f, 0.67f);
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(0, 0f, 0f);
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(0, 1f, 1f);
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(0, 0f, 0f);
    }
    IEnumerator Start()
    {
        print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine("WaitAndPrint");
        print("Done " + Time.time);
    }
}
