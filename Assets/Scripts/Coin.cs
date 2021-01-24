public class Coin : InteractiveBehaviour
{
    void Update() => transform.rotation = _rotator.transform.rotation;

    protected override void DeactivateIfOutOfBound()
    {
        if (_umbrella != null)
            if (transform.position.y - _umbrella.transform.position.y > _allowedDistanceFromPlayer)
                transform.parent.gameObject.SetActive(false);
    }
}
