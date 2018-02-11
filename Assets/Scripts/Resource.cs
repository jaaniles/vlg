using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool allowTargetingColors;
    public Material collectingMaterial;
    public Material targetedMaterial;
    public int timeToCollect = 1;
    public bool isTargeted = false;
    public bool isBeingCollected = false;
    public ResourceTypes.Types resourceType;
    public int yieldAmount = 1;

    void Start()
    {
        gameObject.name = "Resource: " + Random.Range(0, 100000).ToString();
    }

    void Update()
    {
        if (allowTargetingColors)
        {
            HandleColor();
        }
    }
    public virtual void Collect()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 5);
    }

    private void HandleColor()
    {
        if (isBeingCollected)
        {
            gameObject.GetComponent<Renderer>().material = collectingMaterial;
        }
        else if (isTargeted)
        {
            gameObject.GetComponent<Renderer>().material = targetedMaterial;
        }
    }
}
