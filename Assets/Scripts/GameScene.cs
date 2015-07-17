using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameScene
{
    private static GameObject _start;
    private static GameObject _main;
    private static GameObject _ad;
    private static bool _needRestart;
    public static void init(GameObject start,GameObject main,GameObject ad)
    {
        _start = start;
        _main = main;
        _ad = ad;
        _needRestart = false;
    }

    public static void GotoScene(int index)
    {
        switch(index)
        {
            case 1:
                GameData.isStart = false;
                Time.timeScale = 0;
                _start.SetActive(true);
                _start.SendMessage("ShowStart");
                break;
            case 2:
                GameData.isStart = true;
                Time.timeScale = 1;
                _start.SetActive(false);
                if (_needRestart)
                    _main.SendMessage("Reset");
                else
                    _main.SetActive(true);
                if (_ad)
                {
                    _ad.SendMessage("ShowBanner");
                    _ad.SendMessage("ReloadInterstitial");
                }
                break;
            case 3:
                GameData.isStart = false;
                _needRestart = true;
                _start.SetActive(true);
                _start.SendMessage("ShowEnd");
                if (_ad)
                {
                    _ad.SendMessage("HideBanner");
                    _ad.SendMessage("ShowInterstitial");
                }
                break;
        }
    }
}

