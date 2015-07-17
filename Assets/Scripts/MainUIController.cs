using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{

    public Text tfScore;
    public Text tfBanana;
    public Text tfGuide;
    public GameObject[] initBlockList;
    public Transform[] initBlockPos;

    // Use this for initialization
    void Awake()
    {
        if (tfGuide)
            tfGuide.text = GameData.getLanguage("guide");
    }
    void Start()
    {
        GameData.score = 0;
        GameData.banana = 0;
    }

    public GameObject ninjia;
    public GameObject factory;
    void Reset()
    {
        Debug.Log("Reset");
        Start();
        if (ninjia)
            ninjia.SendMessage("Reset");
        if (factory)
            factory.SendMessage("Reset");
        Camera.main.SendMessage("Reset");
        if (initBlockList.Length == initBlockPos.Length)
        {
            for (int i = 0; i < initBlockList.Length; i++)
            {
                initBlockList[i].SendMessage("Reset");
            }
        }
    }

    public void ShowGuide()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (tfScore)
            tfScore.text = GameData.score.ToString();
        if (tfBanana)
            tfBanana.text = GameData.banana.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
