﻿using UnityEngine;
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

    bool nowSecondRound = false;

    int onePlayerPoint = 0;
    int towPlayerPoint = 0;

    [SerializeField]
    Text timetText = null;
    [SerializeField]
    Text onwScoreText = null;
    [SerializeField]
    Text towScoreText = null;






    void Start()
    {
        //   stopTime = true;
        stopTime = false;

        totalPoint = 0;
        limitTime = StartLimitTime;
        nowSecondRound = false;
        onePlayerPoint = 0;
        towPlayerPoint = 0;
    }

    void Update()
    {
        UpdateTime();
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
        if(nowSecondRound==false)
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
        // SceneManager.LoadScene("Result");
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
    }
}