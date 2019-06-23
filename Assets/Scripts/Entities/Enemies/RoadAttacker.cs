using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadAttacker : Enemy
{
    float maxDistance = 1.9f;

    public bool turnLeft = false;
    public bool turnRight = false;
    public bool playerIsRight = false;
    public bool isROnFrameStart;
    public bool canGetTime = true;

    float rightSpeed = 1.666f;
    float leftSpeed = -1.666f;

    // Time taken for the transition.
    float duration = 1.7f;
    float startTime;

    Animator m_animator;

    Vector2 startPoint;

    [SerializeField] GameObject shot;

    // Start is called before the first frame update
    void Start()
    {
        shot = Instantiate(shot);
        m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(startPoint);
        m_animator = GetComponent<Animator>();
        Pos = new Vector3(1, 0, 0);
        // Make a note of the time the script started.
        startTime = Time.time;
        health = Max_Health;
        startPoint = m_Megaman.position;
    }

    // Update is called once per frame
    void Update()
    {
        isROnFrameStart = playerIsRight;

        m_speed = Mathf.Clamp(m_speed, -1.666f, 1.666f);

        if (health > 6)//si tadavia tiene piloto
        {
            ShootTimer();

            if (m_Megaman.position.x > transform.position.x)//si megaman esta a la derecha
            {
                playerIsRight = true;
            }
            else//si esta a la izquierda
            {
                playerIsRight = false;
            }

            if (Vector2.Distance(new Vector2(startPoint.x, 0), new Vector2(transform.position.x, 0)) >= maxDistance)//cuando llegue al limite
            {
                if (playerIsRight)//si megaman esta a la derecha
                {
                    turnRight = true;
                    turnLeft = false;

                }
                else//si esta a la izquierda
                {
                    turnLeft = true;
                    turnRight = false;
                }
            }

            if (playerIsRight && transform.localScale.x < 0)
            {
                m_animator.SetBool("lookBack", true);
            }
            else if (!playerIsRight && transform.localScale.x > 0)
            {
                m_animator.SetBool("lookBack", true);
            }
            else
            {
                m_animator.SetBool("lookBack", false);
            }

            if (turnLeft)
            {
                Derrape(m_speed, leftSpeed);
                turnRight = false;
            }

            if (turnRight)
            {
                Derrape(m_speed, rightSpeed);
                turnLeft = false;
            }
        }


        if (health > 3)//si todavia no esta roto, moverse
        {
            transform.position += new Vector3(1, 0, 0) * Pos.x * m_speed * Time.deltaTime;
        }

        if (health <= 6)
        {
            m_animator.SetBool("dead", true);
            Debug.Log("dead");
        }

        if (health <= 3)
        {
            m_animator.SetBool("destroyed", true);
        }
    }

    private void LateUpdate()
    {
        if (isROnFrameStart != playerIsRight)//si cambio de lado durante este frame
        {
            Debug.Log("cambio!");
            startPoint = m_Megaman.position;
            Debug.Log(startPoint);
        }
    }

    void Derrape(float from, float to)
    {
        if (!m_animator.GetBool("derrape"))
        {
            m_animator.SetBool("derrape", true);
        }
        if (canGetTime)
        {
            startTime = Time.time;
            canGetTime = false;
        }

        float t = (Time.time - startTime) / duration;
        m_speed = Mathf.SmoothStep(from, to, t);

        if (m_speed >= to/2)
        {
            if (turnLeft)
            {
                transform.localScale = new Vector2(-2, 2);
            }

            if (turnRight)
            {
                transform.localScale = new Vector2(2, 2);
            }
        }

        if (m_speed == to)
        {
            turnLeft = false;
            turnRight = false;
            canGetTime = true;
            m_animator.SetBool("derrape", false);
        }
    }

    public override void Shoot()
    {
        shot.SetActive(true);
        if (transform.localScale.x < 0)//si esat miarando a la izquierda
        {
            shot.GetComponent<Projectile>().Speed = Mathf.Abs(shot.GetComponent<Projectile>().Speed);
            shot.GetComponent<Projectile>().Activate(new Vector3(transform.position.x - 0.5f, transform.position.y + 0.16f, transform.position.z));
        }
        else
        {
            shot.GetComponent<Projectile>().Speed *= -1;
            shot.GetComponent<Projectile>().Activate(new Vector3(transform.position.x + 0.7f, transform.position.y + 0.16f, transform.position.z));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (health <= 6 && collision.gameObject.tag == "Player" && m_Megaman.position.y > transform.position.y + 0.25f) // el numero magico es la altura del collider
        {
            m_animator.SetBool("PlayerOnCar", true);
            m_Megaman.SetParent(transform);
            gameObject.tag = "Untagged";
        }
        else
        {
            gameObject.tag = "Enemy";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (health <= 6 && collision.gameObject.tag == "Player")
        {
            m_animator.SetBool("PlayerOnCar", false);
            m_Megaman.SetParent(null);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
