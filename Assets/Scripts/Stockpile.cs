using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour
{
    private ResourceInventory inventory;
    void Start()
    {
<<<<<<< Updated upstream
        inventory = GetComponent<WorkerInventory>();
=======
        //inventory = GetComponent<ResourceInventory>();
>>>>>>> Stashed changes
    }
    void OnTriggerEnter(Collider other)
    {
    }
}
