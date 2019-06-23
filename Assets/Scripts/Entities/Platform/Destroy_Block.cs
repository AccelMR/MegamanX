using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Block : MonoBehaviour
{
 [SerializeField] List<Sprite> Brokens;
  public GameObject Block;
  public float[,] Grid;
  int Vertical, Horizontal;
  int Columns = 4;
  int Rows = 3;
  float tileSize = .46f;
  private GameObject tile;
  private BoxCollider2D Box;
    // Start is called before the first frame update
   void Start()
   {
    transform.position = new Vector2(39.6f, -5.05f);
    GenerateGrid();
   }

  void GenerateGrid()
  {
    GameObject referenceTile = (GameObject)Instantiate(Block);
    for (int Row = 0; Row < Rows; Row++)
    {
      for (int Col = 0; Col < Columns; Col++)
      {
        tile = (GameObject)Instantiate(referenceTile, transform);
        tile.layer = 9;
        Box = tile.AddComponent<BoxCollider2D>();
        float posX = (Col * tileSize) + 20.6f;
        float posY = (Row * -.28f) +.05f ;
        Box.transform.position = new Vector2(posX, posY);
       
      }
    }
    Destroy(referenceTile); 
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    Box.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
   // Block.GetComponent<SpriteRenderer>().sprite = Brokens[10];
    if (collision.collider.CompareTag("CrusherSon"))
    {
    }
  }

  // Update is called once per frame
  void Update()
    {
    }
}
