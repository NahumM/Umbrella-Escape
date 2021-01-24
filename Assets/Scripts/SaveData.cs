using UnityEngine;

public class SaveData
{
    public SaveData(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
    public SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
    public SaveData(string key, bool value)
    {
        int answer;
        if (value)
            answer = 1;
        else answer = 0;
        PlayerPrefs.SetInt(key, answer);
        PlayerPrefs.Save();
    }

}
