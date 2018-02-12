using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ResourceDictionary : SerializableDictionary<ResourceTypes.Types, int> { }

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> { }