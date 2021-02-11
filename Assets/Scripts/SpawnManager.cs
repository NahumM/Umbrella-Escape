using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
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
    [SerializeField] private int cloudStepsToSpawn;
    [SerializeField] private int minCloudStepsToSpawn;
    [SerializeField] private int maxCloudStepsToSpawn;
    [Header("Ufos Settings")]
    [SerializeField] private int ufoStepsToSpawn;
    [SerializeField] private int minUfoStepsToSpawn;
    [SerializeField] private int maxUfoStepsToSpawn;
    [Header("Coins Settings")]
    [SerializeField] private int _coinSpawnStep;

    private int spawnStepsCount;
    private Vector3 playerPosition;
    private Vector3 spawnerPreviousPosition;
    private float nextZPosition;
    private List<GameObject> coinsPool = new List<GameObject>();
    private List<GameObject> ufoPool = new List<GameObject>();
    private List<GameObject> blackCloudsPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        nextZPosition = Random.Range(_minZSpawn + 1, _maxZSpawn - 1);
        spawnerPreviousPosition = transform.position;
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
    void SpawnCoins()
    {
        if (spawnerPreviousPosition.y - transform.position.y >= _coinSpawnStep)
        {
            spawnStepsCount++;
            GetObjectToPool(coinsPool, _coinPrefab).transform.position = RandomZStep();
            spawnerPreviousPosition.y = transform.position.y;
            if (spawnStepsCount == cloudStepsToSpawn)
            {
                GetObjectToPool(blackCloudsPool, _blackCloudPrefab).transform.position = RandomSpawnPosition();
                cloudStepsToSpawn += Random.Range(minCloudStepsToSpawn, maxCloudStepsToSpawn);
            }
            if (spawnStepsCount == ufoStepsToSpawn)
            {
                GetObjectToPool(ufoPool, _ufoPrefab).transform.position = RandomSpawnPosition();
                ufoStepsToSpawn += Random.Range(minUfoStepsToSpawn, maxUfoStepsToSpawn);
            }
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
}
