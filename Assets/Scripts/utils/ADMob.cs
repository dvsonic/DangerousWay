using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class ADMob : MonoBehaviour
{

    public enum ADType { BANNER, INTERSTITIAL };
    public ADType type;
    public string unitId;

    private BannerView banner = null;
    private InterstitialAd interstitial = null;
    // Use this for initialization
    void Start()
    {
    }

    private bool _canShowInterstitial;
    public void ReloadInterstitial()
    {
        if (AdController.admob_show && type == ADType.INTERSTITIAL && AdController.interstitialInventor == AdController.ADInventor.admob)
        {
            _canShowInterstitial = false;
            interstitial = new InterstitialAd(unitId);
            interstitial.AdLoaded += HandleInterstitialLoaded;
            interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
            interstitial.AdOpened += HandleInterstitialOpened;
            interstitial.AdClosing += HandleInterstitialClosing;
            interstitial.AdClosed += HandleInterstitialClosed;
            interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
            interstitial.LoadAd(new AdRequest.Builder()
                .AddTestDevice("8CABE432D4C46F1D7F276DADF83D9945")
                .Build());
        }
    }

    void ShowBanner()
    {
        if (AdController.admob_show && type == ADType.BANNER && AdController.bannerInventor == AdController.ADInventor.admob)
        {
            if (null == banner)
            {
                banner = new BannerView(unitId, AdSize.Banner, AdPosition.Bottom);
                banner.AdLoaded += HandleAdLoaded;
                banner.AdFailedToLoad += HandleAdFailedToLoad;
                banner.AdOpened += HandleAdOpened;
                banner.AdClosing += HandleAdClosing;
                banner.AdClosed += HandleAdClosed;
                banner.AdLeftApplication += HandleAdLeftApplication;
                banner.LoadAd(new AdRequest.Builder()
                    .AddTestDevice("8CABE432D4C46F1D7F276DADF83D9945")
                    .Build());
            }
            else
                banner.Show();
        }
    }

    void HideBanner()
    {
        if(banner != null)
            banner.Hide();
    }

    void ShowInterstitial()
    {
        if (interstitial!= null)
        {
            if (interstitial.IsLoaded())
                interstitial.Show();
            else
                _canShowInterstitial = true;
        }
    }

    public void HideInterstitial()
    {
        DestroyAD();
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void DestroyAD()
    {
        if (banner != null)
        {
            banner.Destroy();
            banner = null;
        }
        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
        }
    }

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("Admob HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("Admob HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        print("Admob HandleAdOpened event received");
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
        print("Admob HandleAdClosing event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        print("Admob HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        print("Admob HandleAdLeftApplication event received");
    }

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("Admob HandleInterstitialLoaded event received.");
        if (_canShowInterstitial)
            interstitial.Show();
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("Admob HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("Admob HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("Admob HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("Admob HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("Admob HandleInterstitialLeftApplication event received");
    }


}
