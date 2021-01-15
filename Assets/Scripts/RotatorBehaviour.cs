using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    void Update() => transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
}
