using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limites : MonoBehaviour
{
    //Variables
    private Vector2 Screen_Bounds;
    private float Object_Width;
    private float Object_Height;
    
    // Start is called before the first frame update
    void Start()
    {
        Screen_Bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Object_Width = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Object_Height = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 View_Pos = transform.position;
        View_Pos.x = Mathf.Clamp(View_Pos.x, Screen_Bounds.x + Object_Width, Screen_Bounds.x * -1 - Object_Width);
        View_Pos.y = Mathf.Clamp(View_Pos.y, Screen_Bounds.y + Object_Height, Screen_Bounds.y * -1 - Object_Height);
        transform.position = View_Pos;
    }
}
