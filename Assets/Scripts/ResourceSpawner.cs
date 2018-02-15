using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{

    public GameObject resource;
    public static HashSet<GameObject> resources = new HashSet<GameObject>();
    public bool continousSpawning;
    public int timeBetweenSpawn;
    public int maxResources;
    void Start()
    {
        if (maxResources < 1)
        {
            return;
        }

        for (int i = 0; i < maxResources; i++)
        {
            SpawnResource();
        }

        if (continousSpawning == true)
        {
            InvokeRepeating("SpawnResource", timeBetweenSpawn, timeBetweenSpawn);
        }
    }

    private void SpawnResource()
    {
        Vector3 randomPosition = RandomPointInBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);
        Instantiate(resource, randomPosition, Quaternion.identity);
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
            (Random.value - 0.5f) * size.x,
            0,
            (Random.value - 0.5f) * size.z
        );
    }
}
