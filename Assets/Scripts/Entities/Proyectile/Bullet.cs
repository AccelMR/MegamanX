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

  // Start is called before the first frame update
  void Start()
  {
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
    if(m_wasShoot)
    {
      float xPos = m_velocity * Time.fixedDeltaTime * m_dir;

      transform.position += new Vector3(xPos, 0.0f, 0.0f);
    }

  }

  public void beeingShot(Vector3 characterPos, float dir)
  {
    transform.position = new Vector3(characterPos.x * dir, characterPos.y + .2f, characterPos.z);

    this.enabled = true;
    m_wasShoot = true;
    m_dir = dir;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    m_wasShoot = false;
    this.enabled = false;
  }

}
