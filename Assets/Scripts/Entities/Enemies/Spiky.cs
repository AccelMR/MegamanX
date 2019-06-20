using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiky : MonoBehaviour
{
    public Rigidbody2D m_spiky;
    // Start is called before the first frame update
    void Start()
    {
      m_spiky = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      transform.position += new Vector3(1, 0, 0);
    }
}
