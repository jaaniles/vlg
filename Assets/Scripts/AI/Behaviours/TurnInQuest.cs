using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInQuest : IBehaviour<GameObject>
{
    private ResourceDictionary requiredResources;

    public TurnInQuest(ResourceDictionary _requiredResources)
    {
        requiredResources = _requiredResources;
    }
    public bool DoBehaviour(GameObject self)
    {
        ResourceInventory playerInventory = PlayerInventory.instance.inventory;
        ResourceInventory resourceInventory = self.GetComponent<ResourceInventory>(); ;

        foreach (KeyValuePair<ResourceTypes.Types, int> entry in requiredResources)
        {
            resourceInventory.RemoveResource(entry.Key, entry.Value);
            playerInventory.AddResource(entry.Key, entry.Value);
        }

        self.GetComponent<WorkerController>().SetStatusText("Quest complete!");
        return true;
    }
}