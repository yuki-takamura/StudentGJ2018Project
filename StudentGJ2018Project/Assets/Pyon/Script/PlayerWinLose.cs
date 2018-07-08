using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinLose : MonoBehaviour
{
    [SerializeField]
    Texture2D winImage = null;

    [SerializeField]
    Texture2D loseImage = null;
    
    Texture2D showImage;

    // Use this for initialization
    public void SetResult(bool isWinner)
    {
        if (isWinner)
        {
            showImage = winImage;
        }
        else
        {
            showImage = loseImage;
        }

        GetComponent<RawImage>().texture = showImage;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
