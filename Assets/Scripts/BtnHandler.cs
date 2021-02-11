using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class BtnHandler : MonoBehaviour
{
    [SerializeField] GameManager _gameManagerScript;
    void ButtonHandle(GameObject button)
    {
        // buttons
    }

    public void PurchaseButtonHandle(GameObject button)
    {
        switch (button.name)
        {
            case "Item_Gold1":
                _gameManagerScript.GetPurchaseController().PurchaseGoldPack(EM_IAPConstants.Product_Gold__1000);
                break;
            case "Item_Gold2":
                _gameManagerScript.GetPurchaseController().PurchaseGoldPack(EM_IAPConstants.Product_Gold__2000);
                break;
            case "Item_Gold3":
                _gameManagerScript.GetPurchaseController().PurchaseGoldPack(EM_IAPConstants.Product_Gold__4000);
                break;
            case "Item_Gold4":
                _gameManagerScript.GetPurchaseController().PurchaseGoldPack(EM_IAPConstants.Product_Gold__6000);
                break;
            case "Item_Gold5":
                _gameManagerScript.GetPurchaseController().PurchaseGoldPack(EM_IAPConstants.Product_Gold__8000);
                break;
        }
    }
}
