using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
  //Variables
  public Transform Mega_Man;
  public float Distancia_Camara;
  public bool seguirY = false;

  bool state;
  void Awake()
  {
    state = false;
    GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / Distancia_Camara);
  }

  // Update is called once per frame
  void Update()
  {
    if(state)
    {
      if (transform.position.y >= 0)
      {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        return;
      }
    }
    
    //if (Mathf.Abs(Mega_Man.transform.position.y - transform.position.y) > 0.75f)
    //{
    //  state = true;
    //}
    //if (transform.position.y >= 0 && Mega_Man)
    //{

    //  state = false;

    //}
    //seguirY = state;
    //if (!seguirY)
    //{
    //  transform.position = new Vector3(Mega_Man.position.x, 0, transform.position.z);
    //}
    //else
    //{
    //  transform.position = Vector3.Lerp(transform.position, new Vector3(Mega_Man.position.x, Mega_Man.position.y, transform.position.z), Time.deltaTime);
    //}
    
  }
  public void FollowY()
  {
    state = true;
  }
}
