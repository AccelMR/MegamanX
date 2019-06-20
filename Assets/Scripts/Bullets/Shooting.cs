using UnityEngine;
using System.Collections;

public class Light_Charge_Shot : MonoBehaviour
{
    public Transform Charge_Spawn;
    public GameObject Shot;
    public GameObject Charge_Shot_Light;
    public GameObject Charge_Shot_Heavy;
    private float Fire_Time;
    private bool charging;
    private float Charge_Time = 3f;
    public bool Can_Shoot = false;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {

            GameObject Bullet = (GameObject)Instantiate(Shot);
            Bullet.transform.position = new Vector3(transform.position.x + .8f, transform.position.y + .05f, -1);
            Fire_Time = Time.time + 0.5f;
        }

        if(Input.GetKeyUp(KeyCode.Z))
        {
            charging = false;
            if (Charge_Time > 1f && Charge_Time < 3f)
            {
                GameObject Bullet = (GameObject)Instantiate(Charge_Shot_Heavy);
                Bullet.transform.position = new Vector3(transform.position.x + .8f, transform.position.y + .05f, -1);
                Fire_Time = Time.time + 0.5f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            charging = false;
            if(Charge_Time >= 3f)
            {
                GameObject Bullet = (GameObject)Instantiate(Charge_Shot_Heavy);
                Bullet.transform.position = new Vector3(transform.position.x + .8f, transform.position.y + .05f, -1);
                Fire_Time = Time.time + 0.5f;
            }
        }
    
        if (Input.GetKey(KeyCode.Z))
        {
            if (!charging)
            {
                charging = true;
                Charge_Time = 0;
            }
            else
            {
                Fire_Time = Time.time + 0.5f;
                Charge_Time += Time.deltaTime;
            }
        }


    }
    

}