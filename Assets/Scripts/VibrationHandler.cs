using UnityEngine;

public class VibrationHandler
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public AndroidJavaClass unityPlayer;
    public AndroidJavaObject currentActivity;
    public AndroidJavaObject vibrator;
#endif
    public void Vibrate(long milliseconds)
    {
        if (isAndroid())
            vibrator.Call("vibrate", milliseconds);
        else
            Handheld.Vibrate();
    }

    public bool HasVibrator()
    {
        return isAndroid();
    }

    public void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}