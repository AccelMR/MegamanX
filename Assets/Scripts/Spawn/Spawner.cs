using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] protected GameObject enemy;
  [SerializeField] protected float range;
  [SerializeField] protected bool canRespawn;
  protected Vector3 position;

  void Start()
  {
    position = transform.position;
    enemy = Instantiate(enemy.gameObject);
    KillEnemy();
  }

  public virtual void SpawnEnemy()
  {
    ResetEnemy();
    enemy.GetComponent<Enemy>().Reset();
  }
  public virtual void ResetEnemy()
  {
    enemy.transform.position = position;

    enemy.SetActive(true);


  }
  public virtual void KillEnemy()
  {
    enemy.SetActive(false);

  }
}
