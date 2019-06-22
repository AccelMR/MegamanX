using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadAttacker : Enemy
{
    float m_maxDistance = 2.5f;
    float m_minDistance = 0.5f;
    public bool RtoL = false;
    public bool LtoR = false;

    bool m_playerIsR = false;
    bool m_playerIsStartFrame;

    public bool canGetTime = true;

    float minimum = 1.666f;
    float maximum = -1.666f;

    // Time taken for the transition.
    float duration = 1.5f;
    float startTime;

    Animator m_animator;

    Vector2 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        Pos = new Vector3(1, 0, 0);
        // Make a note of the time the script started.
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerIsStartFrame = m_playerIsR;

        m_speed = Mathf.Clamp(m_speed, -1.666f, 1.666f);

        if (Vector2.Distance(new Vector2(startPoint.x, 0), new Vector2(transform.position.x, 0)) >= m_maxDistance)//cuando llegue al limite
        {
            if (m_Megaman.position.x > transform.position.x)//si megaman esta a la derecha
            {
                LtoR = true;
            }
            else//si esta a la izquierda
            {
                RtoL = true;
            }
        }

        if (m_Megaman.position.x > transform.position.x)//si megaman esta a la derecha
        {
            m_playerIsR = true;
        }
        else//si esta a la izquierda
        {
            m_playerIsR = false;
        }


        if (m_playerIsR)
        {
            m_animator.SetBool("lookBack", true);
        }
        else
        {
            m_animator.SetBool("lookBack", false);
        }

        if (RtoL)
        {
            Derrape(m_speed, maximum);
            transform.localScale = new Vector2(-2, 2);
        }

        if (LtoR)
        {
            Derrape(m_speed, minimum);
            transform.localScale = new Vector2(2, 2);
        }

        transform.position += new Vector3(1, 0, 0) * Pos.x * m_speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (m_playerIsStartFrame != m_playerIsR)//si cambio de lado durante este frame
        {
            Debug.Log("cambio!");
            startPoint = m_Megaman.position;
            Debug.Log(startPoint);
        }
    }

    void Derrape(float from, float to)
    {
        if (canGetTime)
        {
            startTime = Time.time;
            canGetTime = false;
        }

        float t = (Time.time - startTime) / duration;
        m_speed = Mathf.SmoothStep(from, to, t);
        if (m_speed == to)
        {
            RtoL = false;
            LtoR = false;
            canGetTime = true;
        }
    }
}
