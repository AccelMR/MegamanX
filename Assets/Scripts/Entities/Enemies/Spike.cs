using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Enemy
{
  
  // Start is called before the first frame update
  void Start()
  {
    Reset();
    
    //transform.position *= Mathf.Sign(Pos.x);

  }

  // Update is called once per frame
  void Update()
  {
    
    //Pos = m_Megaman.position - transform.position;
    //Pos.Normalize();
    transform.position += new Vector3(1, 0, 0) * Pos.x * m_speed * Time.deltaTime;
    // transform.position = m_Megaman.gameObject.transform.position;
    //transform.position += new Vector3(m_Megaman.gameObject.transform.position.x, 0, 0);
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

  public override void Reset()
  {
    health = Max_Health;
    
    m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
    if (transform.position.x < m_Megaman.position.x)
    {
      Pos = new Vector3(1, 0, 0);
    }
    else
    {
      Pos = new Vector3(-1, 0, 0);
    }
  }
}
