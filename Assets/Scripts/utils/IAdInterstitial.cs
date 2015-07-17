using UnityEngine;
using System.Collections;

#if UNITY_IOS
using ADInterstitialAd = UnityEngine.iOS.ADInterstitialAd;

public class IAdInterstitial : MonoBehaviour
{

    private ADInterstitialAd fullscreenAd = null;

    void Start()
    {

    }

    void OnFullscreenLoaded()
    {
        Debug.Log("OnFullscreenLoaded");
        if (_canShowInterstitial)
            fullscreenAd.Show();
    }

    public void ReloadInterstitial()
    {
        _canShowInterstitial = false;
        if (AdController.interstitialInventor == AdController.ADInventor.iad)
        {
            if (null == fullscreenAd)
            {
                fullscreenAd = new ADInterstitialAd();
                ADInterstitialAd.onInterstitialWasLoaded += OnFullscreenLoaded;
            }
            else
                fullscreenAd.ReloadAd();
            Debug.Log("Reload FullScreen");
        }    
    }

    private bool _canShowInterstitial;
    public void ShowInterstitial()
    {
        if (AdController.interstitialInventor == AdController.ADInventor.iad)
        {
            if (fullscreenAd != null && fullscreenAd.loaded)
                fullscreenAd.Show();
            else
                _canShowInterstitial = true;
            Debug.Log("Show FullScreen");
        }

    }

    public void HideInterstitial()
    {
        fullscreenAd = null;
        _canShowInterstitial = false;
        Debug.Log("Hide FullScreen");
    }
}
#endif