using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    //Variables
    public Transform Mega_Man;
    public float Distancia_Camara = 30.0f;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / Distancia_Camara);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mega_Man.position.x, Mega_Man.position.y, transform.position.z);
    }
}
