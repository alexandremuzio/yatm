using UnityEngine;
using UnityEngine.UI;

public class TimeCounterView : MonoBehaviour
{
    Text lblTimeCounter;

    GameManager gameManager;

    void Start()
    {
        lblTimeCounter = gameObject.GetComponentInChildren<Text>();
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    void Update()
    {
        var guiTime = gameManager.GetElapsedTime();
        lblTimeCounter.text = string.Format("{0:00}:{1:00}",(int)(guiTime/60), (int)guiTime % 60);
    }
}
