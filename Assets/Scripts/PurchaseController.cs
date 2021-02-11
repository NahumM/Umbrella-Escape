using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class PurchaseController
{
    GameManager gameManagerScript;
    public PurchaseController(GameManager gm)
        {
        IAPProduct[] products = InAppPurchasing.GetAllIAPProducts();
        gameManagerScript = gm;
    }

    public void SubscribeToPurchase()
    {
        InAppPurchasing.PurchaseCompleted += PurchaseCompleteHandle;
        InAppPurchasing.PurchaseFailed += PurchaseFailedHandle;
    }

    public void PurchaseGoldPack(string product)
    {
        InAppPurchasing.Purchase(product);
    }

    void PurchaseCompleteHandle(IAPProduct product)
    {
        switch (product.Name)
        {
            case EM_IAPConstants.Product_Gold__1000:
                gameManagerScript.AddCoins(1000);
                break;
            case EM_IAPConstants.Product_Gold__2000:
                gameManagerScript.AddCoins(2000);
                break;
            case EM_IAPConstants.Product_Gold__4000:
                gameManagerScript.AddCoins(4000);
                break;
            case EM_IAPConstants.Product_Gold__6000:
                gameManagerScript.AddCoins(6000);
                break;
            case EM_IAPConstants.Product_Gold__8000:
                gameManagerScript.AddCoins(8000);
                break;
        }
    }

    void PurchaseFailedHandle(IAPProduct product, string failureReason)
    {
        NativeUI.Alert("Error", "The purchase of product " + product.Name + "has failed. Failure reason - " + failureReason);
    }
}
