using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Utilities : ScriptableObject
{

    public static Transform GetClosestTarget(Collider[] colliders, Vector3 currentPosition)
    {
        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider potentialTarget in colliders)
        {
            GameObject potentialGameObject = potentialTarget.gameObject;

            if (!potentialGameObject.activeSelf)
            {
                continue;
            }

            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTarget = potentialGameObject.transform;
            }
        }

        return closestTarget;
    }

    public static WorkerController GetWorkerController(GameObject go)
    {
        WorkerController worker = go.GetComponent<WorkerController>();

        if (worker == null)
        {
            Debug.LogWarning("Failed to find Worker-component in worker");
            return null;
        }

        return worker;
    }

    public static Collider[] FilterResources(Collider[] resources, ResourceTypes.Types resourceType)
    {
        List<Collider> filteredList = new List<Collider>();
        for (int i = 0; i < resources.Length; i++)
        {
            Resource resource = resources[i].GetComponent<Resource>();

            if (resource.isTargeted == false && resource.resourceType == resourceType)
            {
                filteredList.Add(resources[i]);
            }
        }

        return filteredList.ToArray();
    }

    public static IEnumerable<T> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static Vector3 RandomZRotation(Transform what)
    {
        Vector3 euler = what.eulerAngles;
        euler.z = UnityEngine.Random.Range(0f, 360f);
        return euler;
    }

    public static Transform GetClosestQuestGiver(GameObject self)
    {
        LayerMask mask = LayerMask.GetMask("QuestGiver");
        Collider[] questGivers = Physics.OverlapSphere(self.GetComponent<Collider>().bounds.center, int.MaxValue, mask);
        Transform closestQuestGiver = Utilities.GetClosestTarget(questGivers, self.transform.position);

        return closestQuestGiver;
    }
}
