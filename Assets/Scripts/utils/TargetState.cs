using UnityEngine;
using System.Collections;

public class TargetState : MonoBehaviour {

    private bool isDead;
    private bool isGod;
	// Use this for initialization
	void Start () {
        isDead = false;
        isGod = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsDead
    {
        get { return isDead;}
        set { isDead = value; }
    }

    public bool IsGod
    {
        get { return isGod; }
        set { isGod = value; }
    }
}
