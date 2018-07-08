using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : SingletonMonoBehaviour<GameSystem>
{
    float limitTime = 0;

    [SerializeField]
    float StartLimitTime = 60f;


    [SerializeField]
    int goalPoint = 30;

    int totalPoint = 0;

    bool stopTime = false;



    [SerializeField]//一旦
    bool nowSecondRound = false;
    public bool NowSecondRound//いい名前思いつかない
    {
        get
        {
            return nowSecondRound;
        }
    }


    int onePlayerPoint = 0;
    int towPlayerPoint = 0;

    [SerializeField]
    Text timetText = null;
    [SerializeField]
    Text onwScoreText = null;
    [SerializeField]
    Text towScoreText = null;

    [SerializeField]
    Image roundImage = null;
    [SerializeField]
    Sprite secondNumberSprite = null;

    [SerializeField]
    Timer startTime = null;


    void Start()
    {
        stopTime = true;

        totalPoint = 0;
        limitTime = StartLimitTime;
        nowSecondRound = false;
        onePlayerPoint = 0;
        towPlayerPoint = 0;
    }

    void Update()
    {
        if (stopTime == true && nowSecondRound == true)
            CheckInputStart();

        UpdateTime();
    }

    public void CheckInputStart()
    {

        if (Input.GetKeyDown(KeyCode.Joystick2Button0)
             || Input.GetKeyDown(KeyCode.Joystick2Button1)
             || Input.GetKeyDown(KeyCode.Joystick2Button2)
             || Input.GetKeyDown(KeyCode.Joystick2Button3)
          || Input.GetKeyDown(KeyCode.Joystick1Button0)
                      || Input.GetKeyDown(KeyCode.Joystick1Button1)
                      || Input.GetKeyDown(KeyCode.Joystick1Button2)
                      || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            //321をやる
            startTime.StartTime();
        }
    }

    private void UpdateTime()
    {
        if (stopTime == true)
            return;

        limitTime -= Time.deltaTime;

        //小数点見せないので
        int minute = (int)limitTime / 60;
        int seconds = (int)limitTime % 60;

        timetText.text = minute + ":";
        if (seconds < 10)
            timetText.text += "0";


        timetText.text += seconds;


        if (limitTime < 0)
        {
            EndTurn();
        }
    }

    public void Goal()
    {
        totalPoint += goalPoint;
        if (nowSecondRound == false)
        {
            onwScoreText.text = totalPoint.ToString();

        }
        else
        {
            towScoreText.text = totalPoint.ToString();
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("One", onePlayerPoint);
        PlayerPrefs.SetInt("Tow", towPlayerPoint);

        Debug.Log("1p point=" + onePlayerPoint);
        Debug.Log("2p point=" + towPlayerPoint);


        SceneManager.LoadScene("Result");
    }

    private void EndTurn()
    {
        if (nowSecondRound == false)
        {
            ChangePlayer();
        }
        else
        {
            towPlayerPoint = totalPoint;
            GameOver();
        }
    }



    private void ChangePlayer()
    {
        //321のやつ
        //操作交代させる

        stopTime = true;
        onePlayerPoint = totalPoint;
        limitTime = StartLimitTime;
        nowSecondRound = true;
        totalPoint = 0;
        limitTime = StartLimitTime;
        roundImage.sprite = secondNumberSprite;

        HumanManager.Instance.DeleteAll();
        startTime.StopTime();
    }

    public void StartTime()
    {
        stopTime = false;
        HumanManager.Instance.StartGenerate();
    }
}
