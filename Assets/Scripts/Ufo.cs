using UnityEngine;

public class Ufo : InteractiveBehaviour
{
    [SerializeField] private float _speed;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    override protected void Update()
    {
        transform.rotation = _rotator.transform.rotation;
        //transform.Translate(Vector3.up * _speed * Time.deltaTime);
        DeactivateIfOutOfBound();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.up * _speed * Time.fixedDeltaTime);
    }
}
