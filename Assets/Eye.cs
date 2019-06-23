using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
    public float m_velocity = 0.5f;
    private Vector3 Dir;
    public Transform Target;
    Animator m_animator;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Dir = Target.position - transform.position;
        Dir.Normalize();
        m_animator = GetComponent<Animator>();
        health = Max_Health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * Mathf.Sign(Dir.x) * m_velocity * Time.deltaTime;
        transform.localScale = new Vector3(-2, 2, 1);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 1;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Enemy Hit.   New health = " + health);
        }
    }
}
