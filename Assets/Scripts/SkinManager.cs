using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour, IGameManagerObserver
{
    public int _currentSkinNumber { get; private set; }
    private int _previousSkinNumber;
    private bool _isSkinMenuOn;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private UIManager _uiManagerScript;
    [SerializeField] private GameManager _gameManagerScript;
    void Start()
    {
        LoadSkinsData();
        _gameManagerScript.AddObserver(this);
        if (_skins[_currentSkinNumber].isObtained == false)
            _currentSkinNumber = 0;
        EquipCurrentClothes();
        _previousSkinNumber = _currentSkinNumber;
    }

    public void TurnSkinMenuOn()
    {
        if (_isSkinMenuOn == false)
        {
            UpdateSkinStoreUI();
            _isSkinMenuOn = true;
        } else
        {
            _skins[_currentSkinNumber].skinGameObject.SetActive(false);
            _currentSkinNumber = _previousSkinNumber;
            _skins[_currentSkinNumber].skinGameObject.SetActive(true);
            _uiManagerScript.CloseSkinStoreUI();
            _isSkinMenuOn = false;
        }
    }
    public void Notify(IGameManagerObserver.ChooseEvent option)
    {
        switch(option)
        {
            case IGameManagerObserver.ChooseEvent.menuLoaded:
                EquipCurrentClothes();
                break;
        }
    }

    private void UpdateSkinStoreUI()
    {
        _uiManagerScript.UpdateSkinStoreUI(_skins[_currentSkinNumber]);
    }

    public void NextSkin()
    {
        if (_currentSkinNumber < _skins.Count - 1)
        {
            _skins[_currentSkinNumber].skinGameObject.SetActive(false);
            _currentSkinNumber++;
            _skins[_currentSkinNumber].skinGameObject.SetActive(true);
            UpdateSkinStoreUI();
        }
    }
    public void PreviousSkin()
    {
        if (_currentSkinNumber > 0)
        {
            _skins[_currentSkinNumber].skinGameObject.SetActive(false);
            _currentSkinNumber--;
            _skins[_currentSkinNumber].skinGameObject.SetActive(true);
            UpdateSkinStoreUI();
        }
    }

    public void PurchaseSkin()
    {
        if (_gameManagerScript.GetCoinsAmount() > _skins[_currentSkinNumber].price && _skins[_currentSkinNumber].isObtained == false)
        {
            _gameManagerScript.AddCoins(-_skins[_currentSkinNumber].price);
            _skins[_currentSkinNumber].isObtained = true;
            UpdateSkinStoreUI();
        }
    }

    public void EquipSkin()
    {
        if (_skins[_currentSkinNumber].isObtained == true)
        {
            _uiManagerScript.CloseSkinStoreUI();
            _previousSkinNumber = _currentSkinNumber;
        }
    }

    void AssignGameObjects()
    {
        Transform character = GameObject.Find("Character_BusinessMan_Shirt_01").transform;
        foreach (Skin skin in _skins)
        {
            skin.skinGameObject = character.GetChild(skin.id).gameObject;
        }
    }

    void EquipCurrentClothes()
    {
        if (_skins[_currentSkinNumber].skinGameObject == null)
            AssignGameObjects();
        foreach (Skin skin in _skins)
        {
            if (skin.id != _currentSkinNumber)
            {
                skin.skinGameObject.SetActive(false);
            }
            else skin.skinGameObject.SetActive(true);
        }
    }

    private void SaveSkinsData()
    {
        foreach (Skin skin in _skins)
        {
            new SaveData("Skin" + skin.id, skin.isObtained);
        }
        if (_skins[_currentSkinNumber].isObtained)
            new SaveData("CurrentSkin", _currentSkinNumber);
        else new SaveData("CurrentSkin", _previousSkinNumber);
    }

    private void LoadSkinsData()
    {
        foreach(Skin skin in _skins)
            skin.isObtained = new LoadData().GetBool("Skin" + skin.id);
        _currentSkinNumber = new LoadData().GetInt("CurrentSkin");
    }

    private void OnApplicationQuit() => SaveSkinsData();


    void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveSkinsData();
    }
}
