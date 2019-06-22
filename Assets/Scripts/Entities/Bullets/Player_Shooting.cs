using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    Object bullet_Ref;
    // Start is called before the first frame update
    void Start()
    {
        bullet_Ref = Resources.Load("Bala");
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject Bullet = (GameObject)Instantiate(bullet_Ref);
            Bullet.transform.position = new Vector3(transform.position.x + .8f, transform.position.y + .05f, -1);
        }
    }
}
