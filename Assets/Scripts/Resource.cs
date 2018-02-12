using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
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
}
