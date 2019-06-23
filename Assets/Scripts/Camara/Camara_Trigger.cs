using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Trigger : MonoBehaviour
{
  public Camara m_camara;

  void OnTriggerEnter2D(Collider2D colision)
  {
    if (colision.gameObject.tag == "FirstFloor")
    {
      m_camara.FollowY();
    }
  }
}
