using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HumanState
{
    Boys,
    Girls,
}




public class Human : MonoBehaviour
{
    bool userSelect = false;
    bool isCoupling = false;
    public bool IsCoupling
    {
        get
        {
            return isCoupling;
        }
        set
        {
            isCoupling = value;
        }
    }
    //ごり押し、カップルにした人間
    Human couplingHuman = null;
    public Human CouplingHuman
    {
        get
        {
            return couplingHuman;
        }
    }


    string HumanTag = "Human";
    string MouseTag = "Mouse";

    [SerializeField]//一旦
    HumanState humanState;



    public HumanState GetHumanState
    {
        get
        {
            return humanState;
        }
        set
        {
            humanState = value;
        }
    }

    float walkTime;
    Vector2 walkVec = Vector2.zero;

    [SerializeField]
    float MaxTime = 10f;
    [SerializeField]
    float MinTime = 7f;
    [SerializeField, Header("移動速度")]
    float moveLength = 0.01f;

    [SerializeField]
    GameObject humanPool;



    void Start()
    {
        SetWalk();

    }

    // Update is called once per frame
    void Update()
    {
        if (userSelect)
            return;

        //if (isCoupling && humanState == HumanState.Boys)
        //    return;
        if (humanState == HumanState.Boys)
            return;



        //ランダムウォークを書く

        walkTime -= Time.deltaTime;
        if (walkTime < 0)
        {
            SetWalk();
        }
        transform.position += new Vector3(walkVec.x * moveLength, 0, walkVec.y * moveLength);


    }

    public void SetWalk()
    {

        //一旦ランダム
        walkTime = Random.Range(MinTime, MaxTime);
        walkVec = new Vector2(Random.Range(-1.0f, 1f), Random.Range(-1.0f, 1f));


        transform.position += new Vector3(walkVec.normalized.x * moveLength, 0, walkVec.normalized.y * moveLength);

        transform.rotation = Quaternion.LookRotation(transform.position +
        (Vector3.right * (walkVec.x)) +
        (Vector3.forward * (walkVec.y))
        - transform.position);



    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");
        //プレイヤーが選択中で相手が性別違うなら
        //女性側で制御してしまう
        if (other.collider.CompareTag(HumanTag)/*&& userSelect==true*/)
        {
            if (other.gameObject.GetComponent<Human>().GetHumanState == HumanState.Boys)
            {
                //カップルにする
                Debug.Log("カップルになりました");
                couplingHuman = other.gameObject.GetComponent<Human>();
                couplingHuman.couplingHuman = this;
                couplingHuman.IsCoupling = true;
                this.isCoupling = true;
                //子オブジェクトにする
                couplingHuman.transform.parent = transform;

                if (true/*方向キー入力していたらその方向へ動く*/)
                {

                }
                else
                {
                    //一番近い所へ向かう

                }


            }
        }
        else if (other.collider.CompareTag(MouseTag) && IsCoupling == true)
        {
            //別れさせる
            if (HumanState.Boys == humanState)
            {
                //男だったら
                isCoupling = false;
                couplingHuman.isCoupling = false;
                couplingHuman.SetWalk();
                SetWalk();
                transform.parent = humanPool.transform;
                //管理しなおす

            }
            else
            {
                isCoupling = false;
                couplingHuman.isCoupling = false;
                couplingHuman.SetWalk();
                SetWalk();
                couplingHuman.transform.parent = humanPool.transform;
            }
        }
        else
        {
            //ぶつかった方向より90度以上別の方向に移動



        }
    }
}
