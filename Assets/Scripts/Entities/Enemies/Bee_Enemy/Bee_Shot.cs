using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Shot : MonoBehaviour
{
    public float Speed;
    Vector2 _Direction;
    private bool isReady;
    Rigidbody2D rb;

    void Awake()
    {
        Speed = 5f;
        isReady = false;
       rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {

    }

    public void SetDirection(Vector2 Direction)
    {
        _Direction = Direction.normalized;
        isReady = true;
    }

    void Update()
    {
        
        if (isReady)
        {
            Vector2 position = transform.position;
            position += _Direction * Speed * Time.deltaTime;
            transform.position = position;
        }

        
    }
    void OnTriggerEnter2D(Collider2D cool)
    {
        if (cool.gameObject.CompareTag("ground"))
        {
            Debug.Log("Toco en collider");
            Speed = 0f;
        }
        if (cool.gameObject.CompareTag("MegaManco"))
        {
            Destroy(gameObject);
        }
    }

    

   
}
