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
  [SerializeField] protected int damage;
  [SerializeField] protected int Max_Health;
  protected int health;
  protected Vector3 Pos;

  // Start is called before the first frame update
  void Start()
  {
    m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
    health = Max_Health;
    currentTime = reloadTime;
    canShoot = false;
  }

  public virtual void Reset()
  {
    m_Megaman = GameObject.FindGameObjectWithTag("Player").transform;
    health = Max_Health;
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

  protected virtual void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Bullet")
    {
      health -= 2;
      collision.gameObject.GetComponent<Collider2D>().enabled = false;
      Debug.Log("Enemy Hit.   New health = " + health);
    }
    if (collision.gameObject.tag == "Bullet1")
    {
      health -= 3;
      collision.gameObject.GetComponent<Collider2D>().enabled = false;
      Debug.Log("Enemy Hit.   New health = " + health);
    }
    if (collision.gameObject.tag == "Bullet2")
    {
      health -= 4;
      collision.gameObject.GetComponent<Collider2D>().enabled = false;
      Debug.Log("Enemy Hit.   New health = " + health);
    }
  }

  public int Health
  {
    get { return health; }
  }
  public virtual int MaxHealth
  {
    get { return Max_Health; }
  }
  public virtual int Damage
  {
    get { return damage; }
  }
}
