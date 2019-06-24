using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
  public GameObject Megaman;
  public GameObject Limit_One;
  public GameObject Limit_Two;
  private float Distance;
  public float attenuation = 2;
  public float range = 2;
  public bool lightOn;
  private Color color1 = new Color(1, 1, 1, 1);
  private Color color2 = new Color(.50f, .50f, .50f, 1);

    // Start is called before the first frame update
  void Start()
    {

    }

    // Update is called once per frame
  void Update()
  {
    Distance = Limit_Two.transform.position.x - Megaman.transform.position.x;
    Distance = Mathf.Abs(Distance);
    attenuation = range / Distance;
    color1 = gameObject.GetComponent<SpriteRenderer>().color;
      if ( Distance < range)
      {
      gameObject.GetComponent<SpriteRenderer>().color = color2;//Color.Lerp(color1, color2, Time.deltaTime);// (1,1,1, 1) * attenuation;
      }
      else
       color1 = gameObject.GetComponent<SpriteRenderer>().color;
    //if (lightOn)
    //{
    //}
    //else
    //  gameObject.GetComponent<SpriteRenderer>().color = new Color(.50f, .50f, .50f, 1);

    //if (Megaman.transform.position.x < Limit_One.transform.position.x)
    //{
    //  lightOn = true;
    //}
    //if (Megaman.transform.position.x > Limit_One.transform.position.x)
    //{
    //  lightOn = false;
    //}
    //if (Megaman.transform.position.x > Limit_Two.transform.position.x)
    //{
    //  lightOn = true;
    //}
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
   
  }
}
