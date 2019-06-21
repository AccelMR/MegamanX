using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Item", menuName = "Weapon Item", order = 51)]
public class RecoverWeapon : ItemData
{
    //Amount of health this item heals.
    [Space]
    [SerializeField] int recovery;

    public int Recovery
    {
        get { return recovery; }
    }
}
