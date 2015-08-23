using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public void EnableBoolInAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }

    public void DisableBoolInAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", false);
    }

    public void NavigateTo(int scene)
    {
        Application.LoadLevel(scene);
    }
}
