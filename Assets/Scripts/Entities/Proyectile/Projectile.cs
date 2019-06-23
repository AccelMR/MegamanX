using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] protected int damage;
  [SerializeField] protected float speed;
  // Start is called before the first frame update
  void Start()
  {
    Deactivate();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
  }
  public virtual void Deactivate()
  {
    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().enabled = false;
    gameObject.SetActive(false);

    //Play Animation
    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
    {
    }
  }
  public virtual void Activate(Vector3 position)
  {
    GetComponent<Collider2D>().enabled = true;
    GetComponent<SpriteRenderer>().enabled = true;
    transform.position = position;

  }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
