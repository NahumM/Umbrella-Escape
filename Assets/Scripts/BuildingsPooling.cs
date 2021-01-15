using System.Collections.Generic;
using UnityEngine;

public class BuildingsPooling : MonoBehaviour
{
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int florsCount;
    private float floorHeight;
    private Plane[] cameraFrustum;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            pooledObjects.Add(transform.GetChild(i).gameObject);
    }
    void Update()
    {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(cameraFrustum, pooledObjects[0].GetComponent<Renderer>().bounds))
        {
            if (CompareTag("Building") || CompareTag("BuggedBuilding"))
                PoolBuilding();
            if (CompareTag("Clouds"))
                PoolClouds();
        }
    }

    private void PoolBuilding()
    {
        if (!CompareTag("BuggedBuilding")) // One building has bugged mesh, need to adjust it manualy
            floorHeight = pooledObjects[0].GetComponent<Renderer>().bounds.size.y;
        else floorHeight = 3;
        Vector3 oldPosition = pooledObjects[0].transform.position;
        Vector3 newPosition = new Vector3(oldPosition.x, pooledObjects[pooledObjects.Count - 1].transform.position.y - floorHeight, oldPosition.z);
        pooledObjects[0].transform.position = newPosition;
        pooledObjects.Add(pooledObjects[0]);
        pooledObjects.RemoveAt(0);
    }
    private void PoolClouds()
    {
        Vector3 oldPosition = pooledObjects[0].transform.position;
        Vector3 newPosition = new Vector3(oldPosition.x, pooledObjects[pooledObjects.Count - 1].transform.position.y - 243, oldPosition.z);
        pooledObjects[0].transform.position = newPosition;
        pooledObjects.Add(pooledObjects[0]);
        pooledObjects.RemoveAt(0);
    }
}
