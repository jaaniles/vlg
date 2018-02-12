using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour
{
    private WorkerInventory inventory;
    void Start()
    {
        inventory = GetComponent<WorkerInventory>();
    }
    void OnTriggerEnter(Collider other)
    {
    }
}
