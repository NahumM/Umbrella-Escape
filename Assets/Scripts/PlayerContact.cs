using UnityEngine;

public class PlayerContact : IPlayerContact
{
     public void OnContactAction(GameObject contactObject, PlayerController pcontroller)
    {
        if (contactObject.CompareTag("Pickable"))
        {
            pcontroller.GetGameManagerScript().AddCoins(1);
            contactObject.SetActive(false);
        }
        if (contactObject.CompareTag("Enemy"))
        {
            contactObject.gameObject.GetComponent<InteractiveBehaviour>().StartCoroutine("DeactivateDelay");
            pcontroller.GameOver();
        }
    }
}
