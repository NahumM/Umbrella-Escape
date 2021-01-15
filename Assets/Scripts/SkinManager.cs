using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public int currentSkinNumber;
    private int previousSkinNumber;
    private bool isSkinMenuOn;
    [SerializeField]
    private List<Skin> skins;
    private UIManager uiManagerScript;
    private GameManager gameManagerScript;
    private void Awake() => LoadSkinsData();
    void Start()
    {
        if (skins[currentSkinNumber].isObtained == false)
            currentSkinNumber = 0;

            foreach (Skin skin in skins)
        {
            if (skin.id != currentSkinNumber) 
                skin.skinGameObject.SetActive(false);
            else skin.skinGameObject.SetActive(true);
        }
        uiManagerScript = GameObject.FindObjectOfType<UIManager>();
        gameManagerScript = GameObject.FindObjectOfType<GameManager>();
        previousSkinNumber = currentSkinNumber;
    }

    public void TurnSkinMenuOn()
    {
        if (isSkinMenuOn == false)
        {
            UpdateSkinStoreUI();
            isSkinMenuOn = true;
        } else
        {
            skins[currentSkinNumber].skinGameObject.SetActive(false);
            currentSkinNumber = previousSkinNumber;
            skins[currentSkinNumber].skinGameObject.SetActive(true);
            uiManagerScript.CloseSkinStoreUI();
            isSkinMenuOn = false;
        }
    }

    private void UpdateSkinStoreUI()
    {
        uiManagerScript.UpdateSkinStoreUI(skins[currentSkinNumber]);
    }

    public void NextSkin()
    {
        if (currentSkinNumber < skins.Count - 1)
        {
            skins[currentSkinNumber].skinGameObject.SetActive(false);
            currentSkinNumber++;
            skins[currentSkinNumber].skinGameObject.SetActive(true);
            UpdateSkinStoreUI();
        }
    }
    public void PreviousSkin()
    {
        if (currentSkinNumber > 0)
        {
            skins[currentSkinNumber].skinGameObject.SetActive(false);
            currentSkinNumber--;
            skins[currentSkinNumber].skinGameObject.SetActive(true);
            UpdateSkinStoreUI();
        }
    }

    public void PurchaseSkin()
    {
        if (gameManagerScript.coins > skins[currentSkinNumber].price && skins[currentSkinNumber].isObtained == false)
        {
            gameManagerScript.AddCoins(-skins[currentSkinNumber].price);
            skins[currentSkinNumber].isObtained = true;
            UpdateSkinStoreUI();
        }
    }

    public void EquipSkin()
    {
        if (skins[currentSkinNumber].isObtained == true)
        {
            uiManagerScript.CloseSkinStoreUI();
            previousSkinNumber = currentSkinNumber;
        }
    }

    private void SaveSkinsData()
    {
        foreach (Skin skin in skins)
        {
            PlayerPrefs.SetInt("Skin" + skin.id, boolToInt(skin.isObtained));
        }
        if (skins[currentSkinNumber].isObtained == true)
        PlayerPrefs.SetInt("CurrentSkin", currentSkinNumber);
        else PlayerPrefs.SetInt("CurrentSkin", previousSkinNumber);
        PlayerPrefs.Save();
    }

    private void LoadSkinsData()
    {
        foreach(Skin skin in skins)
        {
            skin.isObtained = intToBool(PlayerPrefs.GetInt("Skin" + skin.id));    
        }
        currentSkinNumber = PlayerPrefs.GetInt("CurrentSkin");

    }

    int boolToInt(bool answer)
    {
        if (answer) return 1;
        else return 0;
    }

    bool intToBool(int answer)
    {
        if (answer == 1) return true;
        else return false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            SaveSkinsData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSkinsData();
    }



}
