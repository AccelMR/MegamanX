﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] GameObject enemy;
  [SerializeField] float range;
  [SerializeField] bool canRespawn;
  Vector3 position;

  void Start()
  {
    position = transform.position;
    enemy = Instantiate(enemy.gameObject);
    KillEnemy();
  }

  // Update is called once per frame
  void Update()
  {
    float distance = Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x);
    Debug.DrawLine(transform.position, transform.position + new Vector3(range, 0, 0), Color.red);
    if(distance <= range && !enemy.activeSelf)
    {
      if (canRespawn)
      {
        SpawnEnemy();
      }
    }
    float enemy2player = Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.position.x - enemy.transform.position.x);
    float enemy2spawner = Mathf.Abs(enemy.transform.position.x - transform.position.x);
    if (enemy2player > range && enemy.activeSelf && enemy2spawner > range && distance > range)
    {
      KillEnemy();
    }
  }
  void SpawnEnemy()
  {
    ResetEnemy();
    enemy.GetComponent<Enemy>().Reset();
  }
  public void ResetEnemy()
  {
    enemy.transform.position = position;

    enemy.SetActive(true);


  }
  void KillEnemy()
  {
    enemy.SetActive(false);

  }
}
