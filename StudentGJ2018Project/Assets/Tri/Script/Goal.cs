using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    string HumanTag = "Human";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag(HumanTag))
        {
            Human human = other.gameObject.GetComponent<Human>();
            if(human.IsCoupling==true)
            {
                GameSystem.Instance.Goal();
                //削除パーティクル的なの

                //ヒューマン管理システムに渡して削除？？
                HumanManager.Instance.DeleteHuman(human,human.CouplingHuman);

            }
        }
    }
}
