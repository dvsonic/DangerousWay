using UnityEngine;
using System.Collections;

public class FriendController : MonoBehaviour {

    public float speed;
    private bool isFlip;
	// Use this for initialization
	void Awake () {
        Physics2D.IgnoreLayerCollision(10, 8, false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        if (isFlip)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            isFlip = !isFlip;
            if (!isFlip)
                transform.localScale = Vector3.one;
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    public void OnDead()
    {
        GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
