using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private ReferenceHandler _referenceHandler;
    [SerializeField] private GameObject _umbrella;
    private GameManager _gameManagerScript;
    private void Start() => _gameManagerScript = _referenceHandler.GetGameManagerReference();
    void Update()
    {
        if (!_gameManagerScript.GetIsGameOver())
        {
            Vector3 preset = new Vector3(transform.position.x, _umbrella.transform.position.y - 5, transform.position.z);
            transform.position = preset;
        }
    }
}
