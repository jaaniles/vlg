using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSafety : IBehaviour<GameObject>
{

    public string statusText = "FLEE! FLEE!";
    private Transform target;
    public bool DoBehaviour(GameObject self)
    {
        WorkerController worker = self.GetComponent<WorkerController>();

        if (ScanForHostiles(self) == false)
        {
            return true; // We are safe
        }

        worker.SetStatusText(statusText);

        if (!target)
        {
            target = GetClosestSafezone(self);
        }

        bool inRange = Behaviours.MoveToTarget(self, target);
        if (inRange == true) return true;

        return false;
    }

    private bool ScanForHostiles(GameObject self)
    {
        WorkerController worker = self.GetComponent<WorkerController>();
        if (worker == null)
        {
            Debug.LogWarning("No workerController found in Worker!!");
            return false;
        }

        LayerMask mask = LayerMask.GetMask("Hostile");
        Collider[] hostiles = Physics.OverlapSphere(self.GetComponent<Collider>().bounds.center, worker.scanDistance, mask);

        if (hostiles.Length > 0)
        {
            return true;
        }

        return false;
    }

    private Transform GetClosestSafezone(GameObject self)
    {
        LayerMask mask = LayerMask.GetMask("Safety");
        Collider[] safezones = Physics.OverlapSphere(self.GetComponent<Collider>().bounds.center, int.MaxValue, mask);
        Transform closestSafezone = Utilities.GetClosestTarget(safezones, self.transform.position);

        return closestSafezone;
    }
}
