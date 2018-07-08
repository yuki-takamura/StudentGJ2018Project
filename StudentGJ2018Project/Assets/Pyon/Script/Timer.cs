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

    bool isStart = false;

    // Use this for initialization
    void Start()
    {
        isStart = false;
        totalTime = TotalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == true)
            return;
        totalTime -= Time.deltaTime;
        seconds = (int)totalTime + 1;
        timerText.text = seconds.ToString();

        if (totalTime <= 0 && isStart == false)
        {
            isStart = true;
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
    public void StopTime()
    {
        timerText.text = "何かボタンを押してください";
        timerText.gameObject.SetActive(true);
        totalTime = TotalTime;
    }


    public void StartTime()
    {
        
        isStart = false;
       
    }
}