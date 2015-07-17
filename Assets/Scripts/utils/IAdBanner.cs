using UnityEngine;
using System.Collections;
#if UNITY_IOS
using ADBannerView = UnityEngine.iOS.ADBannerView;

public class IAdBanner : MonoBehaviour
{

    private ADBannerView banner = null;
    void Start()
    {

    }


    void OnBannerClicked()
    {
        Debug.Log("Clicked!\n");
    }

    void OnBannerLoaded()
    {
        Debug.Log("Loaded!\n");
    }

    public void ShowBanner()
    {
        if (AdController.bannerInventor == AdController.ADInventor.iad)
        {
            if (banner == null)
            {
                banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.BottomCenter);
                ADBannerView.onBannerWasClicked += OnBannerClicked;
                ADBannerView.onBannerWasLoaded += OnBannerLoaded;
                banner.visible = true;
            }
            else
                banner.visible = true;
            Debug.Log("Show Adbanner");

        }
    }

    public void HideBanner()
    {
        if(banner != null)
            banner.visible = false;
        Debug.Log("Hide Adbanner");
    }
}
#endif