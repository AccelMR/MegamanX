using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossProjectile : Projectile
{
    public Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
    }

    // Update is called once per frame
    public override void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

}
