using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : SingletonMonoBehaviour<HumanManager>
{
    [SerializeField]
    Human humanObj = null;

    List<Human> humans = new List<Human>();

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
	}

    public void SelectHuman()
    {
        if(humans.Count == 0)
        {
            return;
        }

        int selectItem = Random.Range(0, humans.Count);
        //humans[selectItem]
    }

    public void DeleteHuman(Human men, Human women)
    {
        if (humans.Count == 0)
        {
            return;
        }

        foreach (Human h in humans)
        {
            if (h == men)
                Destroy(men);

            if (h == women)
                Destroy(women);
        }
    }

    public void InstantiateHuman()
    {
        humans.Add(humanObj);
        float x = Random.Range(stageMin, stageMax);
        float z = Random.Range(stageMin, stageMax);

        Instantiate(humanObj, new Vector3(x, 0.0f, z), Quaternion.identity);
    }
}
