using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
  [SerializeField] List<GameObject> Blocks;
  [SerializeField] List<Sprite> Sprites;
  public Sprite Sp;
  // Start is called before the first frame update
  void Start()
    {
      // Primer piso
      Blocks[4].GetComponent<SpriteRenderer>().sprite = Sprites[3];
      Blocks[5].GetComponent<SpriteRenderer>().sprite = Sprites[4];
      Blocks[6].GetComponent<SpriteRenderer>().sprite = Sprites[4];
      Blocks[7].GetComponent<SpriteRenderer>().sprite = Sprites[6];
      // Segundo piso
      //Blocks[0].GetComponent<SpriteRenderer>().color = new Color(55f, 0.3f, 20f, 1f);
      Blocks[0].GetComponent<SpriteRenderer>().sprite = Sprites[7];
      Blocks[1].GetComponent<SpriteRenderer>().sprite = Sprites[5];
      Blocks[2].GetComponent<SpriteRenderer>().sprite = Sprites[5];
      Blocks[3].GetComponent<SpriteRenderer>().sprite = Sprites[9];
      // Tercer piso
      Blocks[8].GetComponent<SpriteRenderer>().sprite  = Sprites[3];
      Blocks[9].GetComponent<SpriteRenderer>().sprite  = Sprites[4];
      Blocks[10].GetComponent<SpriteRenderer>().sprite = Sprites[4];
      Blocks[11].GetComponent<SpriteRenderer>().sprite = Sprites[6];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

  private void OnTriggerEnter2D(Collider2D collision)
  {

    for (int i = 0; i < Blocks.Count; i++)
    {
      if (collision.name == "Block")
      {
        Blocks[3].GetComponent<SpriteRenderer>().sprite = Sp;
      }
      //if (collision.name == "dtCrusher")
       // Blocks[i].GetComponent<SpriteRenderer>().sprite = Sp;
    }
  }
}
