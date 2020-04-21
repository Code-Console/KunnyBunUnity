using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class Ads : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    private bool isupdate = false;
    void Start()
    {
        M.SHOWLOG("InterstitialAd event Start");
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        RequestRewardBasedVideo();
    }
    void Update()
    {
        if (isupdate)
        {
            isupdate = false;
            transform.GetComponent<Menu>().UpdateValues();
            if(M.next2Coin == 0 && M.next2Dimand==0 && M.IS_REWARD)
            {
                M.IS_REWARD = false;
                transform.GetComponent<Menu>().sleepVideoRewordAds();
            }
        }
    }
    private void RequestRewardBasedVideo()
    {



        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        rewardReloaded();

    }
    public void rewardReloaded() {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        M.SHOWLOG("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleRewardBasedVideoClosed event received");
        this.rewardReloaded();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //string type = args.Type;
        //double amount = args.Amount;
        M.SHOWLOG("HandleRewardBasedVideoRewarded event received for [" + M.COINS+"]  ["+ M.DIMONDS+"]");
        M.COINS += M.next2Coin;
        M.DIMONDS += M.next2Dimand;
        isupdate = true;
        M.next2Coin = M.next2Dimand = 0;
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleRewardBasedVideoLeftApplication event received");
    }

    public void showReward(int dimand, int coin)
    {
        M.SHOWLOG("showReward [dimond = "+ dimand + "] ~~~~~~~[coin = "+ coin + "]~~~~~~~~");
        M.next2Dimand = dimand;
        M.next2Coin = coin;
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
        transform.GetComponent<Menu>().UpdateValues();


#if UNITY_EDITOR
        HandleRewardBasedVideoRewarded(null, null);
#endif

    }

    private void RequestInterstitial()
    {
        M.SHOWLOG("RequestInterstitial event ~~~~~~~~~~~~~~~");
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleAdLoaded event received");
        //this.interstitial.Show();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        M.SHOWLOG("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleAdClosed event received");
        RequestInterstitial();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        M.SHOWLOG("HandleAdLeavingApplication event received");
    }


    public void showInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

}
