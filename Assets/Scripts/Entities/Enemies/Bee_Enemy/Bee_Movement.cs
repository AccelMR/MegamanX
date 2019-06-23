using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Movement : MonoBehaviour
{
    
     public float velocity = -1f;
     Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        myRigidBody.velocity = new Vector2(velocity, myRigidBody.velocity.y);
        

    }
}