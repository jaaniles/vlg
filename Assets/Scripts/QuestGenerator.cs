using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{

    public static IBehaviour<GameObject> GenerateGatherQuest()
    {
        return new BehaviourStack(GenerateQuestSteps());
    }

    private static List<IBehaviour<GameObject>> GenerateQuestSteps()
    {
        List<IBehaviour<GameObject>> questSteps = new List<IBehaviour<GameObject>>();

        ResourceTypes.Types wood = ResourceTypes.Types.Wood;
        ResourceTypes.Types rock = ResourceTypes.Types.Rock;

        ResourceDictionary requiredResources = new ResourceDictionary();
        requiredResources.Add(wood, 10);
        requiredResources.Add(rock, 10);

        foreach (KeyValuePair<ResourceTypes.Types, int> entry in requiredResources)
        {
            questSteps.Add(new GatherResource(entry.Key, entry.Value));
        }

        return questSteps;
    }
}
