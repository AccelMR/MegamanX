using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
  public Transform m_Megaman;
  private Vector3 Pos;
  public float m_speed = .05f;
  public int lives = 6;
    // Start is called before the first frame update
    void Start()
    {
      m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
      Pos = m_Megaman.position - transform.position;
      Pos.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
      transform.position += new Vector3(1, 0, 0) * Mathf.Sign( Pos.x) * m_speed * Time.deltaTime;
    }

  void OnCollisionEnter2D(Collision2D collision)
  {
   // if (collision.collider.CompareTag("Player"))
   // {
   //   // Damage to megaman
   //   transform.position += new Vector3(transform.position.x, 0, 0);
   // }
   // else
   // {
   //   // Spiky continua moviendose en su diereccion
   //   transform.position += new Vector3(transform.position.x, 0, 0) * -1;
   // }
  }

  void resetSpiky()
  {
    lives = 10;
  }
}
