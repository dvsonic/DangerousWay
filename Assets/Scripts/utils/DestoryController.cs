using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestoryController : MonoBehaviour {

	// Use this for initialization
    public List<GameObject> ignoreDestroy;
    private Vector3 offset;
	void Start () {
        offset = Camera.main.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.transform.position - offset;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "hero")
        {
            coll.gameObject.SendMessage("OnDead");
        }
        else
        {
            if (coll.gameObject.transform.parent)
            {
                if (ignoreDestroy.IndexOf(coll.gameObject.transform.parent.gameObject) < 0)
                    Destroy(coll.gameObject.transform.parent.gameObject);
            }
            else
            {
                if(ignoreDestroy.IndexOf(coll.gameObject)<0)
                    Destroy(coll.gameObject);
            }
        }
    }
}
