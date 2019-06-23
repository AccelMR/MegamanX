using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolitas_Girando_2 : MonoBehaviour
{
    public GameObject m_bolita;

    float m_scale_speed = 0.01f;

    GameObject[] m_bolita_list = new GameObject[80];
    
    float Amplitud      = 0.0f;
    float multiplier    = 0.3926f;

    float m_amplitud_max = 3.5f;


    float X = 0.0f;
    float Y = 0.0f;

    float Limite = 0.0f;

    private int m_offset = 0;
    private int m_limit = 16;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 80; i++)
        {
            m_bolita_list[i] = Instantiate
            (
            m_bolita, 
            gameObject.transform.position, 
            Quaternion.identity
            );
        }
    }

    // Update is called once per frame
    void 
    Update()
    {
        // Aumenta la escala del circulo
        if(Amplitud <= m_amplitud_max)
        {
            Amplitud += Time.time * m_scale_speed;
        }
        
        // Calcular las posiciones de las bolitas
        for (int i = 0; i < m_limit; i++)
        {
            X = Mathf.Sin(Time.time + (multiplier * i)) * Amplitud;            
            Y = Mathf.Cos(Time.time + (multiplier * i)) * Amplitud;

            Vector2 bolita_posicion = new Vector2(X, Y);
            Vector2 father_position = gameObject.transform.position;

            bolita_posicion += father_position;

            m_bolita_list[i].gameObject.transform.position = bolita_posicion;
            Limite += Time.deltaTime;
        }


        if (Limite >= 30)
        {
            m_limit *= 2;
            Limite = 0;
            Amplitud = 0;
        }
        if(m_limit == 96)
        {
            for (int i = 0; i < 80; i++)
            {
                Destroy(m_bolita_list[i]);
            }
        }

        return;
    }
}

