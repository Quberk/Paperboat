using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Yodo1.MAS;

public class YodoAdsController : MonoBehaviour
{/*
    public static YodoAdsController yodo;

    private bool rewardCoins, rewardLayar;

    [SerializeField] private CurrencyController currencyController;

    private void Awake()
    {
        if (yodo == null)
        {
            DontDestroyOnLoad(gameObject);
            yodo = this;
        }

        else if (yodo != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("halo");
        Yodo1U3dMasCallback.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] SetInitializeDelegate, success:" + success + ", error: " + error.ToString());
            if (success)
            {
                Debug.Log("[Yodo1 Mas] The initialization has succeeded");
            }
            else
            {
                Debug.Log("[Yodo1 Mas] The initialization has failed");
            }
        });

        Yodo1U3dMas.InitializeSdk();
        InitializeInterstitialAds();
        InitializeRewardedAds();
    }

    #region Interstitial Ad
    private void InitializeInterstitialAds()
    {
        Yodo1U3dMasCallback.Interstitial.OnAdOpenedEvent +=
        OnInterstitialAdOpenedEvent;
        Yodo1U3dMasCallback.Interstitial.OnAdClosedEvent +=
        OnInterstitialAdClosedEvent;
        Yodo1U3dMasCallback.Interstitial.OnAdErrorEvent +=
        OnInterstitialAdErorEvent;
    }

    private void OnInterstitialAdOpenedEvent()
    {
        Debug.Log("[Yodo1 Mas] Interstitial ad opened");
    }

    private void OnInterstitialAdClosedEvent()
    {
        Debug.Log("[Yodo1 Mas] Interstitial ad closed");
    }

    private void OnInterstitialAdErorEvent(Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] Interstitial ad error - " + adError.ToString());
    }

    #endregion


    #region Rewarded Ad
    private void InitializeRewardedAds()
    {
        // Add Events
        Yodo1U3dMasCallback.Rewarded.OnAdOpenedEvent += OnRewardedAdOpenedEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdClosedEvent += OnRewardedAdClosedEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent += OnAdReceivedRewardEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdErrorEvent += OnRewardedAdErorEvent;
    }

    private void OnRewardedAdOpenedEvent()
    {
        Debug.Log("[Yodo1 Mas] Rewarded ad opened");
    }

    private void OnRewardedAdClosedEvent()
    {
        Debug.Log("[Yodo1 Mas] Rewarded ad closed");
    }

    private void OnAdReceivedRewardEvent()
    {
        Rewards(); //Giving Reward
        Debug.Log("[Yodo1 Mas] Rewarded ad received reward");
    }

    private void OnRewardedAdErorEvent(Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] Rewarded ad error - " + adError.ToString());
    }

    #endregion


    void Update()
    {

    }

    private void Rewards()
    {
        if (rewardCoins)
        {
            currencyController.AddCoins(200);
            return;
        }

        if (rewardLayar)
        {
            currencyController.AddLayar(5);
            return;
        }
    }
    
    public void ShowInterstitialAd()
    {
        if (Yodo1U3dMas.IsInterstitialAdLoaded())
        {
            Yodo1U3dMas.ShowInterstitialAd();
        }

        else
        {
            Debug.Log("Yodo1 Mas Interstitial Ad Error");
        }
    }

    public void ShowRewardedAd(int rewardNum)
    {
        if (Yodo1U3dMas.IsRewardedAdLoaded())
        {
            Yodo1U3dMas.ShowRewardedAd();

            //Dismantle all the boolean
            rewardCoins = false;
            rewardLayar = false;

            //Coins
            if (rewardNum == 1)
            {
                rewardCoins = true;
                return;
            }

            //Layar
            if (rewardNum == 2)
            {
                rewardLayar = true;
                return;
            }
        }

        else
        {
            Debug.Log("Yodo1 Mas Rewarded Ad Error");
        }
    }*/
}
