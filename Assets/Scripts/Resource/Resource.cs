using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    public float radius = 3f;
    public bool allowTargetingColors;
    public Material targetedMaterial;
    public bool isTargeted = false;
    public ResourceTypes.Types resourceType;
    public int yieldMultiplier = 1;
    public float materialAmount = 10;
    private float harvestProgress;

    void Update()
    {
        if (allowTargetingColors)
        {
            HandleColor();
        }
    }
    public virtual int Collect()
    {
        // TODO: Add equipment etc modifiers
        float harvestAmount = 1 * Time.deltaTime;
        materialAmount -= harvestAmount;

        if (materialAmount <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 5);

            return -1; // Resource is now depleted
        }

        harvestProgress += harvestAmount;
        if (harvestProgress >= 0.99) // Hack, but makes sure player gets all of materialAmount
        {
            harvestProgress = 0;
            return 1 * yieldMultiplier;
        }
        else
        {
            return 0;
        }
    }
    private void HandleColor()
    {
        if (isTargeted)
        {
            gameObject.GetComponent<Renderer>().material = targetedMaterial;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(radius, transform.lossyScale.y, radius));
    }

    public static Transform GetClosestResource(GameObject fromWho)
    {
        Vector3 center = fromWho.GetComponent<Collider>().bounds.center;
        LayerMask mask = LayerMask.GetMask("Resources");

        // Filter only resources which are not being targeted
        Collider[] things = Utilities.FilterResourcesByTargetedStatus(Physics.OverlapSphere(center, float.MaxValue, mask));
        Transform closest = Utilities.GetClosestTarget(things, fromWho.transform.position);

        return closest;
    }
}
