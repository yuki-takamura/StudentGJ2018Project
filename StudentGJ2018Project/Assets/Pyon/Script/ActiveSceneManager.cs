using UnityEngine;
using UnityEngine.UI;

public class ActiveSceneManager : MonoBehaviour
{
    [SerializeField]
    Text text1 = null;

    [SerializeField]
    Text text2 = null;
    
    [SerializeField]
    GameObject child;
	
	// Update is called once per frame
	void Update ()
    {
        if(text1.GetComponent<PlayerEntry>().canPlayGame
            && text2.GetComponent<PlayerEntry>().canPlayGame)
        {
            child.SetActive(true);
        }
    }
}
