using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVolt : Enemy
{
  [SerializeField] List<GameObject> shots;
  int shotNumber;
  int shotType;

  float distance;

  // Start is called before the first frame update
  void Start()
  {
    m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
    shots[0] = Instantiate(shots[0]);
    shots[1] = Instantiate(shots[1]);

    shots[2] = Instantiate(shots[2]);
    shots[3] = Instantiate(shots[3]);

    shotNumber = 0;
    shotType = Random.Range(0,2);
  }

  // Update is called once per frame
  void Update()
  {
    distance = Mathf.Abs(m_Megaman.transform.position.x - transform.position.x);
    ShootTimer();
  }
  
  public override void ShootTimer()
  {
    if (m_Megaman.position.x <= transform.position.x && distance < 3)
    {
      currentTime -= Time.deltaTime;
      if (currentTime <= 0.0f)
      {
        canShoot = true;
      }
      if (canShoot)
      {
        if (shotType == 0)
        {
          shotNumber++;
          Shoot();
          if (shotNumber == 2)
          {
            currentTime = reloadTime;
            shotNumber = 0;
            shotType = Random.Range(0, 2);
          }
          else
          {
            currentTime = 0.05f;
          }
          canShoot = false;
        }
        else
        {
          shotNumber++;
          Shoot();
          if (shotNumber == 2)
          {
            currentTime = reloadTime;
            shotNumber = 0;
            shotType = Random.Range(0, 2);
          }
          else
          {
            currentTime = reloadTime / 2;
          }
          canShoot = false;
        }
      }
    }
    
  }

  //Fires missiles
  public override void Shoot()
  {
    if(shotType == 0)
    {
      Debug.Log("Shot electroball");
      Debug.Log("Shot missile" + (shotNumber - 1));
      shots[shotType + shotNumber - 1].SetActive(true);
      shots[shotType + shotNumber - 1].GetComponent<Projectile>().Activate(transform.position- new Vector3(0,0.33f,0));
    }
    else
    {
      Debug.Log("Shot missile" + (shotNumber - 1));
      shots[shotType + shotNumber].SetActive(true);
      shots[shotType + shotNumber].GetComponent<Projectile>().Activate(transform.position);
    }
    
  }
}
