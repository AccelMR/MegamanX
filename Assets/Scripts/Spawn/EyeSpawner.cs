using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : Spawner
{
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
        if (distance <= range && !enemy.activeSelf)
        {
            if (canRespawn)
            {
                SpawnEnemy();
            }
        }
        float enemy2player = Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.position.x - enemy.transform.position.x);
        float enemy2spawner = Mathf.Abs(enemy.transform.position.x - transform.position.x);
        if (enemy2player > range && enemy.activeSelf && enemy2spawner > range && distance > range || enemy.GetComponent<Eye>().Health <= 0)
        {
            KillEnemy();
        }
    }

    public override void SpawnEnemy()
    {
        ResetEnemy();
        enemy.GetComponent<Enemy>().Reset();
    }
    public override void ResetEnemy()
    {
        enemy.transform.position = position;

        enemy.SetActive(true);
        enemy.GetComponent<Animator>().SetBool("startWalking", true);

    }
    public override void KillEnemy()
    {
        enemy.SetActive(false);

    }
}
