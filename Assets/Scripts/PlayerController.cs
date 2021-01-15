using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float force;
    private Vector3 positionToMove;
    private GameManager gameManagerScript;
    private SkinManager skinManagerScript;
    [HideInInspector]
    public GameObject umbrellaCopy;
    public bool isGameOver;
    private Vector3 currentVelocity;
    private Vector3 highestVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.FindObjectOfType<GameManager>();
        skinManagerScript = GameObject.FindObjectOfType<SkinManager>();
        gameManagerScript.umbrella = this.gameObject;
        rb.velocity = new Vector3(0, -3, 0);
        ChangeSkin();
    }

    void Update()
    {
        HandleTouchScreen();
        HandleTestPCController();
    }

    private void FixedUpdate() => CalculateYVelocity();

    void HandleTestPCController()
    {
        if (!isGameOver)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                rb.AddForce(Vector3.forward * force, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.LeftArrow))
                rb.AddForce(Vector3.back * force, ForceMode.Acceleration);
        }
    }

    void CalculateYVelocity()
    {
        currentVelocity = rb.velocity;
        if (currentVelocity.y < highestVelocity.y)
            highestVelocity = currentVelocity;
    }

    void HandleTouchScreen()
    {
        if (Input.touchCount > 0 && !isGameOver)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Plane plane = new Plane(Vector3.right, transform.position);
                float distance;
                if (plane.Raycast(ray, out distance))
                    positionToMove = ray.GetPoint(distance);
                float directionZ = positionToMove.z - transform.position.z;
                Vector3 direction = new Vector3(0, 0, directionZ);
                rb.AddForce(direction * force, ForceMode.VelocityChange);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver)
        {
            if (other.CompareTag("Coin"))
            {
                gameManagerScript.AddCoins(1);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.CompareTag("Black Cloud") && !isGameOver)
            {
                other.gameObject.GetComponent<InteractiveBehaviour>().StartCoroutine("DeactivateDelay");
                GameOver();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ufo") && !isGameOver)
        {
            collision.gameObject.GetComponent<InteractiveBehaviour>().StartCoroutine("DeactivateDelay");
            GameOver();
        }
    }
    private void GameOver()
    {
        if (name == "Umbrella")
        {
            umbrellaCopy = Instantiate(this.gameObject);
            Destroy(umbrellaCopy.GetComponent<PlayerController>());
            umbrellaCopy.GetComponent<Rigidbody>().velocity = rb.velocity;
            gameManagerScript.GameOver();
            isGameOver = true;
            this.gameObject.SetActive(false);
        }
    }

    private void ChangeSkin()
    {
        for (int i = 0; i < this.gameObject.transform.GetChild(0).childCount - 1; i++)
        {
            Debug.Log(this.gameObject.transform.childCount);
            if (i == skinManagerScript.currentSkinNumber)
                this.gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            else this.gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GameContinue()
    {
        rb.velocity = highestVelocity;
        umbrellaCopy.SetActive(false);
        isGameOver = false;
    }
}
