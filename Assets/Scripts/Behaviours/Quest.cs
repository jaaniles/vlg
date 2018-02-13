using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GatherQuest : IBehaviour<GameObject>
{
    public List<IBehaviour<GameObject>> questSteps;

    public GatherQuest()
    {
        questSteps = new List<IBehaviour<GameObject>>();
        questSteps.Add(new GoToQuestGiver());
        questSteps.Add(new GatherResource());
        questSteps.Add(new GoToQuestGiver());
        questSteps.Add(new GatherResource());
        questSteps.Add(new GoToQuestGiver());
        questSteps.Add(new ClaimQuestReward());
    }
    public bool DoBehaviour(GameObject self)
    {
        if (questSteps.Count < 1)
        {
            Debug.Log("Quest completed");
            return true;
        }

        IBehaviour<GameObject> currentStep = questSteps[0];

        bool completed = currentStep.DoBehaviour(self);
        if (completed == true)
        {
            Debug.Log("Quest step completed");
            questSteps.Remove(currentStep);
        }

        return false;
    }
}


