using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public TextMeshProUGUI statusText;
    public LayerMask mask;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Transform target;
    public Collider[] colliders;

    void Start()
    {
        statusText.text = "";
    }

    void FixedUpdate()
    {
        if (!target || !target.gameObject.activeSelf)
        {
            SetNewTarget();
            return;
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
        Resource resource = target.gameObject.GetComponent<Resource>();

        float step = moveSpeed * Time.deltaTime;
        if (!target.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (target && target.gameObject == other.gameObject && other.gameObject.CompareTag("Resource"))
        {
            Resource resource = other.gameObject.GetComponent<Resource>();
            if (!resource.isBeingCollected)
            {
                StartCoroutine(Collect(other.gameObject));
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (target && target.gameObject == other.gameObject && other.gameObject.CompareTag("Resource"))
        {
            Resource resource = other.gameObject.GetComponent<Resource>();
            if (!resource.isBeingCollected)
            {
                StartCoroutine(Collect(other.gameObject));
            }
        }
    }

    private IEnumerator Collect(GameObject collectable)
    {
        Resource resource = collectable.GetComponent<Resource>();
        WorkerInventory inventory = GetComponent<WorkerInventory>();

        resource.isBeingCollected = true;
        SetStatusText("Collecting...");
        yield return new WaitForSeconds(resource.timeToCollect);
        SetStatusText("");

        inventory.AddResource(resource.resourceType, resource.yieldAmount);
        resource.Collect();
        StopFollowingTarget();
    }

    private void SetNewTarget()
    {
        colliders = Physics.OverlapSphere(GetComponent<Collider>().bounds.center, int.MaxValue, mask);
        Transform closestTarget = Utilities.GetClosestTarget(colliders, transform.position);

        if (!closestTarget || target)
        {
            return;
        }

        FollowTarget(closestTarget);
        target.gameObject.GetComponent<Resource>().isTargeted = true;
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
