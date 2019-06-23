using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBlader : Enemy
{
    Animator m_animator;
    public GameObject ojoPrefab;
    float timeCounter = 0;//move up and down

    float shotTimeCounter = 0;//cambiar tipo de shot
    float shotSwitchTime = 3; //

    float step = 0.1f;//tiempo para moverse al ser disparado

    bool shotMissile = false;
    float startCurveCounter = 0;
    float startCurve = 1.0f;
    float endCurve = 2.0f;

    public float downPosition;
    public float upPosition;
    bool move = false;
    bool goUp = true;

    public int shotType = 0;

    public int actions = 0;

    [SerializeField] GameObject[] shot = new GameObject[2];
    float[] reloadTime = { 0.05f, 4.0f};

    // Start is called before the first frame update
    void Start()
    {
        shot[0] = Instantiate(shot[0], transform.position, transform.rotation);
        shot[1] = Instantiate(shot[1], transform.position, transform.rotation);
        Pos = new Vector3(-1, 0, 0);
        m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
        m_animator = GetComponent<Animator>();
        health = Max_Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            ShootTimer();
            if (move)
            {
                timeCounter += Time.deltaTime;
                transform.position += new Vector3(1, 0, 0) * Pos.x * m_speed * Time.deltaTime;
                if (timeCounter >= step)
                {
                    move = false;
                    timeCounter = 0;
                }
            }

            if (goUp)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, upPosition), Time.deltaTime * 0.3f);
                if (transform.position.y >= upPosition)
                {
                    goUp = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, downPosition), Time.deltaTime * 0.3f);
                if (transform.position.y <= downPosition)
                {
                    goUp = true;
                }
            }

            shotTimeCounter += Time.deltaTime;
            if (shotTimeCounter >= shotSwitchTime)
            {
                actions = Random.Range(0, 3);
                if (actions == 0)
                {
                    shotType = 0;
                }
                else if (actions == 1)
                {
                    shotType = 1;
                }
                else if (actions == 2)
                {
                    Instantiate(ojoPrefab, transform.position, transform.rotation);
                }
                shotTimeCounter = 0;
            }

            if (shotMissile)
            {
                startCurveCounter += Time.deltaTime;
            }
            if (startCurveCounter >= startCurve)
            {
                Debug.Log("curvear");
            }
            if (startCurveCounter >= endCurve)
            {
                startCurveCounter = 0;
                shotMissile = false;
            }
        }
       
        if (health <= 0)
        {
            m_animator.SetBool("dead", true);
        }
    }

    public override void ShootTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            canShoot = true;
        }
        if (canShoot)
        {
            Shoot();
            canShoot = false;
            currentTime = reloadTime[shotType];
        }
    }

    public override void Shoot()
    {
        if (shotType == 0)
        {
            shot[shotType].SetActive(true);
            shot[shotType].GetComponent<Projectile>().Activate(new Vector3(transform.position.x - 0.68f, transform.position.y + -0.5f, transform.position.z));
        }
        else if (shotType == 1)
        {
            shotMissile = true;
            shot[shotType].SetActive(true);
            shot[shotType].GetComponent<Projectile>().Activate(new Vector3(transform.position.x - 0.68f, transform.position.y + -0.5f, transform.position.z));
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            move = true;
            health -= 1;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Enemy Hit.   New health = " + health);
        }
        else if(collision.gameObject.tag == "Player")
        {
            //bajar vida jugador
        }
    }

}
