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

  private Animator m_animator;
  public Animator Anim
  {
    get
    {
      if (null == m_animator)
      {
        m_animator = GetComponentInChildren<Animator>();
        if (null == m_animator)
        {
          //Throw
        }
      }
      return m_animator;
    }
  }

  [SerializeField]
  private float m_offsetX;
  [SerializeField]
  private float m_offsetY;

  private float m_livingTime;


  [SerializeField]
  private float m_debugTime;

  
  // Start is called before the first frame update
  void Start()
  {
    m_renderer = GetComponentInChildren<SpriteRenderer>();
    m_collider = GetComponent<BoxCollider2D>();
    m_wasShoot = false;
    m_dir = 0.0f;
    m_livingTime = 0.0f;

    //
    m_debugTime = 0.0f;

  }

  // Update is called once per frame
  void Update()
  {
  }

  //Fixed Update
  private void FixedUpdate()
  {
    if(m_wasShoot)
    {
      m_livingTime += Time.fixedDeltaTime;

      float xPos = Time.fixedDeltaTime * m_velocity * m_dir;

      transform.position += new Vector3(xPos, 0.0f, 0.0f);

      //Debug
      m_debugTime += Time.fixedDeltaTime;
    }
    if (!m_renderer.isVisible && m_wasShoot)
    {
      disable(false);

      //Debug
      Debug.Log(m_debugTime);
      m_debugTime = 0.0f;
    }
  }

  public void beeingShot(Vector3 characterPos, float dir)
  {
    if (m_collider.enabled) return;
    

    transform.position = new Vector3(characterPos.x + (m_offsetX * dir), 
                                     characterPos.y + m_offsetY, 
                                     characterPos.z);
    disable(true);

    var scale = transform.GetChild(0).localScale;
    transform.GetChild(0).localScale = scale;
    scale.x = Mathf.Abs(scale.x) * dir;
    transform.GetChild(0).localScale = scale;

    if (Anim)
    {
      Anim.SetBool("Init", true);
    }

    m_dir = dir;

    //Debug
    m_debugTime = 0.0f;

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

    disable(false);
  }

  private void disable(bool bul)
  {
    m_wasShoot = bul;
    m_collider.enabled = bul;
    m_renderer.enabled = bul;
    m_livingTime = 0.0f;

    if (Anim)
    {
      Anim.SetBool("Init", false);
    }
  }

}
