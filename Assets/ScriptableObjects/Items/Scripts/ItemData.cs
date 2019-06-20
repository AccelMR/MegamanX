using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData : ScriptableObject
{
    //item string name.
    [SerializeField]
    new string name;
    //Contains the item object.
    [SerializeField] GameObject item;
    //Brief description of what the item does.
    [TextArea] [SerializeField] string description;
    /// <summary>
    /// Returns the items name
    /// </summary>
    public string Name
    {
        get { return name; }
    }
    /// <summary>
    /// Returns the items description
    /// </summary>
    public string Description
    {
        get { return description; }
    }
    /// <summary>
    /// Returns the items sprite
    /// </summary>
    public GameObject Item
    {
        get { return item; }
    }
}
