using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerAd : MonoBehaviour
{
    public Button hideBannerButton;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    string AndroidAdUnit = "Banner_Android";
    string iOSAdUnit = "Banner_iOS";
    string adUnitID;

    // Start is called before the first frame update
    private void Awake()
    {
        adUnitID = iOSAdUnit;
        if (Application.platform == RuntimePlatform.Android)
            adUnitID = AndroidAdUnit;

        if (hideBannerButton != null)
            hideBannerButton.interactable = false;

        Advertisement.Banner.SetPosition(bannerPosition);
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError,
        };

        Advertisement.Banner.Load(options);
    }

    private void OnBannerLoaded()
    {
        if (hideBannerButton != null)
        {
            hideBannerButton.onClick.AddListener(HideBannerAd);
            hideBannerButton.interactable = true;
        }

        ShowBannerAd();
    }

    private void OnBannerError(string msg)
    {
       
    }

    private void OnBannerClicked()
    {
        // reward player when banner clicked or whaterver we want
    }

    private void OnBannerShown()
    {
        if (hideBannerButton != null)
        {
            hideBannerButton.interactable = true;
        }
    }

    private void OnBannerHidden()
    {
        if (hideBannerButton != null)
        {
            hideBannerButton.interactable = false;
        }
    }

    private void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(adUnitID, options);
        
        if (hideBannerButton != null)
            hideBannerButton.gameObject.SetActive(true);
    }

    private void HideBannerAd()
    {
        Advertisement.Banner.Hide();

        if (hideBannerButton != null)
            hideBannerButton.gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        if (hideBannerButton != null)
            hideBannerButton.onClick.RemoveAllListeners();
    }

}
