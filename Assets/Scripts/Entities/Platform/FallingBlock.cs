using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
  private SpriteRenderer m_blockState;
  private Rigidbody2D m_block;
  public Sprite BrokeChunk;
  public List<Sprite> PlatformState;

  public bool m_wasTouched;
  private float m_startFalling;
  public float m_fallTime;
  private float m_constantFalling;

  void Start()
  {
    m_block = GetComponent<Rigidbody2D>();
    m_wasTouched = false;
    m_startFalling = 0.0f;
    m_fallTime = 1.5f;
    m_constantFalling = 0.05f;
    //m_blockState.sprite = PlatformState[0];
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if ( collision.collider.CompareTag("Player"))
    {
    //  collision.collider.transform.SetParent(transform);
      this.gameObject.GetComponent<SpriteRenderer>().sprite = BrokeChunk;

      m_wasTouched = true;
    }
    //if (collision.collider.CompareTag("Untagged"))
    //{
    //  StartCoroutine(Fall());
    //}
  }

  private void Update()
  {
    if(m_wasTouched)
    {
      m_startFalling += Time.deltaTime;
    }
    if(m_startFalling >= m_fallTime)
    {
      transform.position += new Vector3(0, -m_constantFalling, 0);
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    m_wasTouched = false;
     // collision.collider.transform.SetParent(transform);
  }
}
