using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] protected int damage;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position += new Vector3(-1, 0, 0) * 2.0f * Time.deltaTime;
  }
  public virtual void Deactivate()
  {
    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().enabled = false;

    //Play Animation
    if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
    {
      gameObject.SetActive(false);
    }
  }
  public virtual void Activate()
  {
    GetComponent<Collider2D>().enabled = true;
    GetComponent<SpriteRenderer>().enabled = true;
    gameObject.SetActive(true);

  }
}
