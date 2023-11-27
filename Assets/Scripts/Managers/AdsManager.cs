using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;

[DefaultExecutionOrder(-2)]
[RequireComponent(typeof(RewardAd), typeof(BannerAd), typeof(InterstitialAd))]
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public string AndroidGameID;
    public string iOSGameID;
    public bool testMode;

    private InterstitialAd interstitialAd;
    public InterstitialAd InterstitialAd => interstitialAd ;

    private BannerAd bannerAd;
    public BannerAd BannerAd => bannerAd;

    private RewardAd rewardAd;
    public RewardAd RewardAd => rewardAd;

    private static AdsManager _instance = null;

    public static AdsManager instance
    {
        get => _instance;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this; 

        interstitialAd = GetComponent<InterstitialAd>();
        bannerAd = GetComponent<BannerAd>();
        rewardAd = GetComponent<RewardAd>();

        string gameID = iOSGameID;
        if (Application.platform == RuntimePlatform.Android)
            gameID = AndroidGameID;

        if (string.IsNullOrEmpty(gameID))
            throw new InvalidDataException("No Game ID set");

        Advertisement.Initialize(gameID, testMode, this);

    }


    public void OnInitializationComplete()
    {
        //RewardAd.LoadAd();
        //BannerAd.LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
