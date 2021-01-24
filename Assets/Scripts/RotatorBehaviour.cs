using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    void Update() => transform.Rotate(Vector3.up * (_rotationSpeed * Time.deltaTime));
}
