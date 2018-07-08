using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : SingletonMonoBehaviour<HumanManager>
{
    [SerializeField]
    Human chikaPrefab = null;

    [SerializeField]
    Human syoyaPrefab = null;

    List<Human> humans = new List<Human>();

    [SerializeField]
    float stageMin = -1;

    [SerializeField]
    float stageMax = 1;

    [SerializeField]
   public GameObject humanPool = null;


    [SerializeField]
    int HumanNumber = 20;

    [SerializeField]
    float normalSpeed = 2;
    public float GetNormalSpeed
    {
        get
        {
            return normalSpeed;
        }
    }

    [SerializeField]
    float playerSpeed = 4;
    public float GetPlayerSpeed
    {
        get
        {
            return playerSpeed;
        }
    }
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < HumanNumber; i++)
        {
            if (i % 2 == 0)
                InstantiateHuman(true);
            else
                InstantiateHuman(false);
            humans[humans.Count - 1].SetWalkSpeed=normalSpeed;
            
        }
        SelectHuman();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectHuman()
    {
        if (humans.Count == 0)
        {
            return;
        }

        while (true)
        {
            int r = Random.Range(0, humans.Count);
            if (humans[r].IsCoupling==false)
            {
                humans[r].SetUserSelect();
                humans[r].SetWalkSpeed=playerSpeed;
                break;
            }
        }
        Debug.Log("新しく設定");

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
                Destroy(men.gameObject);

            if (h == women)
                Destroy(women.gameObject);
        }

        //補充
        InstantiateHuman(true);
        InstantiateHuman(false);

    }
    public void DeleteHuman(Human human)
    {
        if (humans.Count == 0)
        {
            return;
        }

        if(human.GetHumanState==HumanState.Boys)
            InstantiateHuman(false);
        else
            InstantiateHuman(true);

        foreach (Human h in humans)
        {
            if (h == human)
                Destroy(human.gameObject);
        }
    }
    public void InstantiateHuman(bool generateChika)
    {
        float x = Random.Range(stageMin, stageMax);
        float z = Random.Range(stageMin, stageMax);
        Human human;
        if (generateChika)
            human = Instantiate(chikaPrefab, new Vector3(x, 0.0f, z), Quaternion.identity).GetComponent<Human>();
        else
            human = Instantiate(syoyaPrefab, new Vector3(x, 0.0f, z), Quaternion.identity).GetComponent<Human>();
        human.transform.parent = humanPool.transform;

        humans.Add(human);
    }
}
