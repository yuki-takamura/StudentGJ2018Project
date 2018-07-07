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
    [SerializeField]
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




    CharacterController characterController;

    Vector3 velocity;

    [SerializeField]
    float walkSpeed = 1.0f;

    Animator animator;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        SetWalk();

    }

    // Update is called once per frame
    void Update()
    {

        if (userSelect/* && isCoupling == false*/)
        {
            MoveController();
            return;
        }


        if (isCoupling && humanState == HumanState.Boys)
            return;

        //ランダムウォークを書く

        walkTime -= Time.deltaTime;
        if (walkTime < 0)
        {
            SetWalk();
        }

        MoveAuto();
    }

    public void SetWalk()
    {

        //一旦ランダム
        walkTime = Random.Range(MinTime, MaxTime);
        walkVec = new Vector2(Random.Range(-1.0f, 1f), Random.Range(-1.0f, 1f));


        //   transform.position += new Vector3(walkVec.normalized.x * moveLength, 0, walkVec.normalized.y * moveLength);

        //transform.rotation = Quaternion.LookRotation(transform.position +
        //(Vector3.right * (walkVec.x)) +
        //(Vector3.forward * (walkVec.y))
        //- transform.position);

        animator.SetFloat("Speed", velocity.magnitude);
        transform.LookAt(transform.position + velocity);


    }


    private void MoveAuto()
    {
        velocity = new Vector3(walkVec.normalized.x * walkSpeed, 0.0f, walkVec.normalized.y * walkSpeed);

        //後で消します
        transform.position += new Vector3(0, -transform.position.y, 0);


        if (characterController.isGrounded)
        {
            if (velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Speed", velocity.magnitude);
                transform.LookAt(transform.position + velocity);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }

    //    Debug.Log(this.name + "   " + velocity);

        characterController.Move(velocity * walkSpeed * Time.deltaTime);
    }


    public void MoveController()
    {
        // Joy-Con(R)
        var h1 = Input.GetAxis("Horizontal 1");
        var v1 = Input.GetAxis("Vertical 1");

        velocity = new Vector3(v1, 0.0f, -h1);

        //TODO デバッグ用　あとで消す
        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            transform.position = Vector3.zero;
        }

        if (characterController.isGrounded)
        {
            if (velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Speed", velocity.magnitude);
                transform.LookAt(transform.position + velocity);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * walkSpeed * Time.deltaTime);



    }


    void OnControllerColliderHit(ControllerColliderHit other)
    {
        // hit.gameObjectで衝突したオブジェクト情報が得られる
        //プレイヤーが選択中で相手が性別違うなら
        //女性側で制御してしまう
      //  Debug.Log("コリダーなう=" + this.gameObject.name);
        if (other.collider.CompareTag(HumanTag)&& userSelect==true&&isCoupling==false)
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
                transform.position += new Vector3(0, -transform.position.y, 0);

            }
            else
            {
                isCoupling = false;
                couplingHuman.isCoupling = false;
                couplingHuman.SetWalk();
                SetWalk();
                couplingHuman.transform.parent = humanPool.transform;
                transform.position += new Vector3(0, -transform.position.y, 0);
            }
        }
        else
        {
            //ぶつかった方向より90度以上別の方向に移動



        }
    }


    public void SetUserSelect()
    {
        userSelect = true;
    }

}
