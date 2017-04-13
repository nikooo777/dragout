using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    public string bannerID;
    public string videoID;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
#elif UNITY_ANDROID
        Admob.Instance().initAdmob(bannerID, videoID);
        //Admob.Instance().setTesting(true);
        Admob.Instance().loadInterstitial();
#endif
    }

    public void ShowBanner(bool topNotBottom)
    {

#if UNITY_EDITOR
#elif UNITY_ANDROID
        Admob.Instance().showBannerRelative(AdSize.Banner, topNotBottom ? AdPosition.TOP_CENTER : AdPosition.BOTTOM_CENTER, 0);

#endif
    }

    public void ShowVideo()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        if(Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
        }
#endif
    }

    public void HideBanner()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Admob.Instance().removeBanner();
#endif
    }
}
