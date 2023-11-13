using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string AndroidAdUnit = "Interstitial_Android";
    string iOSAdUnit = "Interstitial_iOS";
    string adUnitID;

    // Start is called before the first frame update
    private void Awake()
    {
        adUnitID = iOSAdUnit;
        if (Application.platform == RuntimePlatform.Android)
            adUnitID = AndroidAdUnit;
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        // if loading the add didn't worked
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        // ad has failed, make sure to not give reward
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        // ad is beggining to be shown
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        // ad was shown, and ad was also clicked
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        // ad has been completed, reward the player
    }
}
