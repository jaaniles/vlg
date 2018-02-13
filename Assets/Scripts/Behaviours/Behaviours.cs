using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour<T>
{
    bool DoBehaviour(T self);
}
public class GatherResource : IBehaviour<GameObject>
{
    public int amountToGather = 0;
    public int gatherProgress = 0;
    public string statusText;
    private Transform target;

    public GatherResource(int _amount = 5)
    {
        amountToGather = _amount;
    }
    public bool DoBehaviour(GameObject self)
    {
        WorkerController worker = Utilities.GetWorkerController(self);

        if (gatherProgress >= amountToGather)
        {
            return true; // Behaviour completed
        }

        if (target == null)
        {
            SetNewResourceTarget(self);
        }

        if (worker.isInRange(target) == true)
        {
            HandleGathering(self);
        }
        else
        {
            worker.FollowTarget(target);
        }

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
        WorkerInventory inventory = self.gameObject.GetComponent<WorkerInventory>();

        inventory.AddResource(type, amount);
        gatherProgress += amount;
    }
    private void SetNewResourceTarget(GameObject self)
    {
        target = Resource.GetClosestResource(self);
    }
    private void ResetResourceTarget()
    {
        target = null;
    }
}

public class ClaimQuestReward : IBehaviour<GameObject>
{
    public bool DoBehaviour(GameObject self)
    {
        Debug.Log("Quest completed! Time for reward!");
        return true;
    }
}

public class GoToQuestGiver : IBehaviour<GameObject>
{
    public string statusText;
    private Transform target;
    public bool DoBehaviour(GameObject self)
    {
        if (!target)
        {
            target = Utilities.GetClosestQuestGiver(self);
        }

        WorkerController worker = Utilities.GetWorkerController(self);
        if (worker.isInRange(target) == true)
        {
            return true;
        }
        else
        {
            worker.SetStatusText("Travelling..");
            worker.FollowTarget(target);
        }

        return false;
    }
}
