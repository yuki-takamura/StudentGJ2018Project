using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : SingletonMonoBehaviour<HumanManager>
{
    [SerializeField]
    GameObject humanObj = null;

    List<GameObject> humans = new List<GameObject>();

    [SerializeField]
    float stageMin = -1;

    [SerializeField]
    float stageMax = 1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            //一つ選ぶ
            SelectHuman();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            //消す
            DeleteHuman();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //生成する
            humans.Add(humanObj);
            float x = Random.Range(stageMin, stageMax);
            float z = Random.Range(stageMin, stageMax);

            Instantiate(humanObj, new Vector3(x, 0.0f, z), Quaternion.identity);
        }
	}

    void SelectHuman()
    {
        if(humans.Count == 0)
        {
            return;
        }

        int selectItem = Random.Range(0, humans.Count);
        //humans[selectItem]
    }

    void DeleteHuman()
    {
        if (humans.Count == 0)
        {
            return;
        }

        //Destroy();
    }
}
