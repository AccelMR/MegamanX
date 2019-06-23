using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBlock : MonoBehaviour
{
  [SerializeField] List<Sprite> Brokens;

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.CompareTag("Crusher"))
    {
      this.gameObject.GetComponent<Collider2D>().enabled = false;
      this.gameObject.GetComponent<SpriteRenderer>().sprite = Brokens[10];
    }

  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.name == "dtCrusher")
      this.gameObject.GetComponent<Collider2D>().enabled = false;
  }
}
