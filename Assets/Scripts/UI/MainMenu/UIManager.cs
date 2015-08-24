using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public void EnableButtons(GameObject go)
    {
        foreach(Button b in go.GetComponentsInChildren<Button>() )
        {
            b.enabled = true;
        }
        foreach (Text t in go.GetComponentsInChildren<Text>())
        {
            Debug.Log(t.name);
            foreach (Button b in t.GetComponentsInChildren<Button>())
            {
                b.enabled = true;
            }
        }
    }

    public void DisableButtons(GameObject go)
    {
        foreach (Button b in go.GetComponentsInChildren<Button>())
        {
            b.enabled = false;
        }

        foreach (Text t in go.GetComponentsInChildren<Text>())
        {
            foreach (Button b in t.GetComponentsInChildren<Button>())
            {
                b.enabled = true;
            }
        }
    }
}
