using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerInventory : MonoBehaviour
{
    public Dictionary<ResourceTypes.Types, int> resourceInventory = new Dictionary<ResourceTypes.Types, int>();
    private void Start()
    {
        // Initialize resource inventory
        var resourceTypes = Utilities.GetEnumValues<ResourceTypes.Types>();

        foreach (ResourceTypes.Types resourceType in resourceTypes)
        {
            resourceInventory.Add(resourceType, 0);
        }

    }

    public void AddResource(ResourceTypes.Types resource, int amount)
    {

        int outValue;
        if (!resourceInventory.TryGetValue(resource, out outValue))
        {
            Debug.LogWarning("Resource not found!");
            return;
        }

        resourceInventory[resource] += amount;
        Debug.Log("Resource " + resource + " - amount: " + resourceInventory[resource]);
    }
}
