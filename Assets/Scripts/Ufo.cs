using UnityEngine;

public class Ufo : InteractiveBehaviour
{
    [SerializeField] private float _speed;
    void Update()
    {
        transform.rotation = _rotator.transform.rotation;
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
