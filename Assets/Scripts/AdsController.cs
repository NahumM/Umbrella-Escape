using EasyMobile;

public class AdsController
{
    private GameManager _gameManagerScript;

    public AdsController(GameManager gameManager)
    {
        _gameManagerScript = gameManager;
        Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
    }
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

    public void ShowBannedAd()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }

    public void HideBannerAd()
    {
        Advertising.HideBannerAd();
    }
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location) => _gameManagerScript.GameContinue();
}
