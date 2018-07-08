using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Text timerText = null;

    [SerializeField]
    float TotalTime;

    float totalTime;

    int seconds;

    // Use this for initialization
    void Start()
    {
        totalTime = TotalTime;
    }

    // Update is called once per frame
    void Update()
    {
        TotalTime -= Time.deltaTime;
        seconds = (int)TotalTime + 1;
        timerText.text = seconds.ToString();

        if (TotalTime <= 0)
        {
            timerText.text = "GO!!!!!!!!!!!";
            StartCoroutine(StartGame());
            GameSystem.Instance.StartTime();
        }
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        timerText.gameObject.SetActive(false);
    }

    public void StartTime()
    {
        totalTime = TotalTime;
    }
}