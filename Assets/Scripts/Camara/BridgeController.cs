using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
  [SerializeField] List<Sprite> broken;
  Vector2 offset;
  // Start is called before the first frame update
  void Start()
  {
    offset = new Vector2(1.89f, 0.494579f);
  }

  // Update is called once per frame
  void Update()
  {

  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Middle")
    {
      collision.gameObject.GetComponent<SpriteRenderer>().sprite = broken[0];
      collision.gameObject.GetComponent<BoxCollider2D>().size = offset;
      collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
      collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

      GameObject.FindGameObjectWithTag("Player").transform.parent = null;

    }
  }
}
