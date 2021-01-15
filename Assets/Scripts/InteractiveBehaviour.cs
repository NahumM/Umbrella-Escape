using System.Collections;
using UnityEngine;

public class InteractiveBehaviour : MonoBehaviour
{
    private GameObject umbrella;
    private GameObject rotator;
    [SerializeField]
    private float allowedDistanceFromPlayer;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float speed;
    void Start()
    {
        umbrella = GameObject.Find("Umbrella");
        rotator = GameObject.Find("Coin Rotator");
    }

    void Update()
    {
        if (CompareTag("Ufo") || CompareTag("Coin"))
        transform.rotation = rotator.transform.rotation;
        DeactivateIfOutOfBound();
        if (CompareTag("Ufo"))
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void DeactivateIfOutOfBound()
    {
        if (umbrella != null)
            if (transform.position.y - umbrella.transform.position.y > allowedDistanceFromPlayer)
                this.gameObject.SetActive(false);
    }

    private IEnumerator DeactivateDelay()
    { 
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
