using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour<T>
{
    bool DoBehaviour(T self);
}

public class Behaviours : ScriptableObject
{
    public static bool MoveToTarget(GameObject self, Transform target)
    {
        WorkerController worker = Utilities.GetWorkerController(self);
        if (worker.isInRange(target) == true)
        {
            return true;
        }
        else
        {
            worker.FollowTarget(target);
        }

        return false;
    }
}
