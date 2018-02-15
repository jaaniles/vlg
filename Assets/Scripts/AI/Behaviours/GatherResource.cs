using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResource : IBehaviour<GameObject>
{
    public int amountToGather = 0;
    public string statusText;
    private Transform target;
    private ResourceTypes.Types resourceType;

    public GatherResource(ResourceTypes.Types _resourceType, int _amount = 5)
    {
        resourceType = _resourceType;
        amountToGather = _amount;
    }
    public bool DoBehaviour(GameObject self)
    {
        ResourceInventory resourceInventory = GetResourceInventory(self);
        int amountOfResource = resourceInventory.GetResourceAmount(resourceType);

        if (amountOfResource >= amountToGather)
        {
            return true; // Behaviour completed
        }

        if (target == null)
        {
            SetNewResourceTarget(self);
        }

        bool inRange = Behaviours.MoveToTarget(self, target);
        if (inRange == true) HandleGathering(self);

        return false;
    }

    private void HandleGathering(GameObject self)
    {
        Resource resource = target.gameObject.GetComponent<Resource>();

        int harvestedAmount = resource.Collect();
        if (harvestedAmount == -1) // Resource is depleted
        {
            ResetResourceTarget();
            return;
        }

        Gather(self, resource.resourceType, harvestedAmount);

        WorkerController worker = self.GetComponent<WorkerController>();
        worker.SetStatusText("Gathering..");
    }

    private void Gather(GameObject self, ResourceTypes.Types type, int amount)
    {
        ResourceInventory inventory = self.gameObject.GetComponent<ResourceInventory>();

        inventory.AddResource(type, amount);
    }
    private void SetNewResourceTarget(GameObject self)
    {
        target = Resource.GetClosestResource(self, resourceType);
    }
    private void ResetResourceTarget()
    {
        target = null;
    }

    private ResourceInventory GetResourceInventory(GameObject worker)
    {
        ResourceInventory resourceInventory = worker.GetComponent<ResourceInventory>();
        if (resourceInventory == null)
        {
            Debug.LogWarning("No worker inventory found!");
        }
        return resourceInventory;
    }
}