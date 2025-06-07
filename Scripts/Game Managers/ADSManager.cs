using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADSManager : MonoBehaviour
{
    public static ADSManager Instance { get; private set; }

    private const string RewardedAdsUnitId = "";
    private RewardedAd _rewardedAd;
    private float _timeToClickAds = 5f;
    private bool _isRewardReceived;
    private bool _isAdPlaying;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _isAdPlaying = false;
        _isRewardReceived = false;
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadRewardedAd();
    }

    private void AdRewardClosed(object sender, EventArgs e)
    {
        _isAdPlaying = false;
        LoadRewardedAd();
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        _isRewardReceived = true;
    }

    private void LoadRewardedAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        var adRequest = new AdRequest();

        RewardedAd.Load(RewardedAdsUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error);
                return;
            }

            _rewardedAd = ad;

            _rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                _isAdPlaying = false;
                LoadRewardedAd();
            };

            _rewardedAd.OnAdFullScreenContentFailed += (AdError adError) =>
            {
                Debug.LogError("Ad failed to show: " + adError.GetMessage());
                _isAdPlaying = false;
                LoadRewardedAd();
            };
        });
    }

    public void LaunchRewardedAd(Action<bool> onAdResult) => StartCoroutine(LaunchRewardedAdCoroutine(onAdResult));

    private IEnumerator LaunchRewardedAdCoroutine(Action<bool> onAdResult)
    {
        PrepareAdSession();

        yield return WaitForUserToClickAd();

        UIManager.Instance.HideAdsPanelUI();

        yield return WaitUntilAdFinishes();

        onAdResult?.Invoke(_isRewardReceived);
        _isRewardReceived = false;
        TimeManager.Instance.ContinueGame();
    }

    private void PrepareAdSession()
    {
        TimeManager.Instance.PauseGame();
        UIManager.Instance.ShowAdsPanelUI();
        _isRewardReceived = false;
        _isAdPlaying = false;
    }

    private IEnumerator WaitForUserToClickAd()
    {
        float timeLeft = _timeToClickAds;

        while (timeLeft > 0f && !_isAdPlaying)
        {
            timeLeft -= Time.unscaledDeltaTime;
            UIManager.Instance.UpdateADSTimer(timeLeft / _timeToClickAds);
            yield return null;
        }
    }

    private IEnumerator WaitUntilAdFinishes()
    {
        while (_isAdPlaying)
            yield return null;
    }

    public void ShowAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _isAdPlaying = true;
            _isRewardReceived = false;

            _rewardedAd.Show((Reward reward) =>
            {
                _isRewardReceived = true;
            });
        }
        else
        {
            Debug.LogWarning("Rewarded ad is not ready yet.");
        }
    }
}

