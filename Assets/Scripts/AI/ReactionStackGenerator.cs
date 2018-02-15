using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionStackGenerator : MonoBehaviour
{

    public static IBehaviour<GameObject> GenerateReactionStack()
    {
        List<IBehaviour<GameObject>> reactionBehaviours = new List<IBehaviour<GameObject>>();
        reactionBehaviours.Add(new MoveToSafety());

        return new BehaviourStack(reactionBehaviours);
    }
}
