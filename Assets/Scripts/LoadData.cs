using UnityEngine;

public class LoadData
{
    public int GetInt(string key)
    {
      int value = PlayerPrefs.GetInt(key);
        return value;
    }
    public float GetFloat(string key)
    {
        float value = PlayerPrefs.GetFloat(key);
        return value;
    }
    public bool GetBool(string key)
    {
       bool answer;
       int value = PlayerPrefs.GetInt(key);
        if (value == 1) answer = true;
            else answer = false;
       return answer;
    }
}
