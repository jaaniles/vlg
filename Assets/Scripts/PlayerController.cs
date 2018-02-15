﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class PlayerController : MonoBehaviour
{
    public int inventorySpace = 5;
    public float moveSpeed;
    public Transform target;
    public GameObject interactingTarget;
    public TextMeshProUGUI statusText;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private ResourceInventory inventory;
    private float collectProgress;

    void Start()
    {
        inventory = GetComponent<ResourceInventory>();
        statusText.text = "";
    }

    void FixedUpdate()
    {
        if (!target)
        {
            DetermineNextTask();
        }

        MoveTowardsTarget(target);
    }

    void MoveTowardsTarget(Transform target)
    {
        if (!target)
        {
            return;
        }

        FaceTarget();

        float step = moveSpeed * Time.deltaTime;
        if (!target.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    private void DetermineNextTask()
    {
        if (inventory.GetInventoryItemsAmount() >= inventorySpace)
        {
            GoToClosestStockpile();
        }
        else
        {
            GetClosestResource();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (target && target.gameObject == other.gameObject)
        {
            switch (other.gameObject.tag)
            {
                case "Resource":
                    HandleResourceCollect(other.gameObject);
                    break;
                case "Stockpile":
                    HandleStockpileLogic(other.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
    private void HandleResourceCollect(GameObject res)
    {
        SetStatusText("Collecting...");
        Resource resource = res.GetComponent<Resource>();
        ResourceInventory inventory = GetComponent<ResourceInventory>();

        int harvestedAmount = resource.Collect();

        if (harvestedAmount == -1)
        {
            SetStatusText("");
            StopFollowingTarget();
            return;
        }

        inventory.AddResource(resource.resourceType, harvestedAmount);
    }
    private void HandleStockpileLogic(GameObject stockpile)
    {
        ResourceInventory inventory = GetComponent<ResourceInventory>();
        ResourceInventory stockpileInventory = stockpile.GetComponent<ResourceInventory>();

        Debug.Log("Handle stockpile logic");

        // Loop each resource and their amounts to Stockpile inventory
        foreach (KeyValuePair<ResourceTypes.Types, int> entry in inventory.resourceInventory)
        {
            stockpileInventory.AddResource(entry.Key, entry.Value);
        }

        inventory.ResetResourceInventory();

        StopFollowingTarget();
    }
    private void GoToClosestStockpile()
    {
        LayerMask mask = LayerMask.GetMask("Stockpile");
        Collider[] stockpiles = Physics.OverlapSphere(GetComponent<Collider>().bounds.center, int.MaxValue, mask);
        Transform closestStockpile = GetClosest(stockpiles);

        if (!closestStockpile)
        {
            Debug.Log("No closest stockpile!");
            return;
        }

        FollowTarget(closestStockpile);
    }

    private void GetClosestResource()
    {
        LayerMask mask = LayerMask.GetMask("Resources");
        Collider[] resources = Physics.OverlapSphere(GetComponent<Collider>().bounds.center, int.MaxValue, mask);
        Collider[] filteredResources = Utilities.FilterResourcesByTargetedStatus(resources);
        Transform closestResource = GetClosest(filteredResources);

        if (!closestResource)
        {
            return;
        }

        SetNewResourceTarget(closestResource);
    }

    private void SetNewResourceTarget(Transform target)
    {
        FollowTarget(target);

        Resource resource = target.gameObject.GetComponent<Resource>();

        if (resource)
        {
            resource.isTargeted = true;
        }
    }
    private Transform GetClosest(Collider[] colliders)
    {
        Transform closestTarget = Utilities.GetClosestTarget(colliders, transform.position);

        if (!closestTarget)
        {
            return null;
        }

        return closestTarget;
    }

    private void FollowTarget(Transform targetToFollow)
    {
        target = targetToFollow;
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void StopFollowingTarget()
    {
        target = null;
    }

    private void SetStatusText(string text)
    {
        statusText.text = text;
    }
}
