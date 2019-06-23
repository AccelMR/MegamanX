using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolitas_Girando : MonoBehaviour
{
    public GameObject Bolita_Megaman;
    float Angulo;
    Vector3 Centro;
    Vector3 Posicion;
    Vector3 Posicion2;
    Quaternion Rotacion;
    float Radio = 0.01f;
    GameObject[] Bolitas2 = new GameObject[16];

    // Start is called before the first frame update
    void Start()
    {
        Centro = transform.position;
        for (int i = 0; i < 16; i++)
        {
            Bolitas2[i] = Instantiate(Bolita_Megaman, Posicion, Rotacion, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Radio += Time.deltaTime * 2;
        for (int i = 0; i < 16; i++)
        {
            Posicion = Circulo_Random(Centro, Radio, i);
            Bolitas2[i].transform.position = Circulo_Random(Centro, Radio, i);
        }
    }

    Vector3 Circulo_Random(Vector3 _Centro, float _Radio, int _N)
    {
        Angulo = _N * 36;
        Posicion2.x = _Centro.x + _Radio * Mathf.Sin(Angulo * Mathf.Deg2Rad);
        Posicion2.y = _Centro.y + _Radio * Mathf.Cos(Angulo * Mathf.Deg2Rad);
        Posicion2.z = _Centro.z;
        return Posicion2;
    }

}
