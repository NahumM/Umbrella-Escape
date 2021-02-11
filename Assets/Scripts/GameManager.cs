using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private int _coins;
    private float _score;
    private int _scoreRecord;
    [SerializeField] private float _pointsMultiplier;
    private float _timesDied;
    private bool _isGameOver;
    private bool _isGameStarted;
    private GameObject umbrella;
    private AdsController _adsController;
    private PurchaseController _purchaseController;

    [SerializeField] private AudioMixer mixer;
    public float musicVolume;
    public float effectsVolume;
    public bool _isVibrationOn = true;

    private void Awake()
    {
        DeleteOtherIntancesOfThisGameObject();
        LoadGameData();
    }
    void Start()
    {
        LoadSettings();
        SceneManager.sceneLoaded += CheckForSceneChange;
        Initilize();

    }
    void Initilize()
    {
       _adsController = new AdsController(this);
        _purchaseController = new PurchaseController(this);
        _purchaseController.SubscribeToPurchase();

    }
    void Update()
    {
        ScoreCount();
    }
    #region Get/Set
    public int GetCoinsAmount() => _coins;
    public float GetScoreAmount() => _score;
    public int GetScoreRecord() => _scoreRecord;
    public bool GetIsGameOver() => _isGameOver;
    public void AssignUmbrella(GameObject umbrellaObject) => umbrella = umbrellaObject;
    public PurchaseController GetPurchaseController() => _purchaseController;
    #endregion
    #region ObserverImplementation
    private List<IGameManagerObserver> gameManagerObservers = new List<IGameManagerObserver>();
    public void AddObserver(IGameManagerObserver observer)
    {
        gameManagerObservers.Add(observer);
    }
    public void RemoveObserver(IGameManagerObserver observer)
    {
        gameManagerObservers.Remove(observer);
    }
    private void NotifyObservers(IGameManagerObserver.ChooseEvent option)
    {
        foreach (IGameManagerObserver observer in gameManagerObservers)
        {
            observer.Notify(option);
        }
    }
    #endregion
    #region GameStates
    public void AddCoins(int amount)
    {
        _coins += amount;
        NotifyObservers(IGameManagerObserver.ChooseEvent.coinCollection);
    }
    public void Pause()
    {
        NotifyObservers(IGameManagerObserver.ChooseEvent.gamePause);
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        else Time.timeScale = 0f;

    }
    public void Restart()
    {
        _score = 0;
        SceneManager.LoadScene("MainScene");
        _isGameStarted = true;
        _isGameOver = false;
        NotifyObservers(IGameManagerObserver.ChooseEvent.gameRestart);
        Time.timeScale = 1f;
        StartCoroutine("ShowBannerWithDelay");
    }

    public void GameOver()
    {
        if (_score > _scoreRecord)
            _scoreRecord = Mathf.RoundToInt(_score);
        _isGameOver = true;
        CheckIfTimeForAdToShow();
        NotifyObservers(IGameManagerObserver.ChooseEvent.death);
    }

    public void AdToContinueTheGame() => _adsController.ShowRewardedAd();
    public void GameContinue()
    {
        umbrella.SetActive(true);
        PlayerController umbrellaScript = umbrella.GetComponent<PlayerController>();
        umbrellaScript.GameContinue();
        NotifyObservers(IGameManagerObserver.ChooseEvent.gameContinue);
        _isGameStarted = true;
        _isGameOver = false;
    }

    public void ChangeScene() => StartCoroutine("ChangeSceneWithDelay");
    #endregion

    void CheckForSceneChange(Scene scene1, LoadSceneMode mode)
    {
        if (scene1.buildIndex == 0)
        {
            NotifyObservers(IGameManagerObserver.ChooseEvent.menuLoaded);
        }
    }

    void CheckIfTimeForAdToShow()
    {
        _timesDied++;
        if (_timesDied >= 2)
        {
            _timesDied = 0;
            StartCoroutine("ShowAdWithDelay");
        }
    }

    private IEnumerator ChangeSceneWithDelay()
    {
        NotifyObservers(IGameManagerObserver.ChooseEvent.changeScene);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 0)
        {
            Animator officeAnimator = GameObject.Find("OfficeLevel").GetComponent<Animator>();
            officeAnimator.enabled = true;
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(1);
            _isGameStarted = true;
            StartCoroutine("ShowBannerWithDelay");
        }
        else
        {
            Pause();
            _score = 0;
            _isGameStarted = false;
            StopCoroutine("ShowBannerWithDelay");
            _adsController.HideBannerAd();
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator ShowBannerWithDelay()
    {
        yield return new WaitForSeconds(2);
        _adsController.ShowBannedAd();
    }
    private IEnumerator ShowAdWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _adsController.ShowInterstitialAd();
    }

    void ScoreCount()
    {
        if (!_isGameOver && _isGameStarted)
            _score += Time.deltaTime * _pointsMultiplier;
    }

    void DeleteOtherIntancesOfThisGameObject()
    {
        int gameManagersCount = FindObjectsOfType<GameManager>().Length;
        if (gameManagersCount != 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }

    void LoadGameData()
    {
        _coins = new LoadData().GetInt("Coins");
        _scoreRecord = new LoadData().GetInt("Score Record");
    }

    void LoadSettings()
    {
        effectsVolume = new LoadData().GetFloat("EffectsVolume");
        musicVolume = new LoadData().GetFloat("MusicVolume");
        mixer.SetFloat("EffectsVolume", effectsVolume);
        mixer.SetFloat("MusicVolume", musicVolume);
        _isVibrationOn = new LoadData().GetBool("Vibrator");
    }

    void SaveData()
    {
        new SaveData("MusicVolume", musicVolume);
        new SaveData("EffectsVolume", effectsVolume);
        new SaveData("Coins", _coins);
        new SaveData("Score Record", _scoreRecord);
        new SaveData("Vibrator", _isVibrationOn);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            SaveData();
    }

    private void OnApplicationQuit() => SaveData();
}
