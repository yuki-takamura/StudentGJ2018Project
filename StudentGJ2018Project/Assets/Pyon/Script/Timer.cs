using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText = null;

    public float totalTime;
    int seconds;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        seconds = (int)totalTime + 1;
        timerText.text = seconds.ToString();

        if (totalTime <= 0)
            timerText.text = "GO!!!!!!!!!!!";
    }
}