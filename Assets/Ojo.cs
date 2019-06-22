using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ojo : MonoBehaviour
{
    //[SerializeField]
    //private float velocity = 3.0f;
   
    //private Vector2 Dir;
   // public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
    //    Target = GameObject.FindGameObjectWithTag("Player").transform;   //      Dir = Target.position - transform.position;
//        Dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement() 
    {
        //transform.position = new Vector2(1, 0) * Mathf.Sign(Dir.x) * velocity * Time.deltaTime;
    }
}
