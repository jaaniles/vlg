using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    public static PlayerInventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one playerinventory!!");
            return;
        }
        instance = this;
    }
    #endregion

    public ResourceInventory inventory;

    void Start()
    {
        inventory = GetComponent<ResourceInventory>();
    }
}
