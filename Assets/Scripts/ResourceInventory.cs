using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceInventory : MonoBehaviour
{
    public ResourceDictionary resourceInventory = new ResourceDictionary();
    private void Start()
    {
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

    public void AddResource(ResourceTypes.Types resourceType, int amount)
    {
        if (TryGetResource(resourceType) == false) return;

        resourceInventory[resourceType] += amount;
    }
    public void RemoveResource(ResourceTypes.Types resourceType, int amount)
    {
        if (TryGetResource(resourceType) == false) return;

        resourceInventory[resourceType] -= amount;
    }

    public int GetResourceAmount(ResourceTypes.Types resourceType)
    {
        if (TryGetResource(resourceType) == false) return 0;

        return resourceInventory[resourceType];
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

    private bool TryGetResource(ResourceTypes.Types resourceType)
    {
        int outValue;
        if (!resourceInventory.TryGetValue(resourceType, out outValue))
        {
            Debug.LogWarning("Resource not found!");
            return false;
        }

        return true;
    }
}
