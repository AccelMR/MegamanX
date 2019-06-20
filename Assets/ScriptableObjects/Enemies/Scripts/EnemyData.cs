using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy", order = 51)]

public class EnemyData : ScriptableObject
{
    //Enemy string name.
    [SerializeField]
    new string name;
    //Contains the enemy game object.
    [SerializeField] GameObject item;
    //Brief description of what the enemy does.
    [TextArea] [SerializeField] string description;
    [Space]
    //Amount of health the enemy has.
    [SerializeField] int health;
    //Amount of damage enemy deals.
    [SerializeField] int damage;

    /// <summary>
    /// Returns the enemies name
    /// </summary>
    public string Name
    {
        get { return name; }
    }
    /// <summary>
    /// Returns the enemies description
    /// </summary>
    public string Description
    {
        get { return description; }
    }
    /// <summary>
    /// Returns the enemies sprite
    /// </summary>
    public GameObject Item
    {
        get { return item; }
    }
    /// <summary>
    /// Returns the enemies damage
    /// </summary>
    public int Damage
    {
        get { return damage; }
    }
    /// <summary>
    /// Returns the enemies health
    /// </summary>
    public int Health
    {
        get { return health; }
    }
}
