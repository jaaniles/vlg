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
            Resource resource = potentialGameObject.GetComponent<Resource>();

            if (!potentialGameObject.activeSelf || !resource || resource.isTargeted == true)
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

    public static IEnumerable<T> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

}
