using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTree : Resource
{
    void Start()
    {
        resourceType = ResourceTypes.Types.Wood;
    }
    public override void Collect()
    {
        base.Collect();
    }
}
