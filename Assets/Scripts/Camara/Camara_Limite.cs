using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Limite : MonoBehaviour
{
    //Variables
    private Vector2 Velocity;

    public float Smooth_Time_Y;
    public float Smooth_Time_X;
    public GameObject Mega_Man;
    public bool Bounds;
    public Vector3 Min_Camera_Pos;
    public Vector3 Max_Camera_Pos; 

    // Start is called before the first frame update
    void Start()
    {
        Mega_Man = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float Pos_X = Mathf.SmoothDamp(transform.position.x, Mega_Man.transform.position.x, ref Velocity.x, Smooth_Time_X);
        float Pos_Y = Mathf.SmoothDamp(transform.position.y, Mega_Man.transform.position.y, ref Velocity.y, Smooth_Time_Y);

        transform.position = new Vector3(Pos_X, Pos_Y, transform.position.z);

        if (Bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, Min_Camera_Pos.x, Max_Camera_Pos.x), Mathf.Clamp(transform.position.y, Min_Camera_Pos.y, Max_Camera_Pos.y), Mathf.Clamp(transform.position.z, Min_Camera_Pos.z, Max_Camera_Pos.z));
        }

    }
}
