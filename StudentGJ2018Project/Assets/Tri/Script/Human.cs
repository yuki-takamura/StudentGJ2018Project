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
    public bool UserSelect
    {
        get
        {
            return userSelect;
        }
        set
        {
            userSelect = value;
        }
    }

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
    string WallTag = "Wall";
    string DeathTag = "Death";


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


    CharacterController characterController;
    public CharacterController GetCharacterController
    {
        get
        {
            return characterController;
        }
    }

    Vector3 velocity;

    [SerializeField]
    float walkSpeed = 1.0f;
    public float SetWalkSpeed
    {
        set
        {
            walkSpeed = value;
        }
    }


    Animator animator;

    string onePlayertV = "Vertical 1";
    string onePlayertH = "Horizontal 1";

    string twoPlayertV = "Vertical 2";
    string twoPlayertH = "Horizontal 2";

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        SetWalk();

    }

    // Update is called once per frame
    void Update()
    {
        if (isCoupling && humanState == HumanState.Boys)
            return;
        if (userSelect && isCoupling == false)
        {
            MoveController();
            return;
        }

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
        float h1 = 0;
        float v1 = 0;

        if (GameSystem.Instance.NowSecondRound == false)
        {
            Debug.Log("JoyConR" + Input.GetAxis(onePlayertH));
            h1 = Input.GetAxis(onePlayertH);
            v1 = Input.GetAxis(onePlayertV);
        }
        else
        {
            Debug.Log("JoyConL" + Input.GetAxis(twoPlayertH));
            h1 = -Input.GetAxis(twoPlayertH);
            v1 = -Input.GetAxis(twoPlayertV);
        }

        //if (Input.GetKey(KeyCode.UpArrow))
        //    h1 = -1;
        //else if (Input.GetKey(KeyCode.DownArrow))
        //    h1 = 1;
        //else if (Input.GetKey(KeyCode.RightArrow))
        //    v1 = 1;
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //    v1 = -1;




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


    // void OnCollisionEnter(Collision other)
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        // hit.gameObjectで衝突したオブジェクト情報が得られる
        //プレイヤーが選択中で相手が性別違うなら
        if (other.collider.CompareTag(HumanTag) && userSelect == true && isCoupling == false)
        {
            Human human = other.gameObject.GetComponent<Human>();
            if (this.GetHumanState == human.GetHumanState || human.IsCoupling == true)
                return;

            Coupling(human);
        }
        else if (other.collider.CompareTag(MouseTag) && IsCoupling == true)
        {
            HitMouse();
        }
        else if (other.collider.CompareTag(WallTag) && userSelect == false)
        {
            //ぶつかった方向より90度以上別の方向に移動
            if (isCoupling)
            {
                SetGoalMove();
                //カップルなら一番近いゴールへ向かう
            }
            else
            {
                ChangeWalk(other.gameObject.transform.position);
            }
        }
        else if (other.collider.CompareTag(DeathTag) && userSelect == false &&isCoupling==false)
        {
            HumanManager.Instance.DeleteHuman(this);
        }
    }

    private void Coupling(Human human)
    {
        //Debug.Log("当たりました=" + this.gameObject.name + "   " + other.gameObject.name);
        //カップルにする
        Debug.Log("カップルになりました");
        SetWalkSpeed = HumanManager.Instance.GetNormalSpeed;
        human.SetWalkSpeed = HumanManager.Instance.GetNormalSpeed;

        couplingHuman = human;
        couplingHuman.couplingHuman = this;
        couplingHuman.IsCoupling = true;
        couplingHuman.userSelect = false;
        this.userSelect = false;
        this.isCoupling = true;

        if (this.humanState == HumanState.Boys)
        {
            Vector2 vec = new Vector2(transform.position.x - couplingHuman.transform.position.x, transform.position.z - couplingHuman.transform.position.z);

            //離れないと走れないためずらす
            float separateLength = 0.3f;
            characterController.Move(
                new Vector3(
               vec.normalized.x * separateLength, 0,
                vec.normalized.y * separateLength));

            //同じ方向で歩く
            transform.parent = couplingHuman.transform;
            transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            Vector2 vec = new Vector2(couplingHuman.transform.position.x - transform.position.x, couplingHuman.transform.position.z - transform.position.z);

            //離れないと走れないためずらす
            float separateLength = 0.3f;
            couplingHuman.GetCharacterController.Move(
                new Vector3(
               vec.normalized.x * separateLength, 0,
                vec.normalized.y * separateLength));

            //子オブジェクトにする
            couplingHuman.transform.parent = transform;
            couplingHuman.transform.localEulerAngles = Vector3.zero;
        }


        if (false/*方向キー入力していたらその方向へ動く*/)
        {

        }
        else
        {
            //一番近い所へ向かう
            SetGoalMove();
        }

        //新しいプレイヤーを選ぶ
        HumanManager.Instance.SelectHuman();
    }

    public void SetAutoMove(float time, Vector2 vec)
    {
        walkTime = time;
        walkVec = vec;
    }


    private void ChangeWalk(Vector3 endPosition)
    {
        if (humanState == HumanState.Boys)
        {
            Vector2 vec = new Vector2(transform.position.x - endPosition.x, transform.position.z - endPosition.z);
            SetAutoMove(walkTime, vec);
        }
        else
        {
            walkTime = 100f;//壁、ゴールに当たるまで突き進むため
            Vector2 vec = new Vector2(transform.position.x - endPosition.x, transform.position.z - endPosition.z);
            walkVec = vec;
        }
    }

    private void SetGoalMove()
    {

        if (humanState == HumanState.Boys)
        {
            Vector3 nearGoal = GoalManager.Instance.GetNearGoalPosition(transform.position);
            Vector2 vec = new Vector2(nearGoal.x - transform.position.x, nearGoal.z - transform.position.z);

            SetAutoMove(walkTime, vec);
            walkTime = 100f;//壁、ゴールに当たるまで突き進むため
        }
        else
        {
            walkTime = 100f;//壁、ゴールに当たるまで突き進むため
            Vector3 nearGoal = GoalManager.Instance.GetNearGoalPosition(transform.position);
            walkVec = new Vector2(nearGoal.x - transform.position.x, nearGoal.z - transform.position.z);
        }
    }


    private void HitMouse()
    {
        //別れさせる
        if (HumanState.Boys == humanState)
        {
            //男だったら
            isCoupling = false;
            couplingHuman.isCoupling = false;
            couplingHuman.SetWalk();
            SetWalk();
            transform.parent = HumanManager.Instance.transform;
            //管理しなおす
            transform.position += new Vector3(0, -transform.position.y, 0);

        }
        else
        {
            isCoupling = false;
            couplingHuman.isCoupling = false;
            couplingHuman.SetWalk();
            SetWalk();
            couplingHuman.transform.parent = HumanManager.Instance.transform;
            transform.position += new Vector3(0, -transform.position.y, 0);
        }
    }


    public void SetUserSelect()
    {
        userSelect = true;
    }

}
