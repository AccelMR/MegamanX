using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Trigger : MonoBehaviour
{
  public Camara m_camara;

  void OnTriggerEnter2D(Collider2D colision)
  {
    if (colision.gameObject.tag == "Cambio1")
    {
      if (m_camara.seguirY)
        m_camara.seguirY = false;
      else
      {
        m_camara.seguirY = true;
        m_camara.enabled = true;

      }
    }
  }
}
