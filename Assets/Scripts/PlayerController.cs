using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ReferenceHandler _referenceHandler;
    [SerializeField] protected float _forceOfMove;
    private Rigidbody _rb;
    private GameManager _gameManagerScript;
    private SkinManager _skinManagerScript;
    private GameObject _umbrellaCopy;
    private bool _isGameOver;
    private Vector3 _currentVelocity;
    private Vector3 _highestVelocity;
    private ITouchInput touchInputScript;
    void Start()
    {
        touchInputScript = new TouchScreenHandle();
        _rb = GetComponent<Rigidbody>();
        _gameManagerScript = _referenceHandler.GetGameManagerReference();
        _skinManagerScript = _referenceHandler.GetSkinManagerReference();
        _gameManagerScript.AssignUmbrella(this.gameObject);
        _rb.velocity = new Vector3(0, -3, 0);
        ChangeSkin();
    }

    void Update()
    {
        HandleTouchScreen();
        HandleTestPCController();
    }

    private void FixedUpdate() => CalculateYVelocity();
    public GameManager GetGameManagerScript() => _gameManagerScript;

    void HandleTestPCController()
    {
        if (!_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                _rb.AddForce(Vector3.forward * _forceOfMove, ForceMode.VelocityChange);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                _rb.AddForce(Vector3.back * _forceOfMove, ForceMode.VelocityChange);
        }
    }

    void CalculateYVelocity()
    {
        _currentVelocity = _rb.velocity;
        if (_currentVelocity.y < _highestVelocity.y)
            _highestVelocity = _currentVelocity;
    }

    void HandleTouchScreen()
    {
        if (Input.touchCount > 0 && !_isGameOver)
        {
            Touch touch = Input.GetTouch(0);
            touchInputScript.OnTouchMove(touch, transform, _rb, _forceOfMove);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isGameOver)
        {
            IPlayerContact contact = new PlayerContact();
            contact.OnContactAction(other.gameObject, this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!_isGameOver)
        {
            IPlayerContact contact = new PlayerContact();
            contact.OnContactAction(collision.gameObject, this);
        }
    }
    public void GameOver()
    {
        if (name == "Umbrella")
        {
            _umbrellaCopy = Instantiate(this.gameObject);
            Destroy(_umbrellaCopy.GetComponent<PlayerController>());
            _umbrellaCopy.GetComponent<Rigidbody>().velocity = _rb.velocity;
            _gameManagerScript.GameOver();
            _isGameOver = true;
            this.gameObject.SetActive(false);
        }
    }

    private void ChangeSkin()
    {
        for (int i = 0; i < this.gameObject.transform.GetChild(0).childCount - 1; i++)
        {
            if (i == _skinManagerScript._currentSkinNumber)
                this.gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            else this.gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GameContinue()
    {
        _rb.velocity = _highestVelocity;
        _umbrellaCopy.SetActive(false);
        _isGameOver = false;
    }
}
