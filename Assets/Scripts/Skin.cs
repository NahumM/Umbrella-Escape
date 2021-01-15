using UnityEngine;

[System.Serializable]
public class Skin
{
    public string name;
    public int id;
    public int price;
    public int requiredScore;
    public bool isObtained;
    public GameObject skinGameObject;
    public Skin(string name, int id, int price, int requiredScore, bool isObtained, GameObject skinGameObject)
    {
        this.name = name;
        this.id = id;
        this.price = price;
        this.requiredScore = requiredScore;
        this.isObtained = isObtained;
        this.skinGameObject = skinGameObject;
    }
}
