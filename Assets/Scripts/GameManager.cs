using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int coins;
    [HideInInspector]
    public float score;
    [HideInInspector]
    public int scoreRecord;
    [SerializeField]
    private float pointsMultiplier;
    private float timesDied;
    private bool isGameOver;
    private bool isGameStarted;
    public int currentSkinNumber;
    [HideInInspector]
    public GameObject umbrella;
    private AdsController adsController;
    void Start()
    {
        SceneManager.sceneLoaded += CheckForSceneChange;
        int gameManagersCount = FindObjectsOfType<GameManager>().Length;
        if (gameManagersCount != 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
        LoadGameData();
        adsController = GameObject.FindObjectOfType<AdsController>();
    }
    void Update()
    {
        ScoreCount();
        if (timesDied >= 2)
        {
            timesDied = 0;
            StartCoroutine("ShowAdWithDelay");
        }
    }

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
        coins += amount;
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
        score = 0;
        SceneManager.LoadScene("MainScene");
        isGameStarted = true;
        isGameOver = false;
        NotifyObservers(IGameManagerObserver.ChooseEvent.gameRestart);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (score > scoreRecord)
            scoreRecord = Mathf.RoundToInt(score);
        isGameOver = true;
        timesDied++;
        NotifyObservers(IGameManagerObserver.ChooseEvent.death);
    }

    public void GameContinue()
    {
        umbrella.SetActive(true);
        PlayerController umbrellaScript = umbrella.GetComponent<PlayerController>();
        umbrellaScript.GameContinue();
        NotifyObservers(IGameManagerObserver.ChooseEvent.gameContinue);
        isGameStarted = true;
        isGameOver = false;
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
            isGameStarted = true;
        }
        else
        {
            Pause();
            score = 0;
            isGameStarted = false;
            SceneManager.LoadScene(0);
        }
    }
    private IEnumerator ShowAdWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        adsController.ShowInterstitialAd();
    }

    void ScoreCount()
    {
        if (!isGameOver && isGameStarted)
            score += Time.deltaTime * pointsMultiplier;
    }

    void LoadGameData()
    {
        coins = PlayerPrefs.GetInt("Coins");
        scoreRecord = PlayerPrefs.GetInt("Score Record");
    }

    void SaveGameData()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Score Record", scoreRecord);
        PlayerPrefs.Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            SaveGameData();
    }

    private void OnApplicationQuit() => SaveGameData();
}
