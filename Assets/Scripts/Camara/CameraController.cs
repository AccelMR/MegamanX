using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] float offset;
  [SerializeField] List<Sprite> broken;
  Vector2 offsetVector;
  // Start is called before the first frame update
  void Start()
  {
    offsetVector = new Vector2(0.01258147f, 0.01558709f);
    transform.GetChild(1).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

  }

  // Update is called once per frame
  void Update()
  {

  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
      camera.transform.parent = null;
      Vector3 desired = new Vector3(transform.position.x + offset, camera.transform.position.y, camera.transform.position.z);
      camera.transform.position = desired;
    }
    if (collision.tag == "Bullet")
    {
      transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = broken[0];
      transform.GetChild(1).GetComponent<Collider2D>().offset = offsetVector;
      transform.GetChild(1).GetComponent<Rigidbody2D>().gravityScale = 2;
      transform.GetChild(1).GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;

      GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>().setAnim(ANIM_STATE.FALL);
      GameObject.FindGameObjectWithTag("Player").transform.parent = GameObject.Find("Middle").transform;
    }
  }
}
