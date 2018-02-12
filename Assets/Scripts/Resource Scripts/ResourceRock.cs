using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRock : Resource
{
    void Start()
    {
        transform.eulerAngles = Utilities.RandomZRotation(transform);
    }
}
