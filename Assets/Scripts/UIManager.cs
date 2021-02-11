using System.Collections;
using UnityEngine;
using TMPro;
using EasyMobile;

public class UIManager : MonoBehaviour, IGameManagerObserver
{
    [SerializeField]
    private GameManager _gameManagerScript;
    [SerializeField]
    private GameObject
        _coinTextUI,
        _scoreTextUI,
        _exitButtonUI,
        _pauseMenuUI,
        _restartMenuUI,
        _settingsMenuUI,
        _mainMenuUI,
        _skinStoreUI,
        _skinNameText,
        _skinPriceText,
        _skinRequirementsText,
        _storeUI,
        _storeExitButtonUI,
        _menuTitleUI,
        _storeMarkUI,
        _startButtonUI;
    private bool _isGameOver;

    void Awake()
    {
        DeleteOtherInstancesOfThisGameObject();
    }
    void Start()
    {
        _isGameOver = true;
        if (_gameManagerScript != null)
            _gameManagerScript.AddObserver(this);
        DisplayCoinAmounts();
        ShowRecord();
    }
    void Update()
    {
        if (!_isGameOver)
            _scoreTextUI.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(_gameManagerScript.GetScoreAmount()).ToString();
    }

    public void Notify(IGameManagerObserver.ChooseEvent option)
    {
        switch(option)
        {
            case IGameManagerObserver.ChooseEvent.coinCollection:
                DisplayCoinAmounts();
                break;
            case IGameManagerObserver.ChooseEvent.gamePause:
                PauseMenuUI();
                break;
            case IGameManagerObserver.ChooseEvent.changeScene:
                MainMenuUI();
                break;
            case IGameManagerObserver.ChooseEvent.death:
                _isGameOver = true;
                StartCoroutine("RestartMenuUI");
                break;
            case IGameManagerObserver.ChooseEvent.gameRestart:
                StartCoroutine("RestartMenuUI");
                _isGameOver = false;
                break;
            case IGameManagerObserver.ChooseEvent.gameContinue:
                _isGameOver = false;
                StartCoroutine("RestartMenuUI");
                break;

        }
    }
    private void PauseMenuUI()
    {
        if (_pauseMenuUI.activeInHierarchy == false)
            _pauseMenuUI.SetActive(true);
        else _pauseMenuUI.SetActive(false);
    }

    private void SettingsMenuUI()
    {
        if (_settingsMenuUI.activeInHierarchy == false)
            _settingsMenuUI.SetActive(true);
        else _settingsMenuUI.SetActive(false);

    }

    private void MainMenuUI()
    {
        if (_mainMenuUI.activeInHierarchy == false)
        {
            StopCoroutine("RestartMenuUI");
            _exitButtonUI.SetActive(false);
            _mainMenuUI.SetActive(true);
            _isGameOver = true;
            ShowRecord();
        }
        else
        {
            _scoreTextUI.GetComponent<TextMeshProUGUI>().color = Color.white;
            _exitButtonUI.SetActive(true);
            _mainMenuUI.SetActive(false);
            _isGameOver = false;
        }
    }

    public void UpdateSkinStoreUI(Skin skin)
    {
        if (_storeUI.activeInHierarchy == true) StoreUI();
        if (_skinStoreUI.activeInHierarchy == false)
        {
            _skinStoreUI.SetActive(true);
            _startButtonUI.SetActive(false);
        }
        _skinNameText.GetComponent<TextMeshProUGUI>().text = skin.name;
        if (skin.isObtained != true) _skinPriceText.GetComponent<TextMeshProUGUI>().text = skin.price.ToString();
        else _skinPriceText.GetComponent<TextMeshProUGUI>().text = "Purchased";
        _skinRequirementsText.GetComponent<TextMeshProUGUI>().text = "Earn " + skin.requiredScore.ToString() + " points in one game";
        if (_gameManagerScript.GetScoreRecord() >= skin.requiredScore)
            _storeMarkUI.SetActive(true);
        else    _storeMarkUI.SetActive(false);
    }

    public void CloseSkinStoreUI()
    {
        _skinStoreUI.SetActive(false);
        _startButtonUI.SetActive(true);
    }

    public void DisplayCoinAmounts()
    {
        _coinTextUI.GetComponent<TextMeshProUGUI>().text = _gameManagerScript.GetCoinsAmount().ToString();
    }

    private void ShowRecord()
    {

        _scoreTextUI.GetComponent<TextMeshProUGUI>().text = _gameManagerScript.GetScoreRecord().ToString();
        _scoreTextUI.GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }

    public void StoreUI()
    {
        if (_isGameOver == true)
        {
            if (_skinStoreUI.activeInHierarchy == true) CloseSkinStoreUI();
            if (_storeUI.activeInHierarchy == false)
            {
                _storeUI.SetActive(true);
                _storeExitButtonUI.SetActive(true);
                _menuTitleUI.SetActive(false);
            }
            else
            {
                _storeUI.SetActive(false);
                _storeExitButtonUI.SetActive(false);
                _menuTitleUI.SetActive(true);
            }
        }
    }

    private IEnumerator RestartMenuUI()
    {
        if (_restartMenuUI.activeInHierarchy == false && _pauseMenuUI.activeInHierarchy == false)
        {
            yield return new WaitForSeconds(2f);
            _restartMenuUI.SetActive(true);
        } else
        {
            _restartMenuUI.SetActive(false);
        }
        _pauseMenuUI.SetActive(false);
    }

    public void PlayRewardedAd()
    {
        Advertising.ShowRewardedAd();
    }

    void DeleteOtherInstancesOfThisGameObject()
    {
        int uiManagersCount = FindObjectsOfType<UIManager>().Length;
        if (uiManagersCount != 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }
}
