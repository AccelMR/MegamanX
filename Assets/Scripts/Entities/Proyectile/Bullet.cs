using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField]
  private float m_velocity;

  private bool m_wasShoot;

  private float m_dir;

  private BoxCollider2D m_collider;

  private SpriteRenderer m_renderer;

  // Start is called before the first frame update
  void Start()
  {
    m_renderer = GetComponentInChildren<SpriteRenderer>();
    m_collider = GetComponent<BoxCollider2D>();
    m_wasShoot = false;
    m_dir = 0.0f;
  }

  // Update is called once per frame
  void Update()
  {
  }

  //Fixed Update
  private void FixedUpdate()
  {
    if(m_wasShoot /*&& m_renderer.enabled*/)
    {
      float xPos = m_velocity * Time.fixedDeltaTime * m_dir;

      transform.position += new Vector3(xPos, 0.0f, 0.0f);
    }
    if (!m_renderer.isVisible && m_wasShoot)
    {
      disable(false);
    }
  }

  public void beeingShot(Vector3 characterPos, float dir)
  {
    Debug.Log("Single shoot");

    if (m_collider.enabled) return;
    
    disable(true);

    transform.position = new Vector3(characterPos.x + (0.25f * dir), 
                                     characterPos.y + .02f, 
                                     characterPos.z);

    m_dir = dir;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      return;
    }
    if (collision.CompareTag("Bullet"))
    {
      return;
    }

    //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    disable(false);
  }

  private void disable(bool bul)
  {
    m_wasShoot = bul;
    m_collider.enabled = bul;
    m_renderer.enabled = bul;
  }

}
