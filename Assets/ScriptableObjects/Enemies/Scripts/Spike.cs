using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform m_Megaman;
  public float m_speed = .05f;
  private Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
       m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
      Pos = m_Megaman.position - transform.position;
      Pos.Normalize();
      //transform.position *= Mathf.Sign(Pos.x);
      
    }

    // Update is called once per frame
    void Update()
    {
     //Pos = m_Megaman.position - transform.position;
     //Pos.Normalize();
      transform.position += new Vector3(1, 0, 0) * Mathf.Sign( Pos.x) * m_speed * Time.deltaTime;
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
}
