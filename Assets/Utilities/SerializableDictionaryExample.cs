using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionaryExample : MonoBehaviour
{
    // The dictionaries can be accessed throught a property
    [SerializeField]
    ResourceDictionary m_resourceDictionary;
    public IDictionary<ResourceTypes.Types, int> ResourceDictionary
    {
        get { return m_resourceDictionary; }
        set { m_resourceDictionary.CopyFrom(value); }
    }
}
