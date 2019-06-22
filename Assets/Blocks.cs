using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
  private Rigidbody2D m_block;
  public Sprite BrokeChunk;
  public List<Sprite> PlatformState;
  public GameObject L_Block;
  public GameObject R_Block;

  private bool m_wasTouched;
  private float m_startFalling;
  public float m_fallTime;
  private float m_constantFalling;

  void Start()
  {
    this.gameObject.GetComponent<SpriteRenderer>().sprite = PlatformState[0];
    m_block = GetComponent<Rigidbody2D>();
    m_wasTouched = false;
    m_startFalling = 0.0f;
    m_fallTime = 1.5f;
    m_constantFalling = 0.05f;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.CompareTag("Player"))
    {
     // collision.collider.transform.SetParent(transform);
      this.gameObject.GetComponent<SpriteRenderer>().sprite = BrokeChunk;
      m_wasTouched = true;
    }
  }

  private void Update()
  {
    // Left side Falling
    if (L_Block.GetComponent<FallingBlock>().m_wasTouched == true)
    {
      // Se desactiva el block
      this.gameObject.GetComponent<SpriteRenderer>().sprite = BrokeChunk;
      this.gameObject.GetComponent<SpriteRenderer>().transform.position = new Vector3(8.234f,3.2f,0f);
      this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
      // Se cambia el R_Block 
      //L_Block.GetComponent<SpriteRenderer>().sprite = BrokeChunk;
      R_Block.GetComponent<SpriteRenderer>().sprite = PlatformState[1];
      R_Block.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }
    // Right Side Falling
    //if (L_Block.GetComponent<FallingBlock>().m_wasTouched == false && m_wasTouched == false && R_Block.GetComponent<FallingBlock>().m_wasTouched == true)
    //{
    //  // Se desactiva el block
    //  this.gameObject.GetComponent<SpriteRenderer>().sprite = BrokeChunk;
    //  this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    //  // Se cambia el R_Block 
    //  L_Block.GetComponent<SpriteRenderer>().sprite = PlatformState[2];
    //  L_Block.GetComponent<SpriteRenderer>().sortingOrder = 3;
    //}
    // Se cae el bloque solo
    if (m_wasTouched)
    {
   

      m_startFalling += Time.deltaTime;
    }
    if (m_startFalling >= m_fallTime)
    {
      transform.position += new Vector3(0, -m_constantFalling, 0);
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {

    //collision.collider.transform.SetParent(transform,false);
    m_wasTouched = false;
  }
}
