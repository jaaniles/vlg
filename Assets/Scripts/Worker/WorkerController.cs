using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    public int scanDistance = 15;
    public IBehaviour<GameObject> reactions;
    public IBehaviour<GameObject> task;
    public IBehaviour<GameObject> idleTask;
    private NavMeshAgent agent;
    private float radius = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        task = QuestGenerator.GenerateGatherQuest();
        reactions = ReactionStackGenerator.GenerateReactionStack();
    }

    void Update()
    {
        DoPrioritizedTask();
    }

    public void DoPrioritizedTask()
    {
        bool reactionsNeeded = !DoTask(ref reactions);
        if (reactionsNeeded) return;

        bool taskDone = DoTask(ref task);
        if (taskDone)
        {
            task = null;
        }
        else
        {
            return;
        }

        DoTask(ref idleTask);
    }

    public bool DoTask(ref IBehaviour<GameObject> _task)
    {
        if (_task == null)
        {
            return true;
        }

        return _task.DoBehaviour(gameObject);
    }
    public void FollowTarget(Transform target)
    {
        if (target == null)
        {
            return;
        }

        FaceTarget(gameObject.transform, target);

        agent.stoppingDistance = radius * 0.8f;
        agent.updateRotation = false;
        agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
    }

    public bool isInRange(Transform target)
    {
        if (target == null)
        {
            return false;
        }

        FaceTarget(gameObject.transform, target);
        float distance = Vector3.Distance(gameObject.transform.position, target.position);
        return distance <= radius;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
    }

    public static void FaceTarget(Transform self, Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = (target.position - self.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        self.rotation = Quaternion.Slerp(self.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void SetStatusText(string text)
    {
        //statusText.text = text;
    }
}
