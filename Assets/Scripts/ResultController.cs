using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {

	// Use this for initialization
    public Text tfScore;
    public Text tfScoreBest;
    public Text tfReplay;
    public Text tfQuit;
    public Text tfShare;
    public Text tfEnd;
    public Text tfRecord;
    public GameObject Record;
    public Button btnHome;
    public Button btnRank;
    public Button btnDiscuss;
    public Button btnRestart;
    public Button btnHomeIOS;
    public const string BEST_SCORE = "BEST_SCORE";

    void Awake()
    {
        if (tfRecord)
            tfRecord.text = GameData.getLanguage("record");
#if UNITY_ANDROID
        Destroy(btnRank.gameObject);
        Destroy(btnDiscuss.gameObject);
        Destroy(btnHomeIOS.gameObject);
#else
       Destroy(btnHome.gameObject);
#endif
    }
	void Start () {

	}

    void OnEnable()
    {
        tfScore.text = GameData.getLanguage("score") + "\n" + GameData.score;
        int best = PlayerPrefs.GetInt(BEST_SCORE);
        if (GameData.score >= best)
        {
            PlayerPrefs.SetInt(BEST_SCORE, GameData.score);
            best = GameData.score;
            Record.SetActive(true);
        }
        else
        {
            Record.SetActive(false);
        }
        tfScoreBest.text = GameData.getLanguage("bestscore") + "\n" + best;
#if !UNITY_EDITOR
        SocialManager.GetInstance().ReportScore("20003", GameData.score);
#endif
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

    public void Restart()
    {
        GameData.score = 0;
        //Application.LoadLevel("Main");
        GameScene.GotoScene(2);
    }

    public void Quit()
    {
        Canvas canvas = GameObject.FindObjectOfType(typeof(Canvas)) as Canvas;
        if (canvas)
            canvas.SendMessage("DestroyAD", SendMessageOptions.DontRequireReceiver);
        Application.Quit();
    }

    public void Login()
    {
        GameScene.GotoScene(1);
    }

    public void Rank()
    {
        SocialManager.GetInstance().ShowLeaderboard();
    }

    public void ToDiscuss()
    {
#if UNITY_IPHONE
        Application.OpenURL("https://itunes.apple.com/app/id1011674889?mt=8");
#endif
    }

    /*void OnEnable()
    {
        gameObject.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
    }

    void OnDisable()
    {
        gameObject.SendMessage("DestroyAD", SendMessageOptions.DontRequireReceiver);
    }*/
}
