using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Transform m_Megaman;
  //Shooting variables
  [SerializeField] protected float reloadTime;
  protected float currentTime;
  protected bool canShoot;
  //Stats
  [SerializeField] protected float m_speed;
  [SerializeField] protected int Max_Health;
  protected int health;
  protected Vector3 Pos;

  // Start is called before the first frame update
  void Start()
  {
    health = Max_Health;
    currentTime = reloadTime;
    canShoot = false;
  }

  public virtual void Reset()
  {

  }
  public virtual void ShootTimer()
  {
    currentTime -= Time.deltaTime;
    if(currentTime <= 0.0f)
    {
      canShoot = true;
    }
    if (canShoot)
    {
      Shoot();
      canShoot = false;
      currentTime = reloadTime;
    }
  }
  public virtual void Shoot()
  {
    
  }
}
