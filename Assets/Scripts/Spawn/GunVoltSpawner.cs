using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVoltSpawner : Spawner
{
  int MaxHealth;
  // Start is called before the first frame update
  void Start()
  {
    position = transform.position;
    enemy = Instantiate(enemy.gameObject);
    enemy.transform.position = position;
    MaxHealth = enemy.GetComponent<Enemy>().MaxHealth;
    canRespawn = false;
  }

  // Update is called once per frame
  void Update()
  {
    float distance = Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x);
    Debug.DrawLine(transform.position, transform.position - new Vector3(range, 0, 0), Color.red);
    if (distance > range  && enemy != null)
    {
      if(enemy.GetComponent<Enemy>().Health < MaxHealth)
      {
      SpawnEnemy();
      }
    }
    if(enemy.GetComponent<GunVolt>().Health <= 0)
    {
      KillEnemy();
      Destroy(this.gameObject);
    }
  }

  public override void SpawnEnemy()
  {
    enemy.GetComponent<Enemy>().Reset();
    Debug.Log("Enemy Healed");
  }

  public override void KillEnemy()
  {
    Destroy(enemy.gameObject);
    Debug.Log("Enemy destroyed");
    enemy = null;

  }
}
