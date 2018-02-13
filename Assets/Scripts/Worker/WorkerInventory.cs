using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkerInventory : MonoBehaviour
{
    public ResourceDictionary resourceInventory = new ResourceDictionary();
    private void Start()
    {
        // Initialize resource inventory
        ResetResourceInventory();
    }

    public void ResetResourceInventory()
    {
        resourceInventory.Clear();

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
    }
    public void RemoveResource(ResourceTypes.Types resource, int amount)
    {
        int outValue;
        if (!resourceInventory.TryGetValue(resource, out outValue))
        {
            Debug.LogWarning("Resource not found!");
            return;
        }

        resourceInventory[resource] -= amount;
    }

    public int GetInventoryItemsAmount()
    {
        int amount = 0;
        foreach (KeyValuePair<ResourceTypes.Types, int> entry in resourceInventory)
        {
            amount += entry.Value;
        }
        return amount;
    }
}
