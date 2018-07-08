using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Result : MonoBehaviour {

    [SerializeField]
    List<Text> scoreText = new List<Text>();

    [SerializeField]
    List<PlayerWinLose> playerWinLoses = new List<PlayerWinLose>();

    // Use this for initialization
    void Start () {
        int onw = PlayerPrefs.GetInt("One");
        int tow = PlayerPrefs.GetInt("Tow");
        if(onw>tow)//1P勝ち
        {
            //負けはもとから
            playerWinLoses[1].SetResult(true);
        }
        else if(onw < tow)
        {
            playerWinLoses[0].SetResult(true);

        }
        scoreText[0].text = onw.ToString();
        scoreText[1].text = tow.ToString();


    }

    // Update is called once per frame
    void Update () {
		
	}
}
