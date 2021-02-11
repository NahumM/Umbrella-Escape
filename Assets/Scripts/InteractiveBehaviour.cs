using System.Collections;
using UnityEngine;

public class InteractiveBehaviour : MonoBehaviour
{
    protected GameObject _umbrella;
    protected GameObject _rotator;
    [SerializeField] protected float _allowedDistanceFromPlayer;
    virtual protected void Update()
    {
        DeactivateIfOutOfBound();
    }
    virtual protected void DeactivateIfOutOfBound()
    {
        if (_umbrella != null)
            if (transform.position.y - _umbrella.transform.position.y > _allowedDistanceFromPlayer)
                this.gameObject.SetActive(false);
    }

    public void AssignGameObjects(GameObject umbrella, GameObject rotator)
    {
        _umbrella = umbrella;
        _rotator = rotator;
    }

    protected IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
}
