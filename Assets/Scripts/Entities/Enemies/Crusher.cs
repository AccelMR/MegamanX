using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
  [SerializeField] List<GameObject> Limits;
  [SerializeField] List<GameObject> DestroyedBlocks;
  private Vector3 m_Dir;
  [SerializeField]
  private BoxCollider2D boxCol;
  public float m_speed = .5f;
  public int m_healt = 3;
  public bool m_isTouching;
  private float m_startFalling;
  public float m_fallTime;
  private float m_constantFalling;
  public bool m_moveRight;
  public int m_state = 0;
  // Start is called before the first frame update
  void Start()
  {
    boxCol = GetComponent<BoxCollider2D>();
  m_isTouching = false;
    m_Dir = Limits[4].transform.position;
  }

  // Update is called once per frame
  void Update()
  {

    if (m_state == 0)
    {
      //m_speed = 0.5f;
      Walk();
    }
    if (m_state == 1)
    {
      
      if (m_moveRight)
      {
        transform.position += new Vector3(0, 1, 0)  * Time.deltaTime;
        transform.localScale = new Vector2(2, 2);
      }
      else
      {
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime;
        transform.localScale = new Vector2(-2, 2);
      }
      if (this.transform.position.y < Limits[2].transform.position.y) // use limit of the game object :)
      {
        m_moveRight = true;
      }
      if (this.transform.position.y > Limits[3].transform.position.y)
      {
        m_moveRight = false;
      }
    }
   
    Attack();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.CompareTag("Player"))
    {
      // Attack
      //Attack();
      m_speed = 0;
      
      m_state = 1;
    }
    if (collision.collider.CompareTag("Crusher"))
    {
      this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    boxCol.size = new Vector2(0.29f, 2.0f);
    this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    m_moveRight = true;
    m_speed = 0.5f;
    // Limits[4].transform.position += new Vector3(0, 1, 0) * 8 * Time.deltaTime;
    m_state = 0;
  }

  void Walk()
  {
    if (m_moveRight)
    {
      transform.position += new Vector3(1, 0, 0)  * m_speed * Time.deltaTime;
      transform.localScale = new Vector2(2, 2);
    }
    else
    {
      transform.position += new Vector3(-1, 0, 0)  * m_speed * Time.deltaTime;
      transform.localScale = new Vector2(-2, 2);
    }
    if(this.transform.position.x < Limits[0].transform.position.x) // use limit of the game object :)
    {
      m_moveRight = true;
    }
    if (this.transform.position.x > Limits[1].transform.position.x)
    {
      m_moveRight = false;
    }
  }

  void Attack()
  {

    Limits[4].transform.position += new Vector3(0, Random.Range( Mathf.Sin( -1 ),-1 ), 0) * 4 * Time.deltaTime;
    
    //if (Limits[4].transform.position.y < -3.0f )
    //{
    //  Limits[4].transform.position += new Vector3(0, 1, 0) * 4 * Time.deltaTime;
    //
    //}
    // Limits[4].transform.position = transform.position;
    // m_state = 0;

  }
}
