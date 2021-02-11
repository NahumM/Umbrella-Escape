using UnityEngine;

public class PlayerContact : IPlayerContact
{
     public void OnContactAction(GameObject contactObject, PlayerController pcontroller)
    {
        VibrationHandler vibrator;
       if (pcontroller.GetGameManagerScript()._isVibrationOn)
            vibrator = new VibrationHandler();
       else vibrator = null;
        if (contactObject.CompareTag("Pickable"))
        {
            if (vibrator != null)
            {
                vibrator.Vibrate(60);
            }
            pcontroller.GetAudioController().GetAudioSource().panStereo = 0;
            pcontroller.GetAudioController().PlayCoinSound();
            pcontroller.GetGameManagerScript().AddCoins(1);
            contactObject.SetActive(false);
        }
        if (contactObject.CompareTag("Enemy"))
        {
            pcontroller.GameOver();
            if (vibrator != null)
                vibrator.Vibrate(500);
            contactObject.gameObject.GetComponent<InteractiveBehaviour>().StartCoroutine("DeactivateDelay");
        }
    }
}
