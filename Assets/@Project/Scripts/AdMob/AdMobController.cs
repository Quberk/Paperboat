using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobController : MonoBehaviour
{
    private RewardedAd dailyCoinsRewardedAd;
    private RewardedAd dailyPaperclipRewardedAd;
    private RewardedAd dailyLayarRewardedAd;
    private RewardedAd reviveRewardedAd;

    private InterstitialAd interstitialAd;

    private string rewardedAd_Id;
    private string interstitial_Id;

    [Header("Outside Controller")]
    [SerializeField] private UIController uIController;
    //[SerializeField] private CurrencyController currencyController;
    CurrencyTransactionManager currencyTransactionManager = new CurrencyTransactionManager();
    private AdjustController adjustController;

    // Start is called before the first frame update
    void Start()
    {
        rewardedAd_Id = "ca-app-pub-8357568692582110/3404495522";
        interstitial_Id = "ca-app-pub-8357568692582110/6619183353";

        MobileAds.Initialize(initStatus => { });

        dailyCoinsRewardedAd = RequestRewardedVideo(rewardedAd_Id, ReceiveDailyExtraCoins, CloseDailyCoinEarned);
        dailyPaperclipRewardedAd = RequestRewardedVideo(rewardedAd_Id, ReceiveDailyExtraPaperClip, CloseDailyPaperClipEarned);
        dailyLayarRewardedAd = RequestRewardedVideo(rewardedAd_Id, ReceiveDailyExtraLayar, CloseDailyLayarEarned);
        reviveRewardedAd = RequestRewardedVideo(rewardedAd_Id, HandleUserEarnedAd, CloseReviveEarned);

        adjustController = AdjustController.Instance;

    }

    #region Request Ads
    private RewardedAd RequestRewardedVideo(string adUnitId, EventHandler<Reward> rewardCallback, EventHandler<EventArgs> closeCallback)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += rewardCallback;
        rewardedAd.OnAdClosed += closeCallback;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    private InterstitialAd RequestInterstitial(string adUnitId)
    {
        InterstitialAd interstitialAds = new InterstitialAd(adUnitId);

        interstitialAds.OnAdLoaded += HandleOnAdLoaded;
        interstitialAds.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAds.OnAdOpening += HandleOnAdOpening;
        interstitialAds.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        interstitialAds.LoadAd(request);
        return interstitialAds;
    }
    #endregion


    public void ShowInterstitialAd()
    {
        interstitialAd = RequestInterstitial(interstitial_Id);

        if (interstitialAd.IsLoaded())
        {
            //Adjust Event
            adjustController.CallingAdjustEvent("ozuto8");

            interstitialAd.Show();
        }
    }


    #region Daily Coin Reward
    public void ShowDailyCoinsRewardedVideo()
    {
        if (dailyCoinsRewardedAd.IsLoaded())
        {
            //Adjust Event
            adjustController.CallingAdjustEvent("o9b5jk");

            dailyCoinsRewardedAd.Show();
        }
    }

    public void CloseDailyCoinEarned(object sender, EventArgs args)
    {
        dailyCoinsRewardedAd = RequestRewardedVideo(rewardedAd_Id, ReceiveDailyExtraCoins, CloseDailyCoinEarned);
    }

    public void ReceiveDailyExtraCoins(object sender, Reward args)
    {
        //currencyController.AddCoins(800);
        currencyTransactionManager.EarnCurrency(800, TransactionCurrencyTypes.Coins);
    }

    #endregion

    #region Daily PaperClip Reward
    public void ShowDailyPaperClipRewardedVideo()
    {
        if (dailyPaperclipRewardedAd.IsLoaded())
        {

            dailyPaperclipRewardedAd.Show();
        }
    }

    public void CloseDailyPaperClipEarned(object sender, EventArgs args)
    {
        dailyPaperclipRewardedAd = RequestRewardedVideo(rewardedAd_Id, ReceiveDailyExtraPaperClip, CloseDailyPaperClipEarned);
    }

    public void ReceiveDailyExtraPaperClip(object sender, Reward args)
    {
        //currencyController.AddPaperClip(1);
        currencyTransactionManager.EarnCurrency(1, TransactionCurrencyTypes.PaperClip);
    }

    #endregion

    #region Daily Layar Reward

    public void ShowDailyLayarRewardedVideo()
    {
        if (dailyLayarRewardedAd.IsLoaded())
        {
            //Adjust Event
            adjustController.CallingAdjustEvent("1pg6xv");

            dailyLayarRewardedAd.Show();
        }
    }

    public void CloseDailyLayarEarned(object sender, EventArgs args)
    {
        dailyLayarRewardedAd = RequestRewardedVideo(rewardedAd_Id, ReceiveDailyExtraLayar, CloseDailyLayarEarned);
    }

    public void ReceiveDailyExtraLayar(object sender, Reward args)
    {
        //currencyController.AddLayar(5);
        currencyTransactionManager.EarnCurrency(5, TransactionCurrencyTypes.Layar);
    }

    #endregion

    #region Revive Ad
    public void ShowReviveRewardedVideo()
    {
        if (reviveRewardedAd.IsLoaded())
        {
            //Adjust Event
            adjustController.CallingAdjustEvent("sw0pmm");

            reviveRewardedAd.Show();
        }
    }
    #endregion


    #region Handlers
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitialAd.Destroy();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void CloseReviveEarned(object sender, EventArgs args)
    {
        uIController.ReviveButton(0);
        reviveRewardedAd = RequestRewardedVideo(rewardedAd_Id, HandleUserEarnedAd, CloseReviveEarned);
    }


    public void HandleUserEarnedAd(object sender, Reward args)
    {

    }
    #endregion
}
