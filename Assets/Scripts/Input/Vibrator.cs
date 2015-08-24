using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Vibrator : MonoBehaviour {

    public static IEnumerator GoodVibrations(PlayerIndex num)
    {
        GamePad.SetVibration(num, 0.3f, 0.3f);
        yield return new WaitForSeconds(5);
        GamePad.SetVibration(num, 0f, 0f);
    }

    public static IEnumerator BadVibrations(PlayerIndex num)
    {

        GamePad.SetVibration(num, 0f, 0f);
        yield return new WaitForSeconds(5);
        GamePad.SetVibration(num, 0f, 0f);
    }
}
