using System.Collections;
using UnityEngine;
using TMPro;
using EasyMobile;

public class UIManager : MonoBehaviour, IGameManagerObserver
{
    private GameManager gameManagerScript;
    [SerializeField]
    private GameObject
        coinTextUI,
        scoreTextUI,
        exitButtonUI,
        pauseMenuUI,
        restartMenuUI,
        settingsMenuUI,
        mainMenuUI,
        skinStoreUI,
        skinNameText,
        skinPriceText,
        skinRequirementsText,
        storeUI,
        storeExitButtonUI,
        menuTitleUI,
        storeMarkUI,
        startButtonUI;
    private bool isGameOver;

    void Awake()
    {
        int uiManagersCount = FindObjectsOfType<UIManager>().Length;
        if (uiManagersCount != 1)
            Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        isGameOver = true;
        gameManagerScript = FindObjectOfType<GameManager>();
        if (gameManagerScript != null)
            gameManagerScript.AddObserver(this);
        DisplayCoinAmounts();
        ShowRecord();
    }
    void Update()
    {
        if (!isGameOver)
            scoreTextUI.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(gameManagerScript.score).ToString();
    }

    public void Notify(IGameManagerObserver.ChooseEvent option)
    {
        if (option == IGameManagerObserver.ChooseEvent.coinCollection)
            DisplayCoinAmounts();
        if (option == IGameManagerObserver.ChooseEvent.gamePause)
            PauseMenuUI();
        if (option == IGameManagerObserver.ChooseEvent.changeScene)
            MainMenuUI();
        if (option == IGameManagerObserver.ChooseEvent.death)
        {
            isGameOver = true;
            StartCoroutine("RestartMenuUI");
        }
        if (option == IGameManagerObserver.ChooseEvent.gameRestart)
        {
            StartCoroutine("RestartMenuUI");
            isGameOver = false;
        }
        if (option == IGameManagerObserver.ChooseEvent.gameContinue)
        {
            isGameOver = false;
            StartCoroutine("RestartMenuUI");
        }
    }
    private void PauseMenuUI()
    {
        if (pauseMenuUI.activeInHierarchy == false)
            pauseMenuUI.SetActive(true);
        else pauseMenuUI.SetActive(false);
    }

    private void SettingsMenuUI()
    {
        if (settingsMenuUI.activeInHierarchy == false)
            settingsMenuUI.SetActive(true);
        else settingsMenuUI.SetActive(false);

    }

    private void MainMenuUI()
    {
        if (mainMenuUI.activeInHierarchy == false)
        {
            StopCoroutine("RestartMenuUI");
            exitButtonUI.SetActive(false);
            mainMenuUI.SetActive(true);
            isGameOver = true;
            ShowRecord();
        }
        else
        {
            scoreTextUI.GetComponent<TextMeshProUGUI>().color = Color.white;
            exitButtonUI.SetActive(true);
            mainMenuUI.SetActive(false);
            isGameOver = false;
        }
    }

    public void UpdateSkinStoreUI(Skin skin)
    {
        if (storeUI.activeInHierarchy == true) StoreUI();
        if (skinStoreUI.activeInHierarchy == false)
        {
            skinStoreUI.SetActive(true);
            startButtonUI.SetActive(false);
        }
        skinNameText.GetComponent<TextMeshProUGUI>().text = skin.name;
        if (skin.isObtained != true) skinPriceText.GetComponent<TextMeshProUGUI>().text = skin.price.ToString();
        else skinPriceText.GetComponent<TextMeshProUGUI>().text = "Purchased";
        skinRequirementsText.GetComponent<TextMeshProUGUI>().text = "Earn " + skin.requiredScore.ToString() + " points in one game";
        if (gameManagerScript.scoreRecord >= skin.requiredScore)
            storeMarkUI.SetActive(true);
        else    storeMarkUI.SetActive(false);
    }

    public void CloseSkinStoreUI()
    {
        skinStoreUI.SetActive(false);
        startButtonUI.SetActive(true);
    }

    public void DisplayCoinAmounts()
    {
        coinTextUI.GetComponent<TextMeshProUGUI>().text = gameManagerScript.coins.ToString();
    }

    private void ShowRecord()
    {

        scoreTextUI.GetComponent<TextMeshProUGUI>().text = gameManagerScript.scoreRecord.ToString();
        scoreTextUI.GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }

    public void StoreUI()
    {
        if (isGameOver == true)
        {
            if (skinStoreUI.activeInHierarchy == true) CloseSkinStoreUI();
            if (storeUI.activeInHierarchy == false)
            {
                storeUI.SetActive(true);
                storeExitButtonUI.SetActive(true);
                menuTitleUI.SetActive(false);
            }
            else
            {
                storeUI.SetActive(false);
                storeExitButtonUI.SetActive(false);
                menuTitleUI.SetActive(true);
            }
        }
    }

    private IEnumerator RestartMenuUI()
    {
        if (restartMenuUI.activeInHierarchy == false && pauseMenuUI.activeInHierarchy == false)
        {
            yield return new WaitForSeconds(2f);
            restartMenuUI.SetActive(true);
        } else
        {
            restartMenuUI.SetActive(false);
        }
        pauseMenuUI.SetActive(false);
    }

    public void PlayRewardedAd()
    {
        Advertising.ShowRewardedAd();
    }
}
