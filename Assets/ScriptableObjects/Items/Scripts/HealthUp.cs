using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Item", menuName = "Health Item", order = 51)]
public class HealthUp : ItemData
{
    //Amount of health this item heals.
    [Space]
    [SerializeField] int health;

    public int Health
    {
        get { return health; }
    }
}
