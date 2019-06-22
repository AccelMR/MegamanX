using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] float offset;
  [SerializeField] float range;
  [SerializeField] List<Sprite> broken;
  Vector2 offsetVector;
  bool isEnabled;
  // Start is called before the first frame update
  void Start()
  {
    isEnabled = false;
    offsetVector = new Vector2(0.01258147f, 0.01558709f);
    transform.GetChild(1).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    gameObject.GetComponents<BoxCollider2D>()[1].enabled = false;

  }

  // Update is called once per frame
  void Update()
  {
    if (isEnabled)
    {
    }
    else
    {

    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
      camera.transform.parent = null;
      Vector3 desired = new Vector3(transform.position.x + offset, camera.transform.position.y, camera.transform.position.z);
      camera.transform.position = desired;
      gameObject.GetComponents<BoxCollider2D>()[1].enabled = true;
      isEnabled = true;
    }
    if (collision.tag == "Bullet")
    {
      transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = broken[0];
      transform.GetChild(1).GetComponent<Collider2D>().offset = offsetVector;
      transform.GetChild(1).GetComponent<Rigidbody2D>().gravityScale = 2;
      transform.GetChild(1).GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
      gameObject.GetComponents<BoxCollider2D>()[1].enabled = false;

      GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>().setAnim(ANIM_STATE.FALL);
      GameObject.FindGameObjectWithTag("Player").transform.parent = GameObject.Find("Middle").transform;
    }
  }
}
