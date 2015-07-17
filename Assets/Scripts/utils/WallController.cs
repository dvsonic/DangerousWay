using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = Camera.main.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.transform.position - offset;
	}
}
