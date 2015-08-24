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
    }

    public void DisableButtons(GameObject go)
    {
        foreach (Button b in go.GetComponentsInChildren<Button>())
        {
            b.enabled = false;
        }
    }

    public void ChangeFirtSelection(GameObject go)
    {
        var es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.firstSelectedGameObject = go;
    }
}
