using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public ResourceTypes resource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            //PlayerController worker = other.GetComponent<PlayerController>();
            //worker.SetBehaviour(new GatherQuest());
        }
    }
}
