using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
  [SerializeField] List<GameObject> platforms;
  [SerializeField] GameObject trigger;
  [SerializeField] float offset;
  [SerializeField] float range;
  [SerializeField] List<Sprite> broken;
  Vector2 size;
  Vector2 pos;
  int state;
  // Start is called before the first frame update
  void Start()
  {
    state = 0;
    pos = new Vector2(platforms[2].transform.position.x, platforms[2].transform.position.y);
    size = new Vector2(1.9f, 0.4748764f);
    platforms[0].transform.GetChild(0).gameObject.SetActive(false);
    platforms[1].transform.GetChild(0).gameObject.SetActive(false);

  }

  // Update is called once per frame
  void Update()
  {
    if(state == 1)
    {
      platforms[2].GetComponent<Rigidbody2D>().MovePosition
        (Vector2.Lerp(platforms[2].transform.position, new Vector2(platforms[2].transform.position.x, platforms[2].transform.position.y - 2f), 3.5f * Time.deltaTime));
      GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
      camera.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
      camera.transform.localPosition = 
        (Vector3.Lerp(camera.transform.localPosition, new Vector3(0, 0, -10), 3.5f * Time.deltaTime));
      if ((Mathf.Abs(platforms[2].transform.position.y-trigger.transform.position.y)) <= 0.3f)
      {
        state = 2;
        
      }
    }
    if(state == 2)
    {
      platforms[2].GetComponent<SpriteRenderer>().sprite = broken[1];
      platforms[0].transform.GetChild(0).gameObject.SetActive(true);
      platforms[1].transform.GetChild(0).gameObject.SetActive(true);
      GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
      
      camera.transform.localPosition =
        (Vector3.Lerp(camera.transform.localPosition, new Vector3(0, 0, -10), 3.5f * Time.deltaTime));
      state++;
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
    }
    if (collision.tag == "Bullet")
    {
      state = 1;
      platforms[2].GetComponent<SpriteRenderer>().sprite = broken[0];
      platforms[2].GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.02356124f);
      platforms[2].GetComponent<BoxCollider2D>().size = size;

      GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>().setAnim(ANIM_STATE.FALL);
    }
    if(collision.gameObject == trigger)
    {
      
    }
  }
}
