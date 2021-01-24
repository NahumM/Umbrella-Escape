using UnityEngine;
public interface ITouchInput
{
    public void OnTouchMove(Touch touch, Transform playerTransform, Rigidbody rb, float force);
}
