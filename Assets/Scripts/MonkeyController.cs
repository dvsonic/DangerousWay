using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MonkeyController : MonoBehaviour {

	// Use this for initialization
    public enum NinjiaState { IDLE,RUN,DEAD};
    public NinjiaState state;
    public float speed;
    public BlockFactory factory;
    public GameObject guide;
    public AudioSource run;
    public AudioSource dead;
    public AudioSource bonus;
    private GameObject curBlock;
    public Transform bgFar;
    public Transform bgMiddle;
    public Transform bgNear;
    public GameObject pickUp;
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 8, false);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        EventManager.getInstance().addEventListener(Event_Name.BLOCK_DROP_END, OnBlockDropEnd);
    }
	void Start () {

        GetComponent<BoxCollider2D>().isTrigger = false;
        SetState(NinjiaState.IDLE);
        _isStart = false;
    }

    public void Reset()
    {
        Start();
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = false;
        transform.position = new Vector3(-2.25f, 2.3f);
        GetComponent<TargetState>().IsDead = false;
        GetComponent<Animator>().SetBool("isRun", false);
        GetComponent<Animator>().SetBool("isDead", false);
        GetComponent<Animator>().SetTrigger("Reset");
        ResetBG();
    }

    private void updateBG()
    {
        if (bgFar)
            bgFar.position -= new Vector3(0.1f * speed * Time.deltaTime, 0, 0);
        if (bgNear)
            bgNear.position -= new Vector3(0.6f * speed * Time.deltaTime, 0, 0);
        if (bgMiddle)
            bgMiddle.position -= new Vector3(0.2f * speed * Time.deltaTime, 0);
    }

    private void ResetBG()
    {
        if (bgFar)
        {
            bgFar.position = new Vector3(0, bgFar.position.y, bgFar.position.z);
            bgFar.SendMessage("Reset");
        }
        if (bgMiddle)
        {
            bgMiddle.position = new Vector3(0, bgMiddle.position.y, bgMiddle.position.z);
            bgMiddle.SendMessage("Reset");
        }
        if (bgNear)
        {
            bgNear.position = new Vector3(0, bgNear.position.y, bgNear.position.z);
            bgNear.SendMessage("Reset");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameData.isStart)
            return;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!_isStart)
                GameStart();
            else
                Run();
        }

#else
        if (Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            if (!_isStart)
                GameStart();
            else
                Run();
        }
#endif
    }

    public void GameStart()
    {
        _isStart = true;
        if (curBlock)
        {
            curBlock.SendMessage("InvokeNext");
        }
        if (guide)
            Destroy(guide.gameObject);
        factory.CreatBlock();
    }

    private float targetX;
    public void Run()
    {
        if (state == NinjiaState.DEAD)
            return;
        if (curBlock)
        {
            GameObject next = curBlock.GetComponent<BlockController>().nextBlock;
            if (next)
            {
                next.SendMessage("StopDrop");
                targetX = next.transform.position.x;
                curBlock = next;
            }
            SetState(NinjiaState.RUN);
        }
    }

    public void SetState(NinjiaState state)
    {
        this.state = state;
        switch (state)
        {
            case NinjiaState.RUN:
                GetComponent<Animator>().SetBool("isRun", true);
                if (run && !run.isPlaying)
                    run.Play();
                break;
            case NinjiaState.IDLE:
                GetComponent<Animator>().SetBool("isRun", false);
                if (run)
                    run.Stop();
                break;
            case NinjiaState.DEAD:
                GetComponent<Animator>().SetBool("isDead", true);
                GetComponent<Rigidbody2D>().isKinematic = true;
                if (dead)
                    dead.Play();
                break;
        }
    }

    void FixedUpdate()
    {
        if (state == NinjiaState.RUN)
        {
            if (transform.position.x >= targetX)
            {
                SetState(NinjiaState.IDLE);
                Camera.main.GetComponent<CameraFollow>().FollowOnce();
            }
            else
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                updateBG();
            }

        }
        if (transform.position.y < -(Camera.main.orthographicSize-1) && state != NinjiaState.DEAD)
            SetState(NinjiaState.DEAD);

    }

    private bool _isStart;
    private GameObject _standBlock;
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!_isStart)
        {
            if (coll.gameObject.tag == "Block")
            {
                curBlock = coll.gameObject;
                _standBlock = coll.gameObject;
            }
            return;
        }
        if(coll.gameObject.tag == "Block")
        {
            if (coll.contacts[0].normal.Equals(new Vector2(0, 1)))
            {
                GameData.score++;
                factory.CreatBlock();
                _standBlock = coll.gameObject;
            }
            else
            {
                if (_standBlock && coll.transform.position.y - _standBlock.transform.position.y < 0.2f)
                {
                    transform.position += new Vector3(0, coll.transform.position.y - _standBlock.transform.position.y);
                    _standBlock = coll.gameObject;
                    GameData.score++;
                    factory.CreatBlock();
                }
                else
                    OnDead();
            }

        }
    }

    void OnDead()
    {
        if (GetComponent<TargetState>().IsDead == false)
        {
            GetComponent<TargetState>().IsDead = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(GameEnd());
        }
    }


    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Block")
        {
            if (run && run.isPlaying)
                run.Stop();
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Banana")
        {
            GameData.banana++;
            Instantiate(pickUp, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
            DestroyObject(coll.gameObject);
            if (bonus)
                bonus.Play();
        }

    }

    private IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1.0f);
        GameScene.GotoScene(3);
    }

    public void GotoEnd()
    {
        GameScene.GotoScene(3);
    }

    private void OnBlockDropEnd()
    {
        if (state != NinjiaState.DEAD)
        {
            targetX = transform.position.x + 2;
            SetState(NinjiaState.RUN);
        }
    }
}
