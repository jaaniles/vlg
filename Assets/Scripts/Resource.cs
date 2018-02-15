using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Resource : MonoBehaviour
{
    public TextMeshProUGUI materialAmountText;
    public float radius = 3f;
    public bool isTargeted = false;
    public ResourceTypes.Types resourceType;
    public int yieldMultiplier = 1;
    public float materialAmount = 10;
    public float maxMaterialAmount = 25;
    public float growMultiplier = 0.1f;
    private float harvestProgress;

    void FixedUpdate()
    {
        materialAmountText.text = Math.Floor(materialAmount).ToString();

        if (growMultiplier > 0)
        {
            Grow();
        }
    }

    private void Grow()
    {
        materialAmount += growMultiplier * Time.deltaTime;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(radius, transform.lossyScale.y, radius));
    }

    public static Transform GetClosestResource(GameObject fromWho, ResourceTypes.Types resourceType)
    {
        Vector3 center = fromWho.GetComponent<Collider>().bounds.center;
        LayerMask mask = LayerMask.GetMask("Resources");

        Collider[] overlappingResources = Physics.OverlapSphere(center, float.MaxValue, mask);
        Collider[] validResources = Utilities.FilterResources(overlappingResources, resourceType);
        Transform closestValidResource = Utilities.GetClosestTarget(validResources, fromWho.transform.position);

        return closestValidResource;
    }
}
