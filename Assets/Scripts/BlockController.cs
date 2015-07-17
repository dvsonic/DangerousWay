using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public GameObject nextBlock;
    public float posLine;
    private bool hasInvokeNext;
    private bool hasDispatch;
	// Use this for initialization
	void Start () {
        if (!GetComponent<Rigidbody2D>().isKinematic)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Camera.main.orthographicSize+0.6f, gameObject.transform.position.z);
            hasInvokeNext = false;
        }
        else
            hasInvokeNext = true;

	}

    public void Reset()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Camera.main.orthographicSize+0.6f, gameObject.transform.position.z);
        hasInvokeNext = false;
        hasDispatch = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasInvokeNext && gameObject.transform.position.y <= posLine)
            InvokeNext();
        if (!hasDispatch && transform.position.y < -Camera.main.orthographicSize)
        {
            EventManager.getInstance().trigger(Event_Name.BLOCK_DROP_END);
            hasDispatch = true;
        }
	}

    public void StartDrop()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void StopDrop()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        InvokeNext();
    }

    public void InvokeNext()
    {
        if (nextBlock)
        {
            nextBlock.SendMessage("StartDrop");
            hasInvokeNext = true;
        }
    }
}
