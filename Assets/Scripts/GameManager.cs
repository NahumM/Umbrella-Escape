using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        int gameManagersCount = FindObjectsOfType<GameManager>().Length; 
        if (gameManagersCount != 1) Destroy(this.gameObject); 
        else DontDestroyOnLoad(this.gameObject);
        LoadGameData();
    }
    void Start()
    {
        SceneManager.sceneLoaded += CheckForSceneChange;
        DontDestroyOnLoad(this.gameObject);
        _adsController = new AdsController(this);

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
    }

    public void GameOver()
    {
        if (_score > _scoreRecord)
            _scoreRecord = Mathf.RoundToInt(_score);
        _isGameOver = true;
        CheckIfTimeForAdToShow();
        NotifyObservers(IGameManagerObserver.ChooseEvent.death);
    }

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
        }
        else
        {
            Pause();
            _score = 0;
            _isGameStarted = false;
            SceneManager.LoadScene(0);
        }
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

    void LoadGameData()
    {
        _coins = new LoadData().GetInt("Coins");
        _scoreRecord = new LoadData().GetInt("Score Record");
    }

    void SaveGameData()
    {
        new SaveData("Coins", _coins);
        new SaveData("Score Record", _scoreRecord);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            SaveGameData();
    }

    private void OnApplicationQuit() => SaveGameData();
}
