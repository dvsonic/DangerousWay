using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class StartController : MonoBehaviour {

	// Use this for initialization
    public Text tfScore;
    public Text tfDonate;
    public Text tfBanana;
    public Button btnFav;
    public Button btnRank;
    public GameObject badge;
    public GameObject banner;
    public GameObject start;
    public GameObject main;
    public GameObject ad;
    void Awake()
    {
        GameScene.init(start, main, ad);
        SocialManager.GetInstance().Start();
        
#if UNITY_ANDROID
        Destroy(btnRank.gameObject);
        Destroy(btnFav.gameObject);
#endif
        tfScore.text = GameData.getLanguage("bestscore") + "\n" + PlayerPrefs.GetInt("BEST_SCORE");
        if (tfDonate)
        {
            tfDonate.text = GameData.getLanguage("donate");
            tfDonate.gameObject.SetActive(false);
        }
    }
    void Start () {

        
	}

    public void ShowStart()
    {
        tfScore.text = GameData.getLanguage("bestscore") + "\n" + PlayerPrefs.GetInt("BEST_SCORE");
        if (badge)
            badge.SetActive(false);
        if (banner)
            banner.SetActive(true);
        if (tfDonate)
            tfDonate.gameObject.SetActive(false);
    }

    public void ShowEnd()
    {
        tfScore.text = GameData.getLanguage("score") + "\n" + GameData.score.ToString();
        if (badge)
            badge.SetActive(false);
        if (banner)
            banner.SetActive(false);
        if (tfDonate)
            tfDonate.gameObject.SetActive(true);
        total = GameData.score;
        InvokeRepeating("CalcBanana", 0.5f, 0.05f);
    }

    private int total;
    private void CalcBanana()
    {
        if (GameData.banana > 0)
        {
            GameData.banana--;
            total++;
            tfScore.text = GameData.getLanguage("score") + "\n" + total.ToString();
            tfBanana.text = GameData.banana.ToString();
        }
        else
        {
            CancelInvoke("CalcBanana");
            ShowResult();
        }
    }

    private void ShowResult()
    {

        int best = PlayerPrefs.GetInt("BEST_SCORE");
        if (total > best)
        {
            PlayerPrefs.SetInt("BEST_SCORE", total);
            if (badge)
                badge.SetActive(true);
#if !UNITY_EDITOR
            SocialManager.GetInstance().ReportScore("20005", GameData.score);
#endif
        }
    }
    public void Rank()
    {
#if UNITY_IOS && !UNITY_EDITOR
        SocialManager.GetInstance().ShowLeaderboard();
#endif
    }

    public void ToDiscuss()
    {
#if UNITY_IOS && !UNITY_EDITOR
        Application.OpenURL("https://itunes.apple.com/app/id1020021827?mt=8");
#endif
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
    }

    public void StartGame()
    {
        GameScene.GotoScene(2);
    }
}
