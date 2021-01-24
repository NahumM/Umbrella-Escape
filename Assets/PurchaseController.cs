using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class PurchaseController
{
    public PurchaseController()
        {
        IAPProduct[] products = InAppPurchasing.GetAllIAPProducts();

        foreach (IAPProduct product in products)
        {
            Debug.Log("Product name" + product.Name);
        }

        InAppPurchasing.Purchase(EM_IAPConstants.Product_Gold__1500);
    }
}
