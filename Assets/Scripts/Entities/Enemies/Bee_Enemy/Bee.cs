using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{

    public GameObject Disparo;
    public float Fire_Rate;
    public float Next_Shot;
    // Start is called before the first frame update
    void Start()
    {
        Fire_Rate = 1f;
        Next_Shot = Time.time;
        Invoke("Shoot", 1f);
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckIfCanShoot();
    }

    void CheckIfCanShoot()
    {
        if(Time.time > Next_Shot)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject Megaman = GameObject.FindGameObjectWithTag("MegaManco");

        if(Megaman != null)
        {
            GameObject Mina = (GameObject)Instantiate(Disparo);
            Mina.transform.position = transform.position;
            Vector2 direction = Megaman.transform.position - Mina.transform.position;
            Mina.GetComponent<Bee_Shot>().SetDirection(direction);
            Next_Shot = Time.time + Fire_Rate; 
        }

    }
}
