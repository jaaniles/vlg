using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour
{
    private ResourceInventory inventory;
    void Start()
    {
        //inventory = GetComponent<ResourceInventory>();
    }
    void OnTriggerEnter(Collider other)
    {
    }
}
