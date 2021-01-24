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
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location) => _gameManagerScript.GameContinue();
}
