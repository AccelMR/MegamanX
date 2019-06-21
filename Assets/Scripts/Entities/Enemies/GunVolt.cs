using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVolt : Enemy
{
  [SerializeField] List<GameObject> shots;
  int shotNumber;
  int shotType;
  // Start is called before the first frame update
  void Start()
  {
    shots[0] = Instantiate(shots[0]);
    shots[1] = Instantiate(shots[1]);

    shotNumber = 0;
    shotType = Random.Range(0,1);
  }

  // Update is called once per frame
  void Update()
  {
    ShootTimer();
  }
  
  public override void ShootTimer()
  {
    if(m_Megaman.position.x <= transform.position.x)
    {
      currentTime -= Time.deltaTime;
      if (currentTime <= 0.0f)
      {
        canShoot = true;
      }
      if (canShoot)
      {
        if (shotType != 0)
        {
          Shoot();
          currentTime = reloadTime;
          canShoot = false;
          shotType = Random.Range(0, 1);
        }
        else
        {
          Shoot();
          shotNumber++;
          if (shotNumber == 2)
          {
            currentTime = reloadTime;
            shotType = Random.Range(0, 1);
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
    shots[shotType].SetActive(true);
  }
}
