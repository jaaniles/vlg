using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BehaviourStack : IBehaviour<GameObject>
{
    public List<IBehaviour<GameObject>> tasks = new List<IBehaviour<GameObject>>();

    public BehaviourStack(List<IBehaviour<GameObject>> _tasks)
    {
        tasks = _tasks;
    }

    public bool DoBehaviour(GameObject self)
    {
        bool tasksAreCompleted = DoStackTasks(self, tasks);
        return tasksAreCompleted;
    }
    public bool DoStackTasks(GameObject self, List<IBehaviour<GameObject>> steps)
    {
        for (int i = 0; i < steps.Count; i++)
        {
            if (steps[i].DoBehaviour(self) == false)
            {
                return false;
            }
        }
        return true;
    }
}
