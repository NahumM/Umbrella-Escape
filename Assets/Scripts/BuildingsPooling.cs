using System.Collections.Generic;
using UnityEngine;

public class BuildingsPooling : MonoBehaviour
{
    [SerializeField] private float cloudDistance;
    private List<GameObject> _pooledObjects = new List<GameObject>();
    private int _florsCount;
    private float _floorHeight;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        for (int i = 0; i < transform.childCount; i++)
            _pooledObjects.Add(transform.GetChild(i).gameObject);
    }

    private void Update()
    {
        Camera cam = Camera.main;
        Vector3 viewPos = cam.WorldToViewportPoint(_pooledObjects[0].transform.position);
        if (viewPos.y > 1) 
            PoolObject();
    }


    private void PoolObject()
    {
        if (!CompareTag("BuggedBuilding")) // One building has bugged mesh, need to adjust it manualy
            _floorHeight = _pooledObjects[0].GetComponent<Renderer>().bounds.size.y;
        else 
            _floorHeight = 3;

        Vector3 oldPosition = _pooledObjects[0].transform.position;
        Vector3 newPosition;

        if (CompareTag("Clouds")) 
            newPosition = new Vector3(oldPosition.x, _pooledObjects[0].transform.position.y - cloudDistance, oldPosition.z);
        else 
            newPosition = new Vector3(oldPosition.x, _pooledObjects[_pooledObjects.Count - 1].transform.position.y - _floorHeight, oldPosition.z);

        _pooledObjects[0].transform.position = newPosition;
        _pooledObjects.Add(_pooledObjects[0]);
        _pooledObjects.RemoveAt(0);
    }
}
