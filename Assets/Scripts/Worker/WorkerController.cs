using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    public IBehaviour<GameObject> task;
    private NavMeshAgent agent;
    public TextMeshProUGUI statusText;
    private float radius = 3f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (task == null) task = new GatherQuest();

        bool completed = task.DoBehaviour(gameObject);
        if (completed == true)
        {
            task = null;
        }
    }

    public void FollowTarget(Transform target)
    {
        FaceTarget(gameObject.transform, target);

        agent.stoppingDistance = radius * 0.8f;
        agent.updateRotation = false;
        agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
    }

    public bool isInRange(Transform target)
    {
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
        Vector3 direction = (target.position - self.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        self.rotation = Quaternion.Slerp(self.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void SetStatusText(string text)
    {
        statusText.text = text;
    }
}
