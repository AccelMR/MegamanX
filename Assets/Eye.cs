using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private float m_velocity = 0.5f;
    private Vector3 Dir;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Dir = Target.position - transform.position;
        Dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * Mathf.Sign(Dir.x) * m_velocity * Time.deltaTime;
    }
}
