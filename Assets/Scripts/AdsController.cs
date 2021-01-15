using UnityEngine;
using EasyMobile;

public class AdsController : MonoBehaviour
{
    private GameManager gameManagerScript;
    private void Start() => gameManagerScript = GameObject.FindObjectOfType<GameManager>();
    private void OnEnable() => Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
    public void ShowRewardedAd() 
    {
        if (Advertising.IsRewardedAdReady())
            Advertising.ShowRewardedAd();
    }
    public void ShowInterstitialAd()
    {
        if (Advertising.IsInterstitialAdReady())
            Advertising.ShowInterstitialAd();
    }
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location) => gameManagerScript.GameContinue();
}
