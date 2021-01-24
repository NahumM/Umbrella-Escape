using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, IGameManagerObserver
{
    [Header("Spawn Manager Settings")]
    private GameManager _gameManagerScript;
    [SerializeField] private ReferenceHandler _referenceHandler;
    [SerializeField] private GameObject _umbrella;
    [SerializeField] private GameObject _rotator;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _ufoPrefab;
    [SerializeField] private GameObject _blackCloudPrefab;
    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _minZSpawn;
    [SerializeField] private float _maxZSpawn;
    [Header("Black Clouds Settings")]
    [SerializeField] private float _minBlackCloudTimeToSpawn;
    [SerializeField] private float _maxBlackCloudTimeToSpawn;
    [Header("Ufos Settings")]
    [SerializeField] private float _minUfoTimeToSpawn;
    [SerializeField] private float _maxUfoTimeToSpawn;
    [Header("Coins Settings")]
    [SerializeField] private float _coinSpawnStep;

    private Vector3 playerPosition;
    private Vector3 spawnerPreviousPosition;
    private float nextZPosition;
    private List<GameObject> coinsPool = new List<GameObject>();
    private List<GameObject> ufoPool = new List<GameObject>();
    private List<GameObject> blackCloudsPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _gameManagerScript = _referenceHandler.GetGameManagerReference();
        if (_gameManagerScript != null)
            _gameManagerScript.AddObserver(this);
        nextZPosition = Random.Range(_minZSpawn + 1, _maxZSpawn - 1);
        spawnerPreviousPosition = transform.position;
        StartCoroutine("UfoSpawner");
        StartCoroutine("BlackCloudsSpawner");
    }

    void Update()
    {
        HandleSpawnerPosition();
        SpawnCoins();
    }

    void HandleSpawnerPosition()
    {
        playerPosition = _umbrella.transform.position;
        Vector3 spawnerNewPosition = new Vector3(spawnerPreviousPosition.x, playerPosition.y - _distanceFromPlayer, spawnerPreviousPosition.z);
        transform.position = spawnerNewPosition;
    }
    private GameObject GetObjectToPool(List<GameObject> objectPool, GameObject prefab)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }
        GameObject newObject = Instantiate(prefab);
        InteractiveBehaviour objectScript;
        if (newObject.CompareTag("Pickable"))
            objectScript = newObject.transform.GetChild(0).GetComponent<InteractiveBehaviour>();
        else objectScript = newObject.GetComponent<InteractiveBehaviour>();
        objectScript.AssignGameObjects(_umbrella, _rotator);      
        objectPool.Add(newObject);
        return newObject;
    }

    IEnumerator UfoSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minUfoTimeToSpawn, _maxUfoTimeToSpawn));
            GetObjectToPool(ufoPool, _ufoPrefab).transform.position = RandomSpawnPosition();
        }
    }

    IEnumerator BlackCloudsSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minBlackCloudTimeToSpawn, _maxBlackCloudTimeToSpawn));
            GetObjectToPool(blackCloudsPool, _blackCloudPrefab).transform.position = RandomSpawnPosition();
        }
    }

    void SpawnCoins()
    {
        if (spawnerPreviousPosition.y - transform.position.y >= _coinSpawnStep)
        {
            GetObjectToPool(coinsPool, _coinPrefab).transform.position = RandomZStep();
            spawnerPreviousPosition.y = transform.position.y;
        }
    }

    Vector3 RandomZStep()
    {
        if (nextZPosition > _minZSpawn && nextZPosition < _maxZSpawn)
            nextZPosition += Random.Range(0, 2) * 2 - 1;
        else if (nextZPosition <= _minZSpawn)
            nextZPosition += 1;
        else if (nextZPosition >= _maxZSpawn)
            nextZPosition -= 1;
        Vector3 randomZStep = new Vector3(transform.position.x, transform.position.y, nextZPosition);
        return randomZStep;
    }

    Vector3 RandomSpawnPosition()
    {
       Vector3 result = new Vector3(transform.position.x, transform.position.y, Random.Range(_minZSpawn, _maxZSpawn));
       return result;
    }
    float RandomSeconds(float min, float max)
    {
        float result = Random.Range(min, max);
        return result;
    }

    public void Notify(IGameManagerObserver.ChooseEvent option)
    {
        switch(option)
        {
            case IGameManagerObserver.ChooseEvent.death:
                StopAllCoroutines();
                break;
            case IGameManagerObserver.ChooseEvent.gameContinue:
                StartCoroutine("UfoSpawner");
                StartCoroutine("BlackCloudsSpawner");
                break;
        }
    }
    private void OnDisable()
    {
        _gameManagerScript.RemoveObserver(this);
    }
}
