using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, IGameManagerObserver
{
    [Header("Spawn Manager Settings")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject ufoPrefab;
    [SerializeField]
    private GameObject blackCloudPrefab;
    [SerializeField]
    private float distanceFromPlayer;
    [SerializeField]
    private float minZSpawn;
    [SerializeField]
    private float maxZSpawn;
    [Header("Black Clouds Settings")]
    [SerializeField]
    private float minBlackCloudTimeToSpawn;
    [SerializeField]
    private float maxBlackCloudTimeToSpawn;
    [Header("Ufos Settings")]
    [SerializeField]
    private float minUfoTimeToSpawn;
    [SerializeField]
    private float maxUfoTimeToSpawn;
    [Header("Coins Settings")]
    [SerializeField]
    private float coinSpawnStep;

    private Vector3 playerPosition;
    private Vector3 spawnerPreviousPosition;
    private float nextZPosition;
    private bool isGameOver;
    private GameManager gameManagerScript;
    private List<GameObject> coinsPool = new List<GameObject>();
    private List<GameObject> ufoPool = new List<GameObject>();
    private List<GameObject> blackCloudsPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        nextZPosition = Random.Range(minZSpawn + 1, maxZSpawn - 1);
        spawnerPreviousPosition = transform.position;
        StartCoroutine("UfoSpawner");
        StartCoroutine("BlackCloudsSpawner");
        gameManagerScript = FindObjectOfType<GameManager>();
        if (gameManagerScript != null)
            gameManagerScript.AddObserver(this);
    }

    void Update()
    {
        HandleSpawnerPosition();
        SpawnCoins();
        if (isGameOver) StopAllCoroutines();
    }

    void HandleSpawnerPosition()
    {
        playerPosition = player.transform.position;
        Vector3 spawnerNewPosition = new Vector3(spawnerPreviousPosition.x, playerPosition.y - distanceFromPlayer, spawnerPreviousPosition.z);
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
        objectPool.Add(newObject);
        return newObject;
    }

    IEnumerator UfoSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minUfoTimeToSpawn, maxUfoTimeToSpawn));
            GetObjectToPool(ufoPool, ufoPrefab).transform.position = RandomSpawnPosition();
        }
    }

    IEnumerator BlackCloudsSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBlackCloudTimeToSpawn, maxBlackCloudTimeToSpawn));
            GetObjectToPool(blackCloudsPool, blackCloudPrefab).transform.position = RandomSpawnPosition();
        }
    }

    void SpawnCoins()
    {
        if (spawnerPreviousPosition.y - transform.position.y >= coinSpawnStep)
        {
            GetObjectToPool(coinsPool, coinPrefab).transform.position = RandomZStep();
            spawnerPreviousPosition.y = transform.position.y;
        }
    }

    Vector3 RandomZStep()
    {
        if (nextZPosition > minZSpawn && nextZPosition < maxZSpawn)
            nextZPosition += Random.Range(0, 2) * 2 - 1;
        else if (nextZPosition <= minZSpawn)
            nextZPosition += 1;
        else if (nextZPosition >= maxZSpawn)
            nextZPosition -= 1;
        Vector3 randomZStep = new Vector3(transform.position.x, transform.position.y, nextZPosition);
        return randomZStep;
    }

    Vector3 RandomSpawnPosition()
    {
       Vector3 result = new Vector3(transform.position.x, transform.position.y, Random.Range(minZSpawn, maxZSpawn));
       return result;
    }
    float RandomSeconds(float min, float max)
    {
        float result = Random.Range(min, max);
        return result;
    }

    public void Notify(IGameManagerObserver.ChooseEvent option)
    {
        if (option == IGameManagerObserver.ChooseEvent.death)
            isGameOver = true;
        if (option == IGameManagerObserver.ChooseEvent.gameContinue)
        {
            isGameOver = false;
            StartCoroutine("UfoSpawner");
            StartCoroutine("BlackCloudsSpawner");
        }
    }
    private void OnDisable()
    {
        gameManagerScript.RemoveObserver(this);
    }
}
