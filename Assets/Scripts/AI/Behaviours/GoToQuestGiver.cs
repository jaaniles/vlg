using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToQuestGiver : IBehaviour<GameObject>
{
    public string statusText;
    private Transform target;
    public bool DoBehaviour(GameObject self)
    {
        if (!target)
        {
            target = Utilities.GetClosestQuestGiver(self);
        }

        bool inRange = Behaviours.MoveToTarget(self, target);
        if (inRange == true) return true;

        return false;
    }
}