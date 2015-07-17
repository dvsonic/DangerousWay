using UnityEngine;
using System.Collections;

public class CamerAdjust : MonoBehaviour {

    public int GameWidth;//游戏宽度
	// Use this for initialization
	void Awake () {
        float GameWidthUnit = GameWidth / 100;
        float GameHeightUnit = GameWidthUnit / Camera.main.aspect;
        Camera.main.orthographicSize = GameHeightUnit / 2;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
