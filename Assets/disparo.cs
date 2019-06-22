using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo : MonoBehaviour
{
    Object bullet_ref;
    // Start is called before the first frame update
    void Start()
    {
        bullet_ref = Resources.Load("Disparo");
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
                {
            GameObject bullet = (GameObject)(Instantiate(bullet_ref));
            bullet.transform.position = new Vector3(transform.position.x + .4f, transform.position.y + .2f, -1);

        }
    }
}
