using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
  private SpriteRenderer m_blockState;
  private Rigidbody2D m_block;
  public float fallDelay;
  public Sprite BrokeChunk;
  public List<Sprite> PlatformState;
  void Start()
  {
    m_block = GetComponent<Rigidbody2D>();
    //m_blockState.sprite = PlatformState[0];
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.CompareTag("Untagged"))
    {
      StartCoroutine(Fall());
    }
  }

  IEnumerator Fall()
  {
    yield return new WaitForSeconds(fallDelay);
    m_block.isKinematic = false;
   
    this.GetComponent<SpriteRenderer>().sprite = BrokeChunk;
    this.GetComponent<Rigidbody2D>().gravityScale = .2f;
    //GetComponent<Collider2D>().isTrigger = true;
    yield return 0;
  }
}
