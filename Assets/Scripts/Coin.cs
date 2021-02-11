public class Coin : InteractiveBehaviour
{
    protected override void Update()
    {
        transform.rotation = _rotator.transform.rotation;
        DeactivateIfOutOfBound();
    }

    protected override void DeactivateIfOutOfBound()
    {
        if (_umbrella != null)
            if (transform.position.y - _umbrella.transform.position.y > _allowedDistanceFromPlayer)
                transform.parent.gameObject.SetActive(false);
    }
}
