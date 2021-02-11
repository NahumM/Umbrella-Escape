using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ReferenceHandler _referenceHandler;
    [SerializeField] protected float _forceOfMove;
    private Rigidbody _rb;
    private GameManager _gameManagerScript;
    private SkinManager _skinManagerScript;
    private AudioController _audioController;
    private GameObject _umbrellaCopy;
    [SerializeField] private GameObject _lHand;
    private bool _isGameOver;
    private Vector3 _currentVelocity;
    private Vector3 _highestVelocity;
    private ITouchInput touchInputScript;
    void Start()
    {
        touchInputScript = new TouchScreenHandle(this);
        _rb = GetComponent<Rigidbody>();
        _audioController = GetComponent<AudioController>();
        _gameManagerScript = _referenceHandler.GetGameManagerReference();
        _skinManagerScript = _referenceHandler.GetSkinManagerReference();
        _gameManagerScript.AssignUmbrella(this.gameObject);
        IfCloneDetachUmbrella();
        ChangeSkin();
        _rb.velocity = new Vector3(0, -4, 0);;
    }

    void Update()
    {
        HandleTouchScreen();
        HandleTestPCController();
    }

    private void FixedUpdate() => CalculateYVelocity();
    public GameManager GetGameManagerScript() => _gameManagerScript;
    public AudioController GetAudioController() => _audioController;
    public float GetForce() => _forceOfMove;

    void HandleTestPCController()
    {
        if (!_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _rb.AddForce(Vector3.forward * _forceOfMove, ForceMode.VelocityChange);
                _audioController.GetAudioSource().panStereo = 1;
                _audioController.PlaySwingSound();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _audioController.GetAudioSource().panStereo = -1;
                _rb.AddForce(Vector3.back * _forceOfMove, ForceMode.VelocityChange);
                _audioController.PlaySwingSound();
            }
        }
    }

    void CalculateYVelocity()
    {
        _currentVelocity = _rb.velocity;
        if (_currentVelocity.y < _highestVelocity.y)
            _highestVelocity = _currentVelocity;
        if (_currentVelocity.y > _highestVelocity.y) { }
          //  _rb.velocity = new Vector3(_rb.velocity.x, _highestVelocity.y, _rb.velocity.z);
    }

    void HandleTouchScreen()
    {
        if (Input.touchCount > 0 && !_isGameOver)
        {
            Touch touch = Input.GetTouch(0);
            touchInputScript.OnTouchMove(touch, transform, _rb);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        BodyPartTrigger(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        BodyPartCollision(collision);
    }

    public void BodyPartCollision(Collision collision)
    {
        if (!_isGameOver)
        {
            IPlayerContact contact = new PlayerContact();
            contact.OnContactAction(collision.gameObject, this);
        }
    }

    public void BodyPartTrigger(Collider other)
    {
        if (!_isGameOver)
        {
            IPlayerContact contact = new PlayerContact();
            contact.OnContactAction(other.gameObject, this);
        }
    }
    public void GameOver()
    {
        if (name == "Umbrella")
        {
            _isGameOver = true;
            _umbrellaCopy = Instantiate(this.gameObject);
            Destroy(_umbrellaCopy.GetComponent<PlayerController>());
            _umbrellaCopy.GetComponent<Rigidbody>().freezeRotation = false;
            _umbrellaCopy.GetComponent<Rigidbody>().velocity = _highestVelocity;
            _gameManagerScript.GameOver();
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

    void IfCloneDetachUmbrella()
    {
        if (this.gameObject.name != "Umbrella")
        {
            Destroy(_lHand.GetComponent<FixedJoint>());
        }
    }
}
